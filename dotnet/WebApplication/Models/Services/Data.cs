using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using WebApplication.Models.DataBase;

namespace WebApplication.Models.Services
{
    public class Data
    {
        #region Fields

        public List<DataBase.File> files { get; set; }
        public List<DataBase.Dictionary> dictionary { get; set; }
        public List<DataBase.Quote> quotes { get; set; }
        public List<DataBase.Link> links { get; set; }
        public List<DataBase.Project> projects { get; set; }
        public List<DataBase.User> users { get; set; }
        public List<DataBase.OverTimeWorkReport> overTimeWorkReports { get; set; }

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
