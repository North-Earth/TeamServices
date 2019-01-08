using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace WebApplication.Models.Services
{
    public class Service : IService
    {
        public readonly User _currentUser;

        public Service()
        {
            _currentUser = new User { MachineName = "HostMachine" };
        }

        public string GetMachineName(string ip)
        {
            var hostEntry = Dns.GetHostEntry(ip);
            var hostName = hostEntry.HostName;

            return hostName;
        }
    }
}
