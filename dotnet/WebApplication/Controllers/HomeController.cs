using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Linq;
using WebApplication.Models;
using WebApplication.Models.Repositories;
using WebApplication.Models.Services;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        #region Fields

        public IConfiguration Configuration { get; }

        public IService _service { get; }

        public IRepository _repository { get; }

        public IDataLoader _dataLoader { get; }

        public Data _data { get; }

        #endregion

        #region Constructors

        public HomeController(IConfiguration configuration, IService service,
            IRepository repository, IDataLoader dataLoader)
        {
            Configuration = configuration;

            _service = service;

            _repository = repository;

            _dataLoader = dataLoader;

            _data = (_service as Service).GetData(loader: _dataLoader, repository: _repository).Result;
        }

        #endregion

        #region Methods

        public IActionResult Index()
        {
            var staff = _data.Staff;

            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress;
            var machineName = _service.GetMachineName(ipAddress.ToString());
            var currentUser = staff.Where(e => e.MachineName == machineName).FirstOrDefault();
            ViewData["UserName"] = currentUser?.Name;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion
    }
}
