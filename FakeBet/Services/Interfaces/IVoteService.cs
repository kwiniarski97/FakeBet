using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeBet.Services.Interfaces
{
    using FakeBet.DTO;

    public interface IVoteService
    {
        Task AddVoteAsync(VoteAddDTO voteDto);
    }
}
