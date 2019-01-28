using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models.DataBase
{
    public class Staff
    {
        public string UserName { get; set; }

        public string MachineName { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public string IpAddress { get; set; }
    }
}
