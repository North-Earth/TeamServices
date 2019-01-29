using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models.DataBase
{
    /// <summary>
    /// Модель данных для файлового репозитория.
    /// </summary>
    public class File
    {
        /// <summary>
        /// Формальное название файла, для отображения.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание файла.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Наименование и Расширение файла.
        /// </summary>
        public string FileName { get; set; }
    }
}
