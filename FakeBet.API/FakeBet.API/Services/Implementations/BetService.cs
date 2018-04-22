using System;
using System.Collections.Generic;
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
        private IBetRepository _betRepository;

        private IUserRepository _userRepository;

        private IMatchRepository _matchRepository;

        private IMapper mapper;

        public BetService(IBetRepository betRepository, IUserRepository userRepository,
            IMatchRepository matchRepository,
            IMapper mapper)
        {
            _betRepository = betRepository;
            _userRepository = userRepository;
            _matchRepository = matchRepository;
            this.mapper = mapper;
        }

        public async Task<BetDTO> GetBetByIdAsync(ulong id)
        {
            var bet = await _betRepository.GetBetByIdAsync(id);
            var betDTO = mapper.Map<BetDTO>(bet);
            return betDTO;
        }

        // todo refractor into smaller functions
        public async Task AddBetAsync(BetDTO betDTO)
        {
            if (betDTO == null)
            {
                return;
            }

            var bet = mapper.Map<Bet>(betDTO);
            bet.DateOfBetting = DateTime.UtcNow;

            var user = await this._userRepository.GetUserWithoutBetsAsync(bet.UserId);

            if (user == null)
            {
                throw new Exception("No userDTO with given Id");
            }

            user.Points = user.Points - (bet.BetOnTeamA + bet.BetOnTeamB);
            if (user.Points < 0)
            {
                throw new Exception("You don't have enough points!");
            }

            var match = await this._matchRepository.GetMatchAsync(bet.MatchId);
            if (match == null)
            {
                throw new Exception($"Match with given id {bet.MatchId} not found");
            }

            if (match.MatchTime.CompareTo(DateTime.UtcNow) <= 0)
            {
                match.Status = MatchStatus.OnGoing;
                await this._matchRepository.UpdateMatchAsync(match);
                throw new Exception("Match already started.");
            }

            match.TeamAPoints += bet.BetOnTeamA;
            match.TeamBPoints += bet.BetOnTeamB;

            await _matchRepository.UpdateMatchAsync(match);
            await _userRepository.UpdateUserAsync(user);
            await _betRepository.AddBetAsync(bet);
        }

        public async Task<IEnumerable<Bet>> GetWinnersBetsByMatchIdAsync(string matchId, Team winner)
        {
            return await this._betRepository.GetWinnersByMatchIdAsync(matchId, winner);
        }


        private async Task<IEnumerable<Bet>> GetBetsByMatchIdAsync(string matchId)
        {
            return await this._betRepository.GetBetsByMatchIdAsync(matchId);
        }
    }
}