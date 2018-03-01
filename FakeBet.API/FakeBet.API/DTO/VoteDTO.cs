using System;

namespace FakeBet.API.DTO
{
    public class VoteDTO
    {
        public Guid VoteId { get; set; }

        public Guid MatchId { get; set; }
        
        public string UserId { get; set; }

        public int UserPick { get; set; }

        public int UserPoints { get; set; }
    }
}