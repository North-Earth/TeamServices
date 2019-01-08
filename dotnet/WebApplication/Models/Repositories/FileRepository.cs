using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace WebApplication.Models.Repositories
{
    public interface IFileRepository
    {
        Task<List<File>> GetFiles();
    }

    public class FileRepository : IFileRepository
    {
        private readonly string _connectionString = null;

        public FileRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<File>> GetFiles()
        {
            using (IDbConnection connection = new SqlConnection(_connectionString))
            {
                var files = await connection.QueryAsync<File>("SELECT fls.Id, fls.Name, fls.Description, fls.FileName FROM twt.Files AS fls");
                return files.ToList();
            }
        }
    }
}
