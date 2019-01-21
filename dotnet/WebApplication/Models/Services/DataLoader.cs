using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models.Services
{
    public class DataLoader : IDataLoader
    {
        #region Fields

        private readonly IConfiguration _configuration;

        private readonly Data _data;

        #endregion

        #region Constructors

        public DataLoader(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion

        #region Methods

        public Task GetDataAsync<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public Task LoadDataAsync()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
