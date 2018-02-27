using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FakeBet.Extensions;

namespace FakeBet.Models
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

        public string TeamBName { get; set; }

        public int TeamBPoints { get; set; }

        public float PointsRatio { get; set; }

        public IEnumerable<Vote> Votes { get; set; }

        public void GenerateDefaultValues()
        {
            MatchId = TeamAName.RemoveAllSpaces() + TeamBName.RemoveAllSpaces() +
                      Category +
                      MatchTime.Ticks;
            Status = DetermineMatchStatus(MatchTime);
            TeamAPoints = 0;
            TeamBPoints = 0;
            PointsRatio = 1;
            Votes = new List<Vote>();
        }

        private static MatchStatus DetermineMatchStatus(DateTime date)
        {
            return DateTime.Compare(date, DateTime.Now) > 0 ? MatchStatus.NonStarted : MatchStatus.Ended;
        }
    }
}