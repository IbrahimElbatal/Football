using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Football.Api.Models
{
    public class Player
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        public Nationality Nationality { get; set; }
        
        [ForeignKey(nameof(Nationality))]
        public int NationalityId { get; set; }

        [Required]
        [MaxLength(500)]
        public string ImageUrl { get; set; }
        public DateTime DateOfBirth { get; set; }

        [ForeignKey(nameof(Team))]
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}