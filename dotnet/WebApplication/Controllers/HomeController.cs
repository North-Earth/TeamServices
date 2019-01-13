using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using WebApplication.Models;
using WebApplication.Models.DataBase;
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
        public List<Models.DataBase.User> _users { get; }

        #endregion

        #region Constructors

        public HomeController(IConfiguration configuration, IService service, IRepository repository)
        {
            Configuration = configuration;
            var usersQuery = Configuration.GetValue<string>("SqlQueries:Users");

            _service = service;

            _repository = repository;
            _users = repository.GetData<User>(usersQuery).Result.ToList();
        }

        #endregion

        #region Methods

        public IActionResult Index()
        {
            var ipAddress = Request.HttpContext.Connection.RemoteIpAddress;
            var machineName = _service.GetMachineName(ipAddress.ToString());
            var currentUser = _users.Where(e => e.MachineName == machineName).FirstOrDefault();
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
