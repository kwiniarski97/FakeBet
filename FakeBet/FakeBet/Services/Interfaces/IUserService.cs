using System.Collections.Generic;
using System.Threading.Tasks;
using FakeBet.DTO;
using FakeBet.Models;

namespace FakeBet.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterUserAsync(UserAuthDto userAuthDto);

        Task<UserDTO> LoginUserAsync(string login, string password);

        Task<UserDTO> GetUserAsync(string nickName);

        Task ChangeUserStatusAsync(string nickName, UserStatus status);

        Task<List<UserTopDTO>> Get20BestUsersAsync();
    }
}