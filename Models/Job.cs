using System.ComponentModel.DataAnnotations;

namespace contracted.Models
{
    public class Job
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public decimal Budget { get; set; }
    }
}