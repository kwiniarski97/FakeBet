﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FakeBet.API.DTO;
using FakeBet.API.Helpers;
using FakeBet.API.Models;
using FakeBet.API.Repository.Interfaces;
using FakeBet.API.Services.Interfaces;

namespace FakeBet.API.Services.Implementations
{
    public class MatchService : IMatchService
    {
        private IMatchRepository repository;

        private IMapper mapper;

        private IPrizeCalculator _prizeCalculator;

        public MatchService(IMatchRepository repository, IMapper mapper, IPrizeCalculator calculator)
        {
            this.repository = repository;
            this.mapper = mapper;
            this._prizeCalculator = calculator;
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
            if (await repository.GetMatchAsync(match.MatchId) != null)
            {
                throw new Exception("Match already exists");
            }

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


        public async Task EndMatchAsync(string matchId, Team winner)
        {
            var match = await GetMatchAsync(matchId);
            if (match == null)
            {
                throw new Exception($"Match with given id {matchId} not found");
            }


            await this._prizeCalculator.CalculateAsync(match, winner);

            match.Winner = winner;
            match.Status = MatchStatus.Ended;

            await this.repository.EndMatchAsync(match);
        }

        public async Task<IEnumerable<MatchDTO>> GetAllAsync()
        {
            var matches = await this.repository.GetAllAsync();
            var matchesDTO = this.mapper.Map<IEnumerable<MatchDTO>>(matches);
            return matchesDTO;
        }

        public async Task UpdateMatchAsync(MatchDTO matchDTO)
        {
            var match = this.mapper.Map<Match>(matchDTO);
            await this.repository.UpdateMatchAsync(match);
        }
    }
}