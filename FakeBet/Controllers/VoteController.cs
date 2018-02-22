using System.Threading.Tasks;
using FakeBet.DTO;
using FakeBet.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FakeBet.Controllers
{
    [Route("api/[controller]")]
    public class VoteController : Controller
    {
        private IVoteService service;

        public VoteController(IVoteService service)
        {
            this.service = service;
        }

        [HttpPost("[action]")]
        public async Task Add([FromBody] VoteAddDTO vote)
        {
            await service.AddVoteAsync(vote);
        }
    }
}