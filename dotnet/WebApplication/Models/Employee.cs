using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string MachineName { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public string IpAddress { get; set; }

        public string MacAddress { get; set; }
    }
}
