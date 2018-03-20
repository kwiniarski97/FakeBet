using System;
using System.Collections.Generic;
using FakeBet.API.Models;

namespace FakeBet.API.DTO
{
    public class UserDTO
    {
        public string NickName { get; set; }

        public string Email { get; set; }

        public DateTime CreateTime { get; set; }

        public int Points { get; set; }

        public IEnumerable<Bet> Bets { get; set; }

        public UserStatus Status { get; set; }

        public string Token { get; set; }

        public UserRole Role { get; set; }
    }
}