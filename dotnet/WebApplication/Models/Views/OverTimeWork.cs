using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models.Views
{
    public class OverTimeWork
    {
        public string Name { get; set; }

        public int OverTime { get; set; }

        public int TimeOff { get; set; }

        public int LastOverTime { get; set; }

        public int LastTimeOff { get; set; }
    }
}
