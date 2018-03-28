using System;
using FakeBet.API.Models;

namespace FakeBet.API.DTO
{
    public class MatchDTO
    {
        public string MatchId { get; set; }

        public string Category { get; set; }

        public DateTime MatchTime { get; set; }

        public MatchStatus Status { get; set; }

        public string TeamAName { get; set; }

        public int TeamAPoints { get; set; }

        public string TeamANationalityCode { get; set; }

        public string TeamBName { get; set; }

        public int TeamBPoints { get; set; }

        public string TeamBNationalityCode { get; set; }
        
        public Team Winner { get; set; }
    }
}