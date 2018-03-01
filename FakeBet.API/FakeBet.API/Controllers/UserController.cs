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

        private readonly AppSettingsSecret _appSettings;

        public UserController(IUserService userService, IOptions<AppSettingsSecret> appSettings)
        {
            _userService = userService;
            _appSettings = appSettings.Value;
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
        public async Task<IActionResult> Register([FromBody] UserAuthDto user)
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
        public async Task<IActionResult> Login([FromBody]UserAuthDto userDto)
        {
            var user = await _userService.LoginUserAsync(userDto.NickName, userDto.Password);

            if (user == null)
            {
                return Unauthorized();
            }


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.NickName)
                }),
                Expires = DateTime.Now.AddDays(3),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
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
    }
}