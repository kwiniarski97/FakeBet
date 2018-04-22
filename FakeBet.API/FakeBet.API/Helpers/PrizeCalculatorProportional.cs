using System.Threading.Tasks;
using FakeBet.API.Models;
using FakeBet.API.Services.Interfaces;

namespace FakeBet.API.Helpers
{
    public class PrizeCalculatorProportional : IPrizeCalculator
    {
        private IUserService _userService;

        private IBetService _betService;

        public PrizeCalculatorProportional(IUserService userService, IBetService betService)
        {
            _userService = userService;
            _betService = betService;
        }

        public async Task CalculateAsync(Match match, Team winner)
        {
            var bets = await this._betService.GetWinnersBetsByMatchIdAsync(match.MatchId, winner);

            var totalPoints = match.TeamAPoints + match.TeamBPoints;

            var winnersTeamPoints = winner == Team.A ? match.TeamAPoints : match.TeamBPoints;

            foreach (var bet in bets)
            {
                var betPoints = bet.BetOnTeamA > 0 ? bet.BetOnTeamA : bet.BetOnTeamB;
                var ratio = betPoints / (double) winnersTeamPoints;
                var wonPoints = ratio * totalPoints;
                await this._userService.AddUserPointsAsync(bet.UserId, (int) wonPoints);
            }
        }
    }
}