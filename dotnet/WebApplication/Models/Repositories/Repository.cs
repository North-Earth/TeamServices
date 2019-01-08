using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace WebApplication.Models.Repositories
{
    public class Repository : IRepository
    {
        #region Fields

        private readonly string _connectionString = null;

        #endregion

        #region Constructors

        public Repository(string connectionString)
        {
            _connectionString = connectionString;
        }

        #endregion

        #region Methods

        public async Task<IEnumerable<T>> GetData<T>(string sqlQuery) where T : class
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var data = await connection.QueryAsync<T>(sqlQuery);
                return data.ToList();
            }
        }

        #endregion
    }
}
