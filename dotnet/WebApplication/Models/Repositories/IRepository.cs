using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models.Repositories
{
    public interface IRepository
    {
        Task<IEnumerable<T>> GetData<T>(string sqlQuery) where T : class;

        void SetData<T>(string sqlQuery, List<T> data) where T : class;
    }
}
