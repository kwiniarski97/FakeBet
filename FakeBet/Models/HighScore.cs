using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FakeBet.Models
{
    public class HighScore
    {
        [Key]
        public long TickOfUpdate { get; set; }
        private List<User> TopYear { get; set; }
        private List<User> TopMonth { get; set; }
        private List<User> TopWeek { get; set; }
        private List<User> TopDay { get; set; }

    }
}