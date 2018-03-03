using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FakeBet.API.DTO;
using FakeBet.API.Helpers;
using FakeBet.API.Models;
using FakeBet.API.Repository.Interfaces;
using FakeBet.API.Services.Interfaces;

namespace FakeBet.API.Services.Implementations
{
    public class UserService : IUserService
    {
        private IUserRepository repository;

        private IMapper mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
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

            byte[] passwordHash, passwordSalt;
            Encryption.CreatePasswordHash(userAuthDto.Password, out passwordHash, out passwordSalt);

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

            if (!Encryption.VerifyPasswordHash(password, user.PasswordHash, user.Salt))
            {
                return null;
            }

            var userMapped = mapper.Map<UserDTO>(user);
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
    }
}