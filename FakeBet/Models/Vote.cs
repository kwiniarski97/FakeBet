

using System;
using System.ComponentModel.DataAnnotations;

namespace FakeBet.Models
{
    public class Vote
    {
        [Key]
        public Guid VoteId { get; set; }

        // FK 
        public Guid MatchId { get; set; }

        public Match Match { get; set; }

        // FK
        public Guid UserId { get; set; }

        public User User { get; set; }

        //todo zastanow sie jak to zrobic
        public int UserPick { get; set; }

        public int UserPoints { get; set; }
    }
}
