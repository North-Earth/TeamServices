using System.Net;
using WebApplication.Models.DataBase;

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
