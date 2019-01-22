using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Models.Repositories;

namespace WebApplication.Models.Services
{
    public class DataLoader : IDataLoader
    {
        #region Fields

        private readonly IConfiguration _configuration;
        private readonly IRepository _repository;

        private readonly Data _data = new Data();

        #endregion

        #region Constructors

        public DataLoader(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region Methods

        public async Task<T> GetDataAsync<T>() where T : class
        {
            await LoadDataAsync();
            return _data as T;
        }

        public async Task LoadDataAsync()
        {
            var queries = _data.GetQueries(_configuration);

            _data.files = _repository.GetData<Models.DataBase.File>(queries.Where(q
                => q.Name == "Files").FirstOrDefault().Query).Result.ToList();

            _data.dictionary = _repository.GetData<DataBase.Dictionary>(queries.Where(q
                => q.Name == "Dictionary").FirstOrDefault().Query).Result.ToList();

            _data.quotes = _repository.GetData<DataBase.Quote>(queries.Where(q
                => q.Name == "Quotes").FirstOrDefault().Query).Result.ToList();

            _data.links = _repository.GetData<DataBase.Link>(queries.Where(q
                => q.Name == "Links").FirstOrDefault().Query).Result.ToList();

            _data.projects = _repository.GetData<DataBase.Project>(queries.Where(q
                => q.Name == "Projects").FirstOrDefault().Query).Result.ToList();

            _data.users = _repository.GetData<DataBase.User>(queries.Where(q
                => q.Name == "Users").FirstOrDefault().Query).Result.ToList();

            _data.overTimeWorkReports = _repository.GetData<DataBase.OverTimeWorkReport>(queries.Where(q
                => q.Name == "OverTimeWorkReports").FirstOrDefault().Query).Result.ToList();
        }

        #endregion
    }
}
