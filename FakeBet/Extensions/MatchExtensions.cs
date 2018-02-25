using System;
using System.Collections.Generic;
using FakeBet.Models;

namespace FakeBet.Extensions
{
    public static class MatchExtensions
    {
        public static Match GenerateDefaultValues(this Match match)
        {
            var matchWithDefaults = new Match();
            ;
            matchWithDefaults.Status = match.Status ?? DetermineMatchStatus(match.MatchTime);
            matchWithDefaults.MatchId = match.TeamAName.RemoveAllSpaces() + match.TeamBName.RemoveAllSpaces() +
                                        match.Category +
                                        match.MatchTime.Ticks;
            matchWithDefaults.TeamAPoints = 0;
            matchWithDefaults.TeamBPoints = 0;
            matchWithDefaults.PointsRatio = 1;
            matchWithDefaults.Votes = new List<Vote>();
            return matchWithDefaults;
        }

        private static MatchStatus DetermineMatchStatus(DateTime date)
        {
            return DateTime.Compare(date, DateTime.Now) > 0 ? MatchStatus.NonStarted : MatchStatus.Ended;
        }
    }
}