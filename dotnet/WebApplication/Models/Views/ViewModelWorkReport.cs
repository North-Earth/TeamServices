using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models.Views
{
    public class ViewModelWorkReport
    {
        [Required(ErrorMessage = "Необходимо заполнить описание причины")]
        [MaxLength(256)]
        public string Description { get; set; }

        /// <summary>
        /// Идентификатор типа отчёта.
        /// </summary>
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить дату отчёта")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить кол-во рабочих часов")]
        public int TimeHour { get; set; }
    }
}
