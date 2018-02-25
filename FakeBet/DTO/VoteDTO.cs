using System;
using System.ComponentModel.DataAnnotations;
using FakeBet.Models;

namespace FakeBet.DTO
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