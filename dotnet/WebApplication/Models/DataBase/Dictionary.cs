using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.DataBase
{
    public class Dictionary
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить наименование")]
        [MaxLength(64)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить описание")]
        [MaxLength(256)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить SQL выражение")]
        [MaxLength(256)]
        public string SqlExpression { get; set; }
    }
}
