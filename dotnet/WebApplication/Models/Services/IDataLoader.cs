using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models.Repositories;

namespace WebApplication.Models.Services
{
    public interface IDataLoader
    {
        //Task<T> GetDataAsync<T>() where T : class;

        Task<Data> GetDataAsync(IRepository repository);
    }
}
