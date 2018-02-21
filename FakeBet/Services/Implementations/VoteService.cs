namespace FakeBet.Services.Implementations
{
    using System.Threading.Tasks;

    using AutoMapper;

    using FakeBet.DTO;
    using FakeBet.Models;
    using FakeBet.Repository.Interfaces;
    using FakeBet.Services.Interfaces;

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
            var vote = this.mapper.Map<Vote>(voteDto);
            await this.repository.AddVoteAsync(vote);
        }
    }
}