using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using FakeBet.DTO;
using FakeBet.Models;
using FakeBet.Repository.Interfaces;
using FakeBet.Services.Interfaces;

namespace FakeBet.Services.Implementations
{
    public class MatchService : IMatchService
    {
        private IMatchRepository repository;

        private IMapper mapper;

        public MatchService(IMatchRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task AddNewMatchAsync(MatchAddDTO matchAddDTO)
        {
            var match = mapper.Map<MatchAddDTO, Match>(matchAddDTO);
            await repository.AddNewMatchAsync(match);
        }

        public async Task<Match> GetMatchAsync(string matchId)
        {
            return await repository.GetMatchAsync(matchId);
        }

        public async Task<IEnumerable<Match>> GetNotStartedMatchesAsync()
        {
            return await repository.GetNotStartedMatchesAsync();
        }

        public async Task ChangeMatchStatusAsync(string matchId, MatchStatus status)
        {
            await repository.ChangeMatchStatusAsync(matchId, status);
        }
    }
}