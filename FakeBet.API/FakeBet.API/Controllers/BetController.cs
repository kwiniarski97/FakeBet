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
        private IBetService service;

        public BetController(IBetService service)
        {
            this.service = service;
        }

        [Authorize]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> Get(ulong id)
        {
            var bet = await service.GetBetByIdAsync(id);
            return Ok(bet);
        }

        [Authorize]
        [HttpPost("[action]")]
        public async Task<IActionResult> Add([FromBody] BetDTO bet) 
        {
            try
            {
                await service.AddBetAsync(bet);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}