using System;

namespace WebApplication.Models.DataBase
{
    public class OvertimeWorkReport
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string UserName { get; set; }

        public int OvertimeHour { get; set; }

        public DateTime LoadDtm { get; set; }
    }
}
