namespace FakeBet.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

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
        [Key]
        public string NickName { get; set; }

        [Required]
        public string Email { get; set; }

        // todo ONLY FOR NOW
        [Required]
        public string Password { get; set; }

        [Required]
        [MaxLength(64)]
        public byte[] Salt { get; set; }

        public DateTime CreateTime { get; set; }

        public int Points { get; set; }

        public IEnumerable<Vote> Votes { get; set; }

        public UserStatus Status { get; set; }
    }
}