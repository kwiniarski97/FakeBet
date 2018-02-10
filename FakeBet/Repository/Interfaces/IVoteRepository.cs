using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBet.Models;

namespace FakeBet.Repository.Interfaces
{
    public interface IVoteRepository
    {
        IQueryable<Vote> Votes { get; }

        IEnumerable<Vote> GetUserVotes(Guid userId);
    }
}