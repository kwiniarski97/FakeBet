using System;
using FakeBet.API.Models;

namespace FakeBet.API.DTO
{
    public class BetDTO
    {
        public ulong Id { get; set; }

        public string MatchId { get; set; }

        public string UserId { get; set; }

        public int BetOnTeamA { get; set; }

        public int BetOnTeamB { get; set; }
    }
}