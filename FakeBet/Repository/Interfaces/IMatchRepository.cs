using System;
using System.Collections.Generic;
using FakeBet.Models;

namespace FakeBet.Repository.Interfaces
{
    public interface IMatchRepository
    {
        void AddNewMatch(Match match);

        Match GetMatch(Guid matchId);

        IEnumerable<Match> GetNotStartedMatches();

        void ChangeMatchStatus(Guid matchId, MatchStatus status);
    }
}
