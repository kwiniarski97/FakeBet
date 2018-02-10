using System;
using System.Collections.Generic;
using System.Linq;
using FakeBet.Models;

namespace FakeBet.Repository.Interfaces
{
    public interface IMatchRepository
    {
        IQueryable<Match> Matches { get; }

        void AddNewMatch(Match match);

        Match GetMatch(Guid matchId);

        IEnumerable<Match> GetNotStartedMatches();

        void ChangeMatchStatus(Guid matchId, MatchStatus status);
    }
}