namespace FakeBet.Controllers
{
    using System.Net;
    using System.Threading.Tasks;

    using FakeBet.DTO;
    using FakeBet.Models;
    using FakeBet.Services.Interfaces;

    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("[action]/{nickName}")]
        public async Task<UserDto> GetUser(string nickName)
        {
            return await this.userService.GetUserAsync(nickName);
        }

        [HttpPost("[action]")]
        public async Task RegisterUser([FromBody] UserRegisterDto user)
        {
            await this.userService.RegisterUserAsync(user);
        }

        [HttpPut("[action]/{nickName}")]
        public async Task ActivateUser(string nickName)
        {
            await this.userService.ActivateUserAsync(nickName);
        }

        [HttpPut("[action]/{nickName}")]
        public async Task DeactivateUser(string nickName)
        {
            await this.userService.DeactivateUserAsync(nickName);
        }

        [HttpPut("[action]/{nickName}")]
        public async Task BanUser(string nickName)
        {
            await this.userService.BanUserAsync(nickName);
        }
    }
}