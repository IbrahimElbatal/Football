using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Football.Api.Dtos
{
    public class EditTeamDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Name { get; set; }

        [Required]
        [MaxLength(500)]
        public string CoachName { get; set; }

        public DateTime FoundationDate { get; set; }
        
        public int CountryId { get; set; }

        public IFormFile Logo { get; set; }

    }
}