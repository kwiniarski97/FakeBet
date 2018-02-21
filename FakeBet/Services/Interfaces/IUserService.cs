namespace FakeBet.Services.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FakeBet.DTO;
    using FakeBet.Models;

    public interface IUserService
    {
        Task RegisterUserAsync(UserRegisterDto userRegisterDto);

        Task<UserDTO> GetUserAsync(string nickName);

        Task ActivateUserAsync(string nickName);

        Task DeactivateUserAsync(string nickName);

        Task BanUserAsync(string nickName);

        Task<List<UserTopDTO>> Get20BestUsersAsync();
    }
}