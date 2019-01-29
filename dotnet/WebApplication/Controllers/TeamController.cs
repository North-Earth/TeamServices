using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebApplication.Models.DataBase;
using WebApplication.Models.Repositories;
using WebApplication.Models.Services;
using WebApplication.Models.Views;

namespace WebApplication.Controllers
{
    public class TeamController : Controller
    {
        public IConfiguration Configuration { get; }

        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly IRepository _repository;

        private readonly IDataLoader _dataLoader;

        private readonly IService _service;

        private readonly Data _data;

        public TeamController(IConfiguration configuration, IHostingEnvironment hostingEnvironment,
            IRepository repository, IService service, IDataLoader dataLoader)
        {
            Configuration = configuration;

            _hostingEnvironment = hostingEnvironment;

            _repository = repository;

            _dataLoader = dataLoader;

            _service = service;

            _data = (_service as Service)
                .GetData(loader: _dataLoader, repository: _repository).Result;
        }

        public IActionResult Index()
        {
            var quotes = _data.Quotes ?? new List<Quote>();
            var staff = _data.Staff ?? new List<Staff>();
            var quoteRnd = new Random().Next(quotes.Count);

            ViewData["IP"] = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            ViewData["MachineName"] = _service.GetMachineName(Request.HttpContext.Connection.RemoteIpAddress.ToString());
            ViewData["User"] = staff.Where(usr => usr.MachineName == ViewData["MachineName"].ToString()).FirstOrDefault()?.Name;

            Models.DataBase.Quote quote = default;

            if (quotes.Count > 0)
                quote = quotes[quoteRnd];

            ViewBag.Quote = quote;
            ViewBag.Links = _data.Links;
            ViewBag.Projects = _data.Projects;

            return View();
        }

        public IActionResult Dictionary()
        {
            ViewData["Dictionary"] = _data.Dictionary ?? new List<Models.DataBase.Dictionary>();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Dictionary(Models.DataBase.Dictionary dictionary)
        {
            var queries = _data.GetQueries(Configuration);
            var query = queries.Where(q => q.Name == "DictionaryInsert").FirstOrDefault().Query.ToString();

            var data = new List<Dictionary>
            {
                dictionary
            };

            ViewData["Dictionary"] = _data.Dictionary ?? new List<Dictionary>();

            try
            {
                _repository.SetData<Models.DataBase.Dictionary>(query, data);

                return RedirectToAction(nameof(Dictionary));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Dictionary));
            }

        }

        public IActionResult Files()
        {
            var files = _data.Files ?? new List<Models.DataBase.File>();
            return View(files);
        }

        public VirtualFileResult GetFile(string fileName)
        {
            var path = Path.Combine("~/Files", fileName);
            return File(path, "application/octet-stream", fileName);
        }

        public IActionResult WorkReports()
        {
            var overTimeWorkReports = _data.WorkReports ?? new List<WorkReport>();

            var service = _service as Service;
            var reports = service.ParseToOvertimeReport(overTimeWorkReports);

            ViewBag.OverTimeWorkReports = reports;

            return View();
        }

        [HttpPost]
        public IActionResult WorkReports(ViewModelWorkReport report)
        {
            var machineName = _service.GetMachineName(Request.HttpContext.Connection.RemoteIpAddress.ToString());
            var staff = _data.Staff ?? new List<Staff>();
            var currentUser = staff
                .Where(usr => usr.MachineName == machineName.ToString())
                .FirstOrDefault() ?? new Staff { Name = "Unknown", UserName = "Unknown" };

            if (report.Id == 2)
                report.TimeHour = -report.TimeHour;

            var newReports = new List<WorkReport>
            {
                new WorkReport
                {
                    Name = currentUser.Name,
                    UserName = currentUser.UserName,
                    Description = report.Description,
                    LoadDtm = report.Date,
                    TimeHour = report.TimeHour
                }
            };

            var queries = _data.GetQueries(Configuration);

            var query = queries.Where(q => q.Name == "WorkReportsInsert").FirstOrDefault().Query;

            _repository.SetData(query, newReports);

            return RedirectToAction(nameof(WorkReports));
        }

        public IActionResult WorkReportsHistory()
        {
            ViewBag.WorkReports = _data.WorkReports.OrderBy(r => r.LoadDtm);
            return View();
        }
    }
}