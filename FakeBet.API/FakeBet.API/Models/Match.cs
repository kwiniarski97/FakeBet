﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FakeBet.API.Extensions;

namespace FakeBet.API.Models
{
    public enum MatchStatus
    {
        NonStarted,
        OnGoing,
        Ended
    }

    public enum Team
    {
        A,
        B
    }


    public class Match
    {
        // teamAName+teamBName+Category+dateTime.Ticks
        [Key] public string MatchId { get; set; }

        public string Category { get; set; }

        public DateTime MatchTime { get; set; }

        public MatchStatus? Status { get; set; }

        public string TeamAName { get; set; }

        public int TeamAPoints { get; set; } = 0;

        [MaxLength(2)] public string TeamANationalityCode { get; set; }

        public string TeamBName { get; set; }

        public int TeamBPoints { get; set; } = 0;

        [MaxLength(2)] public string TeamBNationalityCode { get; set; }


        public Team Winner { get; set; }
        
        public List<Bet> Bets { get; set; }


        public void GenerateDefaultValues()
        {
            MatchId = TeamAName.RemoveAllSpaces() + TeamBName.RemoveAllSpaces() +
                      Category +
                      MatchTime.Ticks;
            Status = DetermineMatchStatus(MatchTime);
            Bets = new List<Bet>();
        }

        private static MatchStatus DetermineMatchStatus(DateTime date)
        {
            return DateTime.Compare(date, DateTime.Now) > 0 ? MatchStatus.NonStarted : MatchStatus.Ended;
        }
    }
}