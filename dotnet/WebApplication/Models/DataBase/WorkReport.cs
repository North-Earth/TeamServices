using System;

namespace WebApplication.Models.DataBase
{
    public class WorkReport
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string UserName { get; set; }

        public int TimeHour { get; set; }

        public DateTime LoadDtm { get; set; }
    }
}
