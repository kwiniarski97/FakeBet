using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FakeBet.Models
{
    public class Vote
    {
        public Guid VoteId { get; set; }

        public Guid MatchId { get; set; }

        public Guid UserId { get; set; }

        //todo zastanow sie jak to zrobic
        public int UserPick { get; set; }

        public int UserPoints { get; set; }
    }
}
