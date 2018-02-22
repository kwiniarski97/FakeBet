using System;
using FakeBet.Models;

namespace FakeBet.DTO
{
    public class MatchAddDTO
    {
        public string Category { get; set; }

        public DateTime MatchTime { get; set; }

        public string TeamAName { get; set; }

        public string TeamBName { get; set; }

        public MatchStatus? Status { get; set; }
    }
}