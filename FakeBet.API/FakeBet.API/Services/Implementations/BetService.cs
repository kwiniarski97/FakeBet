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

        public async Task<BetDTO> GetVoteByIdAsync(Guid id)
        {
            var vote = await repository.GetVoteByIdAsync(id);
            var voteDTO = mapper.Map<BetDTO>(vote);
            return voteDTO;
        }

        public async Task AddVoteAsync(BetDTO betDTO)
        {
            var voteFromRepo = await GetVoteByIdAsync(betDTO.BetId);
            if (voteFromRepo != null)
            {
                throw new Exception("Bets already found");
            }
            var vote = mapper.Map<Bet>(betDTO);
            vote.BetId = new Guid();
            await repository.AddVoteAsync(vote);
        }
    }
}