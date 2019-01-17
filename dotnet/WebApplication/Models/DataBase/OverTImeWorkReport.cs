using System;

namespace WebApplication.Models.DataBase
{
    public class OverTimeWorkReport
    {
        public string Name { get; set; }

        public string UserName { get; set; }

        public int OverTimeHour { get; set; }

        public DateTime LoadDtm { get; set; }
    }
}
