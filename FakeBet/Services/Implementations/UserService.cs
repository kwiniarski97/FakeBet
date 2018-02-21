namespace FakeBet.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using FakeBet.DTO;
    using FakeBet.Models;
    using FakeBet.Repository.Interfaces;
    using FakeBet.Services.Interfaces;

    public class UserService : IUserService
    {
        private IUserRepository repository;

        private IMapper mapper;

        public UserService(IUserRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task RegisterUserAsync(UserRegisterDto userRegisterDto)
        {
            var user = this.mapper.Map<UserRegisterDto, User>(userRegisterDto);

            await this.repository.RegisterUserAsync(user);
        }

        public async Task<UserDTO> GetUserAsync(string nickName)
        {
            var user = await this.repository.GetUserAsync(nickName);
            return this.mapper.Map<User, UserDTO>(user);
        }

        public async Task ActivateUserAsync(string nickName)
        {
            await this.repository.ActivateUserAsync(nickName);
        }

        public async Task DeactivateUserAsync(string nickName)
        {
            await this.repository.DeactivateUserAsync(nickName);
        }

        public async Task BanUserAsync(string nickName)
        {
            await this.repository.BanUserAsync(nickName);
        }

        public async Task<List<UserTopDTO>> Get20BestUsersAsync()
        {
            var users = await this.repository.Get20BestUsersAsync();
            var userTopDtos = this.mapper.Map<List<UserTopDTO>>(users);
            return userTopDtos;
        }
    }
}