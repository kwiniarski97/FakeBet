using System;
using System.Net.Http;
using System.Threading.Tasks;
using FakeBet.API.DTO;
using FakeBet.API.Models;
using FakeBet.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FakeBet.API.Controllers
{
    [Route("api/[controller]")]
    public class MatchController : Controller
    {
        private IMatchService _matchService;

        private IBetService _betService;

        public MatchController(IMatchService matchService)
        {
            this._matchService = matchService;
        }

        [Authorize("StatusActive")]
        [HttpGet("{matchId}")]
        public async Task<IActionResult> Get(string matchId)
        {
            try
            {
                var match = await _matchService.GetMatchAsync(matchId);
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
                await _matchService.AddNewMatchAsync(match);
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
            var nonStartedMatches = await _matchService.GetNotStartedMatchesAsync();
            return Ok(nonStartedMatches);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("[action]/{matchId}")]
        public async Task<IActionResult> End([FromBody] MatchDTO value, string matchId)
        {
            try{
                await this._matchService.EndMatchAsync(matchId, value.Winner);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var matches = await this._matchService.GetAllAsync();
            return Ok(matches);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] MatchDTO matchDTO)
        {
            try
            {
                await this._matchService.UpdateMatchAsync(matchDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}