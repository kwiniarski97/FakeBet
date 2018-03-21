using System;
using System.Threading.Tasks;
using AutoMapper;
using FakeBet.API.DTO;
using FakeBet.API.Models;
using FakeBet.API.Repository.Interfaces;
using FakeBet.API.Services.Interfaces;

namespace FakeBet.API.Services.Implementations
{
    public class BetService : IBetService
    {
        private IBetRepository repository;

        private IMapper mapper;

        public BetService(IBetRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<BetDTO> GetBetByIdAsync(ulong id)
        {
            var bet = await repository.GetBetByIdAsync(id);
            var betDTO = mapper.Map<BetDTO>(bet);
            return betDTO;
        }

        public async Task AddBetAsync(BetDTO betDTO)
        {
            var bet = mapper.Map<Bet>(betDTO);
            await repository.AddBetAsync(bet);
        }
    }
}