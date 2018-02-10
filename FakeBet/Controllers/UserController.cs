namespace FakeBet.Controllers
{
    using System.Threading.Tasks;

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
        public async Task<User> GetUser(string nickName)
        {
            return await this.userService.GetUserAsync(nickName);
        }

//        [HttpPost()]

    }
}