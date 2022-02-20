using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballManager.Data.Models
{
    using static DataConstants;
    public class Player
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(FullNameMaxLength, MinimumLength = FullNameMinLength)]
        public string FullName { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        [StringLength(PositionMaxLength, MinimumLength = PositionMinLength)]
        public string Position { get; set; }

        [Range(SpeedMinValue, SpeedMaxValue)]
        public byte Speed { get; set; }

        [Range(EnduranceMinValue, EnduranceMaxValue)]
        public byte Endurance { get; set; }

        [Required]
        [StringLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public IEnumerable<UserPlayer> UserPlayers { get; set; } = new HashSet<UserPlayer>();
    }
}
