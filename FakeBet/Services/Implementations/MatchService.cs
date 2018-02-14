namespace FakeBet.Services.Implementations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using FakeBet.DTO;
    using FakeBet.Models;
    using FakeBet.Repository.Interfaces;
    using FakeBet.Services.Interfaces;

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
            var match = this.mapper.Map<MatchAddDTO, Match>(matchAddDTO);
            await this.repository.AddNewMatchAsync(match);
        }

        public async Task<Match> GetMatchAsync(string matchId)
        {
            return await this.repository.GetMatchAsync(matchId);
        }

        public async Task<IEnumerable<Match>> GetNotStartedMatchesAsync()
        {
            return await this.repository.GetNotStartedMatchesAsync();
        }

        public async Task ChangeMatchStatusAsync(string matchId, MatchStatus status)
        {
            await this.repository.ChangeMatchStatusAsync(matchId, status);
        }
    }
}