using System.ComponentModel.DataAnnotations;

namespace Football.Api.Models
{
    public class Country
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Name { get; set; }
    }
}