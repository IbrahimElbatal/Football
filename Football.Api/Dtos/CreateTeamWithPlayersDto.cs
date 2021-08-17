using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Football.Api.Models;
using Microsoft.AspNetCore.Http;

namespace Football.Api.Dtos
{
    public class CreateTeamWithPlayersDto
    {
        public CreateTeamWithPlayersDto()
        {
            //Players = new List<CreatePlayerDto>();
        }
        [Required]
        [MaxLength(500)]
        public string Name { get; set; }

        public int CountryId { get; set; }

        [Required]
        [MaxLength(500)]
        public string CoachName { get; set; }
        public IFormFile Logo { get; set; }
        public DateTime FoundationDate { get; set; }

        public string Players { get; set; }
        public IFormFile[] PlayerImages { get; set; }
    }

    public class CreatePlayerDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        public int NationalityId { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
