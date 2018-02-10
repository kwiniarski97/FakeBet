﻿namespace FakeBet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public enum UserStatus
    {
        Active,

        NotActivated,

        Deactivated,

        Banned
    }

    public class User
    {
        [Key]
        public string NickName { get; set; }

        public string Email { get; set; }

        // todo ONLY FOR NOW
        public string Password { get; set; }

        public string Salt { get; set; }

        public DateTime CreateTime { get; set; }

        public int Points { get; set; }

        public IEnumerable<Vote> VotesHistory { get; set; }

        public UserStatus Status { get; set; }
    }
}