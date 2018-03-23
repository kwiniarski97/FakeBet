using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FakeBet.API.DTO;
using FakeBet.API.Extensions;
using FakeBet.API.Helpers;
using FakeBet.API.Models;
using FakeBet.API.Repository.Interfaces;
using FakeBet.API.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FakeBet.API.Services.Implementations
{
    public class UserService : IUserService
    {
        private IUserRepository repository;

        private IMapper mapper;

        private readonly AppSettingsSecret _appSettings;

        public UserService(IUserRepository repository, IMapper mapper, IOptions<AppSettingsSecret> appSettings)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._appSettings = appSettings.Value;
        }

        public async Task RegisterUserAsync(UserAuthDTO userAuthDto)
        {
            if (string.IsNullOrEmpty(userAuthDto.Password) || userAuthDto.Password.Length < 8 ||
                string.IsNullOrWhiteSpace(userAuthDto.Email) || string.IsNullOrWhiteSpace(userAuthDto.NickName))
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
        }

        private async Task<bool> IsEmailUnique(string email)
        {
            var user = await this.repository.GetUserByEmailAsync(email);
            return user == null;
        }


        public async Task<UserDTO> LoginUserAsync(string nickname, string password)
        {
            if (string.IsNullOrEmpty(nickname) || string.IsNullOrEmpty(password))
            {
                throw new Exception("Nickname or password cannot be empty");
            }

            var user = await repository.GetUserAsync(nickname);

            if (user == null)
            {
                throw new Exception("We cannot find such user.");
            }

            if (!Authorization.VerifyPasswordHash(password, user.PasswordHash, user.Salt))
            {
                user.IncreaseFailedLoginCounter();
                await this.repository.UpdateUserAsync(user);
                //todo send mail here someday
                return null;
            }

            switch (user.Status) //todo refractor
            {
                case UserStatus.Deactivated:
                    throw new Exception("Your account is deleted. Contact administrator to retrieve it back.");
                case UserStatus.NotActivated:
                    throw new Exception("Your account is not activated. Check your email for activation link.");
                case UserStatus.Banned:
                    throw new Exception(
                        "Your account is banned. If you think you've been banned by accident contact administrator.");
            }

            user.ResetFailedLoginCounterAsync();

            var userMapped = mapper.Map<UserDTO>(user);

            userMapped.Token = GenerateUserToken(user);

            return userMapped;
        }


        public async Task<UserDTO> GetUserAsync(string nickName)
        {
            var user = await repository.GetUserAsync(nickName);
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
            var user = mapper.Map<UserAuthDTO, User>(userDto);

            if (!Authorization.VerifyPasswordHash(userDto.Password, user.PasswordHash, user.Salt))
            {
                throw new Exception("Incorrect password");
            }

            if (!await OnlyEmailChanged(user)) throw new Exception("Changed more than email");
            await this.repository.UpdateUserAsync(user);
        }

        public async Task DeleteAccountAsync(UserAuthDTO user)
        {
            var originalUser = await this.repository.GetUserAsync(user.NickName);
            if (originalUser == null)
            {
                throw new Exception("User doesn't exists");
            }

            if (!Authorization.VerifyPasswordHash(user.Password, originalUser.PasswordHash, originalUser.Salt))
            {
                throw new Exception("Wrong password");
            }

            originalUser.Status = UserStatus.Deactivated;
            await this.repository.UpdateUserAsync(originalUser);
        }

        public async Task UpdatePasswordAsync(ChangePasswordDTO model)
        {
            var user = await this.repository.GetUserAsync(model.Nickname);
            if (user == null)
            {
                throw new Exception("You must be logged!");
            }

            if (!Authorization.VerifyPasswordHash(model.CurrentPassword, user.PasswordHash, user.Salt))
            {
                throw new Exception("Wrong current password");
            }

            Authorization.CreatePasswordHash(model.NewPassword, out var passwordHash, out var passwordSalt);

            user.PasswordHash = passwordHash;
            user.Salt = passwordSalt;

            await this.repository.UpdateUserAsync(user);
        }

        private string GenerateUserToken(User user)
        {
            //todo add identity 
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.NickName),
                    new Claim(ClaimTypes.Role, user.Role.ToString()),
                    new Claim("Status", user.Status.ToString())
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private async Task<bool> OnlyEmailChanged(User user)
        {
            var userOriginal = await this.repository.GetUserAsync(user.NickName);
            return userOriginal.ArePropertiesSame(user, new[] {"Email"});
        }
    }
}