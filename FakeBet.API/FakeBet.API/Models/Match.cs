using System;
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


    public class Match
    {
        // teamAName+teamBName+Category+dateTime.Ticks
        [Key] public string MatchId { get; set; }

        public string Category { get; set; }

        public DateTime MatchTime { get; set; }

        public MatchStatus? Status { get; set; }

        public string TeamAName { get; set; }

        public int TeamAPoints { get; set; }
        
        [MaxLength(2)]
        public string TeamANationalityCode { get; set; }

        public string TeamBName { get; set; }

        public int TeamBPoints { get; set; }
        
        [MaxLength(2)]
        public string TeamBNationalityCode { get; set; }

        public float PointsRatio { get; set; }

        public IEnumerable<Bet> Votes { get; set; }

        public void GenerateDefaultValues()
        {
            MatchId = TeamAName.RemoveAllSpaces() + TeamBName.RemoveAllSpaces() +
                      Category +
                      MatchTime.Ticks;
            Status = DetermineMatchStatus(MatchTime);
            TeamAPoints = 0;
            TeamBPoints = 0;
            PointsRatio = 1;
            Votes = new List<Bet>();
        }

        private static MatchStatus DetermineMatchStatus(DateTime date)
        {
            return DateTime.Compare(date, DateTime.Now) > 0 ? MatchStatus.NonStarted : MatchStatus.Ended;
        }
    }
}