namespace FakeBet.Services.Interfaces
{
    using System;
    using System.Threading.Tasks;

    using FakeBet.DTO;
    using FakeBet.Models;

    public interface IUserService
    {
        Task RegisterUserAsync(UserRegisterDto userRegisterDto);

        Task<UserDto> GetUserAsync(string nickName);

        Task ActivateUserAsync(string nickName);

        Task DeactivateUserAsync(string nickName);

        Task BanUserAsync(string nickName);
    }
}