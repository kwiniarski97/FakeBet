using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

        public async Task RegisterUserAsync(UserAuthDto userAuthDto)
        {
            if (string.IsNullOrEmpty(userAuthDto.Password) || userAuthDto.Password.Length < 8)
            {
                throw new Exception("Password is invalid");
            }

            if (await GetUserAsync(userAuthDto.NickName) != null)
            {
                throw new Exception("Username is already taken");
            }

            Authorization.CreatePasswordHash(userAuthDto.Password, out var passwordHash, out var passwordSalt);

            var user = mapper.Map<UserAuthDto, User>(userAuthDto);
            user.PasswordHash = passwordHash;
            user.Salt = passwordSalt;

            await repository.RegisterUserAsync(user);
        }

        public async Task<UserDTO> LoginUserAsync(string nickname, string password)
        {
            if (string.IsNullOrEmpty(nickname) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = await repository.GetUserAsync(nickname);

            if (user == null)
            {
                return null;
            }

            if (!Authorization.VerifyPasswordHash(password, user.PasswordHash, user.Salt))
            {
                user.IncreaseFailedLoginCounter();
                await this.repository.UpdateUserAsync(user);
                //todo send mail here someday
                return null;
            }

            user.ResetFailedLoginCounterAsync();

            var userMapped = mapper.Map<UserDTO>(user);

            userMapped.Token = GenerateUserToken(user.NickName);

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

        public async Task UpdateEmailAsync(UserDTO userDto)
        {
            var user = mapper.Map<UserDTO, User>(userDto);
            if (await OnlyEmailChanged(user))
            {
                await this.repository.UpdateUserAsync(user);
            }
        }

        private string GenerateUserToken(string nickname)
        {
            //todo add identity 
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, nickname)
                }),
                Expires = DateTime.Now.AddDays(3),
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