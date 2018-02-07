using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FakeBet.Models;

namespace FakeBet.Repository.Interfaces
{
    public interface IVoteRepository
    {
        IEnumerable<Vote> GetUserVotes(Guid userId);
    }
}