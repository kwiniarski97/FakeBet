using FakeBet.API.Email;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Fluent;
using LogLevel = NLog.LogLevel;

namespace FakeBet.API.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using AutoMapper;
    using FakeBet.API.DTO;
    using FakeBet.API.Helpers;
    using FakeBet.API.Models;
    using FakeBet.API.Repository.Interfaces;
    using FakeBet.API.Services.Interfaces;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    public class UserService : IUserService
    {
        private IUserRepository repository;

        private IMapper mapper;

        private readonly IEmailClient _emailClient;

        private readonly AppSettingsSecret _appSettings;


        public UserService(IUserRepository repository, IMapper mapper, IOptions<AppSettingsSecret> appSettings,
            IEmailClient emailClient)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._appSettings = appSettings.Value;
            this._emailClient = emailClient;
        }

        public async Task RegisterUserAsync(UserAuthDTO userAuthDto)
        {
            if (string.IsNullOrEmpty(userAuthDto.Password) || userAuthDto.Password.Length < 8
                                                           || string.IsNullOrWhiteSpace(userAuthDto.Email)
                                                           || string.IsNullOrWhiteSpace(userAuthDto.NickName))
            {
                throw new Exception("Password is invalid");
            }

            if (await GetUserAsync(userAuthDto.NickName) != null)
            {
                throw new Exception("Username is already taken");
            }

            if (!(await IsEmailUnique(userAuthDto.Email)))
            {
                throw new Exception("Email is already taken");
            }

            Authorization.CreatePasswordHash(userAuthDto.Password, out var passwordHash, out var passwordSalt);

            var user = mapper.Map<UserAuthDTO, User>(userAuthDto);
            user.PasswordHash = passwordHash;
            user.Salt = passwordSalt;

            await repository.RegisterUserAsync(user);
            
            this._emailClient.SendActivationLink(user.Email, user.NickName);
        }

        private async Task<bool> IsEmailUnique(string email)
        {
            var user = await this.repository.GetUserByEmailAsync(email);
            return user == null;
        }

        public async Task<string> LoginUserAsync(string nickname, string password)
        {
            if (string.IsNullOrEmpty(nickname) || string.IsNullOrEmpty(password))
            {
                throw new Exception("NickName or password cannot be empty");
            }

            var user = await repository.GetUserWithoutBetsAsync(nickname);

            if (user == null)
            {
                throw new Exception("We cannot find such user.");
            }

            if (!Authorization.VerifyPasswordHash(password, user.PasswordHash, user.Salt))
            {
                user.IncreaseFailedLoginCounter();

                if (user.FailedLoginsAttemps >= 10)
                {
                    user.Status = UserStatus.NonActivated;

                    this._emailClient.NotifyAboutBlockade(user.Email);
                }

                await this.repository.UpdateUserAsync(user);

                throw new Exception("Password or login incorrect.");
            }

            switch (user.Status)
            {
                // todo refractor
                case UserStatus.Deactivated:
                    throw new Exception("Your account is deleted. Contact the administrator to get it back.");
                case UserStatus.NonActivated:
                    throw new Exception("Your account is not activated. Check your email for activation link.");
                case UserStatus.Banned:
                    throw new Exception(
                        "Your account is banned. If you think you've been banned by accident contact administrator.");
            }

            if (user.FailedLoginsAttemps > 0) // user logged in so reset his failure login counter
            {
                user.ResetFailedLoginCounterAsync();
                await this.repository.UpdateUserAsync(user);
            }

            var token = GenerateJwtToken(user);

            return token;
        }

        public async Task<UserDTO> GetUserAsync(string nickName)
        {
            var user = await repository.GetUserWithoutBetsAsync(nickName);
            return mapper.Map<User, UserDTO>(user);
        }

        public async Task ChangeUserStatusAsync(string nickName, UserStatus status)
        {
            if (await GetUserAsync(nickName) == null)
            {
                throw new Exception("User not found");
            }

            await repository.ChangeUserStatusAsync(nickName, status);
        }

        public async Task<List<UserTopDTO>> Get20BestUsersAsync()
        {
            var users = await repository.Get20BestUsersAsync();
            var userTopDtos = mapper.Map<List<UserTopDTO>>(users);
            return userTopDtos;
        }

        public async Task UpdateEmailAsync(UserAuthDTO userDto)
        {
            var user = await GetUserIfLogged(userDto.NickName, userDto.Password);


            user.Email = userDto.Email;

            await this.repository.UpdateUserAsync(user);
        }

        public async Task DeleteAccountAsync(UserAuthDTO user)
        {
            var originalUser = await GetUserIfLogged(user.NickName, user.Password);

            originalUser.Status = UserStatus.Deactivated;

            await this.repository.UpdateUserAsync(originalUser);
        }

        public async Task UpdatePasswordAsync(ChangePasswordDTO model)
        {
            var user = await GetUserIfLogged(model.NickName, model.CurrentPassword);

            Authorization.CreatePasswordHash(model.NewPassword, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.Salt = passwordSalt;

            await this.repository.UpdateUserAsync(user);
        }

        public async Task AddUserPointsAsync(string userId, int wonPoints)
        {
            var user = await this.repository.GetUserWithoutBetsAsync(userId);

            user.Points += wonPoints;

            await this.repository.UpdateUserAsync(user);
        }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await this.repository.GetAllUsersAsync();
            var usersDto = this.mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(users);
            return usersDto;
        }

        public async Task UpdateUserAsync(UserDTO userDTO)
        {
            var user = await this.repository.GetUserWithoutBetsAsync(userDTO.NickName);
            if (user == null)
            {
                throw new Exception($"User with {userDTO.NickName} nickname not found");
            }

            //todo refractor

            user.Email = userDTO.Email;

            user.Points = userDTO.Points;

            user.Status = userDTO.Status;

            user.Role = userDTO.Role;

            if (user.Status.Equals(UserStatus.Banned) && !user.Status.Equals(userDTO.Status))
            {
                this._emailClient.NotifyAboutBan(user.NickName);
            }

            await this.repository.UpdateUserAsync(user);
        }

        public async Task ActivateUserAsync(string encodedValue)
        {
            var decodedNickName = Encryptor.FromBase64(encodedValue);
            var user = await GetUserAsync(decodedNickName);
            if (user == null)
            {
                throw new Exception("No such user");
            }
            user.Status = UserStatus.Active;
            await this.UpdateUserAsync(user);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    new[]
                    {
                        new Claim(
                            ClaimTypes.Name,
                            user.NickName),
                        new Claim(
                            ClaimTypes.Role,
                            user.Role.ToString()),
                        new Claim(
                            "Status",
                            user.Status.ToString())
                    }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<User> GetUserIfLogged(string nickName, string password)
        {
            var user = await this.repository.GetUserWithoutBetsAsync(nickName);
            if (user == null)
            {
                throw new Exception("You must be logged!");
            }

            if (!Authorization.VerifyPasswordHash(password, user.PasswordHash, user.Salt))
            {
                throw new Exception("Wrong current password");
            }

            return user;
        }
    }
}