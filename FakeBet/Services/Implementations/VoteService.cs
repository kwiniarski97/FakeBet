using System.Threading.Tasks;
using AutoMapper;
using FakeBet.DTO;
using FakeBet.Models;
using FakeBet.Repository.Interfaces;
using FakeBet.Services.Interfaces;

namespace FakeBet.Services.Implementations
{
    public class VoteService : IVoteService
    {
        private IVoteRepository repository;

        private IMapper mapper;

        public VoteService(IVoteRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task AddVoteAsync(VoteAddDTO voteDto)
        {
            var vote = mapper.Map<Vote>(voteDto);
            await repository.AddVoteAsync(vote);
        }
    }
}