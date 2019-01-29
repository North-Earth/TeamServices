using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models.Services
{
    public interface IService
    {
        string GetMachineName(string ip);
    }
}
