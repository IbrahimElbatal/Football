using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Football.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace Football.Api.Dtos
{
    public class EditPlayerDto
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
        public int NationalityId { get; set; }

        public IFormFile Image { get; set; }
        public DateTime DateOfBirth { get; set; }

        public int TeamId { get; set; }
    }
}
