using System;
using System.Collections.Generic;
using FakeBet.Models;

namespace FakeBet.DTO
{
    public class UserDTO
    {
        public string NickName { get; set; }

        public string Email { get; set; }

        public DateTime CreateTime { get; set; }

        public int Points { get; set; }

        public IEnumerable<Vote> Votes { get; set; }

        public UserStatus Status { get; set; }
        
        public string Token { get; set; }
    }
}