using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Football.Api.Dtos
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NationalityName { get; set; }
        public int NationalityId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int TeamId { get; set; }
        public string TeamName { get; set; }
    }
}
