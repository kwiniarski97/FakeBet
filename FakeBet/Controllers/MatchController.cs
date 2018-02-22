using System.Collections.Generic;
using System.Threading.Tasks;
using FakeBet.DTO;
using FakeBet.Models;
using FakeBet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FakeBet.Controllers
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
        public async Task<Match> Get(string matchId)
        {
            return await service.GetMatchAsync(matchId);
        }

        [HttpPost("[action]")]
        public async Task Add([FromBody] MatchAddDTO match)
        {
            await service.AddNewMatchAsync(match);
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<Match>> GetNotStartedMatches()
        {
            return await service.GetNotStartedMatchesAsync();
        }

        [HttpPut("[action]/{matchId}")]
        public async Task ChangeMatchStatus(string matchId, [FromBody] MatchStatus status)
        {
            await service.ChangeMatchStatusAsync(matchId, status);
        }
    }
}