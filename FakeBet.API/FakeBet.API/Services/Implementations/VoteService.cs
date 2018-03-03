using System;
using System.Threading.Tasks;
using AutoMapper;
using FakeBet.API.DTO;
using FakeBet.API.Models;
using FakeBet.API.Repository.Interfaces;
using FakeBet.API.Services.Interfaces;

namespace FakeBet.API.Services.Implementations
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

        public async Task<VoteDTO> GetVoteByIdAsync(Guid id)
        {
            var vote = await repository.GetVoteByIdAsync(id);
            var voteDTO = mapper.Map<VoteDTO>(vote);
            return voteDTO;
        }

        public async Task AddVoteAsync(VoteDTO voteDto)
        {
            var voteFromRepo = await GetVoteByIdAsync(voteDto.MatchId);
            if (voteFromRepo != null)
            {
                throw new Exception("Vote already found");
            }
            var vote = mapper.Map<Vote>(voteDto);
            vote.VoteId = new Guid();
            await repository.AddVoteAsync(vote);
        }
    }
}