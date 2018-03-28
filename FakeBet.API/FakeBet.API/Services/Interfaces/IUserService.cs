using System.Collections.Generic;
using System.Threading.Tasks;
using FakeBet.API.DTO;
using FakeBet.API.Models;

namespace FakeBet.API.Services.Interfaces
{
    public interface IUserService
    {
        Task RegisterUserAsync(UserAuthDTO userAuthDto);

        Task<UserDTO> LoginUserAsync(string login, string password);

        Task<UserDTO> GetUserAsync(string nickName);

        Task ChangeUserStatusAsync(string nickName, UserStatus status);

        Task<List<UserTopDTO>> Get20BestUsersAsync();

        Task UpdateEmailAsync(UserAuthDTO user);
        
        Task DeleteAccountAsync(UserAuthDTO user);
        
        Task UpdatePasswordAsync(ChangePasswordDTO model);
        Task AddUserPointsAsync(string betUserId, int wonPoints);
    }
}