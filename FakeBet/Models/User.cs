using System;
using System.Collections.Generic;

namespace FakeBet.Models
{
    public class User
    {
        public Guid UserId { get; set; }

        public string NickName { get; set; }

        //todo ONLY FOR NOW
        public string Password { get; set; }

        public string Salt { get; set; }

        public DateTime CreateTime { get; set; }

        public int Points { get; set; }

        public IEnumerable<Vote> VotesHistory { get; set; }
    }
}