using System;
using System.Threading.Tasks;
using FakeBet.API.DTO;
using FakeBet.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FakeBet.API.Controllers
{
    [Route("api/[controller]")]
    public class BetController : Controller
    {
        private IBetService _betService;

        private IMatchService _matchService;

        private IUserService _userService;


        public BetController(IBetService betService, IMatchService matchService, IUserService userService)
        {
            this._betService = betService;
            this._matchService = matchService;
            this._userService = userService;
        }

        [Authorize("StatusActive")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get(ulong id)
        {
            var bet = await _betService.GetBetByIdAsync(id);
            return Ok(bet);
        }

        [Authorize("StatusActive")]
        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody] BetDTO bet)
        {
            try
            {
                await _matchService.UpdateMatchWithNewBetAsync(bet);
                await _userService.UserPlacedBet(bet);
                await _betService.AddBetAsync(bet);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}