using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Dictionary
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(1024)]
        public string Description { get; set; }

        [Required]
        [MaxLength(2048)]
        public string SqlExpression { get; set; }
    }
}
