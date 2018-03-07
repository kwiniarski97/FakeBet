using System;
using System.Threading.Tasks;
using FakeBet.API.DTO;
using FakeBet.API.Models;
using FakeBet.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FakeBet.API.Controllers
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
        
        [AllowAnonymous]
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
        public async Task<IActionResult> GetNotStarted()
        {
            var nonStartedMatches = await service.GetNotStartedMatchesAsync();
            return Ok(nonStartedMatches);
        }

        [HttpPut("[action]/{matchId}")]
        public async Task<IActionResult> ChangeStatus(string matchId, [FromBody] MatchStatus status)
        {
            await service.ChangeMatchStatusAsync(matchId, status);
            return Ok();
        }
    }
}