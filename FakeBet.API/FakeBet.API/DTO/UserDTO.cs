﻿using System;
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

        public UserStatus Status { get; set; }

        public UserRole Role { get; set; }
    }
}