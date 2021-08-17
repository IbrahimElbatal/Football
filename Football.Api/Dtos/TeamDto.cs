using System;

namespace Football.Api.Dtos
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryName { get; set; }
        public int CountryId { get; set; }
        public string CoachName { get; set; }
        public string LogoUrl { get; set; }
        public DateTime FoundationDate { get; set; }
    }
}