using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using WebApplication.Models.DataBase;

namespace WebApplication.Models.Services
{
    public class Data
    {
        #region Fields

        public List<File> Files { get; set; }
        public List<Dictionary> Dictionary { get; set; }
        public List<Quote> Quotes { get; set; }
        public List<Link> Links { get; set; }
        public List<Project> Projects { get; set; }
        public List<Staff> Staff { get; set; }
        public List<WorkReport> OvertimeWorkReports { get; set; }

        #endregion

        #region Constructors

        public Data()
        {

        }

        #endregion

        #region Methods

        public IEnumerable<SqlQuery> GetQueries(IConfiguration configuration)
        {
            var queries = configuration.GetSection("NewSqlQueries").Get<List<SqlQuery>>();
            return queries;
        }

        #endregion
    }
}
