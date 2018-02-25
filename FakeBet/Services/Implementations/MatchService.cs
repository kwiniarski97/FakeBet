using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using FakeBet.DTO;
using FakeBet.Extensions;
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

        public async Task AddNewMatchAsync(MatchDTO matchDTO)
        {
            if (matchDTO.TeamAName == null || matchDTO.TeamBName == null || matchDTO.Category == null ||
                matchDTO.MatchTime == default(DateTime))
            {
                throw new Exception("All fields must be filled.");
            }

            var match = mapper.Map<MatchDTO, Match>(matchDTO);
            match.GenerateDefaultValues();
            await repository.AddNewMatchAsync(match);
        }

        public async Task<Match> GetMatchAsync(string matchId)
        {
            var match = await repository.GetMatchAsync(matchId);
            if (match == null)
            {
                throw new Exception($"Match with given id {matchId} not found");
            }

            return match;
        }

        public async Task<IEnumerable<Match>> GetNotStartedMatchesAsync()
        {
            return await repository.GetNotStartedMatchesAsync();
        }

        public async Task ChangeMatchStatusAsync(string matchId, MatchStatus status)
        {
            await GetMatchAsync(matchId); //if null throws exception
            await repository.ChangeMatchStatusAsync(matchId, status);
        }

        }
}