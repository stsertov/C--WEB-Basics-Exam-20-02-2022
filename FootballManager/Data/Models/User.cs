using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballManager.Data.Models
{
    using static DataConstants;
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(UsernameMaxLength, MinimumLength = UsernameMinLength)]
        public string Username { get; set; }

        [Required]
        [StringLength(EmailMaxLength, MinimumLength = EmailMinLength)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public IEnumerable<UserPlayer> UserPlayers { get; set; } = new HashSet<UserPlayer>();
    }
}
