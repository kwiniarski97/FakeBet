using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FakeBet.API.Models
{
    public enum UserStatus
    {
        /// <summary>
        /// User that confirmed his email. Normal state.
        /// </summary>
        Active,

        /// <summary>
        /// User that not confirmed his email.
        /// </summary>
        NotActivated,

        /// <summary>
        /// User that requested account delete.
        /// </summary>
        Deactivated,

        /// <summary>
        /// User that has been banned.
        /// </summary>
        Banned
    }

    public class User
    {
        [Key] public string NickName { get; set; }

        [Required] public string Email { get; set; }

        [Required, MaxLength(64)] public byte[] PasswordHash { get; set; }

        [Required, MaxLength(128)] public byte[] Salt { get; set; }

        [Required]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        [Required] public int Points { get; set; } = 5000;

        public int FailedLoginsAttemps { get; set; } = 0;
        
        public IEnumerable<Vote> Votes { get; set; }

        public UserStatus Status { get; set; } = UserStatus.NotActivated;
    }
}