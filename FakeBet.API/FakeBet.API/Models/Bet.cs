using System;
using System.ComponentModel.DataAnnotations;

namespace FakeBet.API.Models
{
    public class Bet
    {
        [Key] public Guid BetId { get; set; }

        // FK 
        public string MatchId { get; set; }

        public Match Match { get; set; }

        // FK
        public string UserId { get; set; }

        public User User { get; set; }

        public int BetOnTeamA { get; set; }

        public int BetOnTeamB { get; set; }
    }
}