using System;
using System.Threading.Tasks;
using FakeBet.DTO;
using FakeBet.Models;
using FakeBet.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FakeBet.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class MatchController : Controller
    {
        private IMatchService service;

        public MatchController(IMatchService service)
        {
            this.service = service;
        }

        [AllowAnonymous]
        [HttpGet("{matchId}")]
        public async Task<IActionResult> Get(string matchId)
        {
            var match = await service.GetMatchAsync(matchId);
            return Ok(match);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody] MatchDTO match)
        {
            try
            {
                await service.AddNewMatchAsync(match);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetNotStartedMatches()
        {
            var nonStartedMatches = await service.GetNotStartedMatchesAsync();
            return Ok(nonStartedMatches);
        }

        [HttpPut("[action]/{matchId}")]
        public async Task<IActionResult> ChangeMatchStatus(string matchId, [FromBody] MatchStatus status)
        {
            await service.ChangeMatchStatusAsync(matchId, status);
            return Ok();
        }
    }
}