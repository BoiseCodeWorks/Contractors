using System.ComponentModel.DataAnnotations;

namespace contracted.Models
{
    public class Contractor
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1, 999)]
        public decimal Rate { get; set; }
    }
}





