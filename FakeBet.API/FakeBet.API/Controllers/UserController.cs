using System;
using System.Threading.Tasks;
using FakeBet.API.DTO;
using FakeBet.API.Models;
using FakeBet.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FakeBet.API.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize("StatusActive")]
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


        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] UserAuthDTO userDto)
        {
            try
            {
                var user = await _userService.LoginUserAsync(userDto.NickName, userDto.Password);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
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

        [HttpGet("[action]")]
        public async Task<IActionResult> Top20()
        {
            var top = await _userService.Get20BestUsersAsync();
            return Ok(top);
        }

        [Authorize(Roles = "User")]
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateEmail([FromBody] UserAuthDTO model)
        {
            await this._userService.UpdateEmailAsync(model);
            return Ok(); // todo dodaj exceptions itd.
        }

        [Authorize(Roles = "User")]
        [HttpPut("[action]")]
        public async Task<IActionResult> DeleteAccount([FromBody] UserAuthDTO user)
        {
            try
            {
                await this._userService.DeleteAccountAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("[action]")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
        {
            try
            {
                await this._userService.UpdatePasswordAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}