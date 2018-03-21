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
    public class MatchController : Controller
    {
        private IMatchService service;

        public MatchController(IMatchService service)
        {
            this.service = service;
        }

        [HttpGet("{matchId}")]
        public async Task<IActionResult> Get(string matchId)
        {
            try
            {
                var match = await service.GetMatchAsync(matchId);
                return Ok(match);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
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

        [HttpGet("[action]")]
        public async Task<IActionResult> GetNotStarted()
        {
            var nonStartedMatches = await service.GetNotStartedMatchesAsync();
            return Ok(nonStartedMatches);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("[action]/{matchId}")]
        public async Task<IActionResult> ChangeStatus(string matchId, [FromBody] MatchStatus status)
        {
            await service.ChangeMatchStatusAsync(matchId, status);
            return Ok();
        }
    }
}