using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Football.Api.Models
{
    public class Team
    {
        public Team()
        {
            Players = new HashSet<Player>();
        }
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Name { get; set; }

        [ForeignKey(nameof(Country))]
        public int CountryId { get; set; }
        public Country Country { get; set; }

        [Required]
        [MaxLength(500)]
        public string CoachName { get; set; }

        [Required]
        [MaxLength(500)]
        public string LogoUrl { get; set; }
        public DateTime FoundationDate { get; set; }

        public ICollection<Player> Players { get; set; }
    }
}
