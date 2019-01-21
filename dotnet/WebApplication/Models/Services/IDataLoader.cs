using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models.Services
{
    public interface IDataLoader
    {
        Task GetDataAsync<T>() where T : class;

        Task LoadDataAsync();
    }
}
