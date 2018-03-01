using System;
using System.Threading.Tasks;
using FakeBet.API.DTO;
using FakeBet.API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FakeBet.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class VoteController : Controller
    {
        private IVoteService service;

        public VoteController(IVoteService service)
        {
            this.service = service;
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var vote = await service.GetVoteByIdAsync(id);
            return Ok(vote);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody] VoteDTO vote)
        {
            try
            {
                await service.AddVoteAsync(vote);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}