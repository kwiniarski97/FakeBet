namespace FakeBet.DTO
{
    using System;
    using System.Collections.Generic;

    using FakeBet.Models;

    public class UserDto
    {
        public string NickName { get; set; }

        public string Email { get; set; }

        public DateTime CreateTime { get; set; }

        public int Points { get; set; }

        public IEnumerable<Vote> VotesHistory { get; set; }

        public UserStatus Status { get; set; }
    }
}