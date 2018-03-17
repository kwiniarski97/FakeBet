using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FakeBet.API.DTO;
using FakeBet.API.Helpers;
using FakeBet.API.Models;
using FakeBet.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FakeBet.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{nickName}")]
        public async Task<IActionResult> Get(string nickName)
        {
            var user = await _userService.GetUserAsync(nickName);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody] UserAuthDTO user)
        {
            try
            {
                await _userService.RegisterUserAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] UserAuthDTO userDto)
        {
            var user = await _userService.LoginUserAsync(userDto.NickName, userDto.Password);

            if (user == null)
            {
                return BadRequest($"User with {userDto.NickName} nickname was not found");
            }

            if (user.Status != UserStatus.Active)
            {
                return BadRequest($"Your account is {user.Status.ToString()}");
            }

            //todo przenies to do serwisu
            
            return Ok(user);
        }

        [HttpPut("[action]/{nickName}")]
        public async Task<IActionResult> ChangeStatus(string nickName, [FromBody] UserStatus status)
        {
            try
            {
                await _userService.ChangeUserStatusAsync(nickName, status);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<IActionResult> Top20()
        {
            var top = await _userService.Get20BestUsersAsync();
            return Ok(top);
        }

        [HttpPost("[action]")]
        public async Task UpdateEmail([FromBody] UserDTO model)
        {
            await this._userService.UpdateEmailAsync(model);
        }
    }
}