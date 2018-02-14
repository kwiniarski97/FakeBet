namespace FakeBet.DTO
{
    using System;

    using FakeBet.Models;

    public class MatchAddDTO
    {
        public string Category { get; set; }

        public DateTime MatchTime { get; set; }

        public string TeamAName { get; set; }

        public string TeamBName { get; set; }

        public MatchStatus? Status { get; set; }
    }
}