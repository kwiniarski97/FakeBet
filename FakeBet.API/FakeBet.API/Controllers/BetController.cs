using System;
using System.Threading.Tasks;
using FakeBet.API.DTO;
using FakeBet.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace FakeBet.API.Controllers
{
    [Route("api/[controller]")]
    public class BetController : Controller
    {
        private IBetService _betService;

        private IMatchService _matchService;

        private IUserService _userService;

        private static readonly Logger Log = LogManager.GetCurrentClassLogger();


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
                Log.Info(
                    $"Bet placed, user: {bet.UserId}, bet on team A:{bet.BetOnTeamA}, bet on team B:{bet.BetOnTeamB}");
                return Ok();
            }
            catch (Exception ex)
            {
                Log.Error($"{bet.UserId} - {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}