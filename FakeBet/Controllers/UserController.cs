namespace FakeBet.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using FakeBet.DTO;
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

        [HttpGet("{nickName}")]
        public async Task<UserDTO> Get(string nickName)
        {
            return await this.userService.GetUserAsync(nickName);
        }

        [HttpPost("[action]")]
        public async Task Register([FromBody] UserRegisterDto user)
        {
            await this.userService.RegisterUserAsync(user);
        }

        [HttpPut("[action]/{nickName}")]
        public async Task Activate(string nickName)
        {
            await this.userService.ActivateUserAsync(nickName);
        }

        [HttpPut("[action]/{nickName}")]
        public async Task Deactivate(string nickName)
        {
            await this.userService.DeactivateUserAsync(nickName);
        }

        [HttpPut("[action]/{nickName}")]
        public async Task Ban(string nickName)
        {
            await this.userService.BanUserAsync(nickName);
        }

        [HttpGet("[action]")]
        public async Task<List<UserTopDTO>> Top20()
        {
            return await this.userService.Get20BestUsersAsync();
        }
    }
}