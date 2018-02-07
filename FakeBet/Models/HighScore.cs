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
        private IEnumerable<User> TopYear { get; set; }
        private IEnumerable<User> TopMonth { get; set; }
        private IEnumerable<User> TopWeek { get; set; }
        private IEnumerable<User> TopDay { get; set; }

    }
}