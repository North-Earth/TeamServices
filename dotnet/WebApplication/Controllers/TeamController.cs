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

        private readonly List<Models.DataBase.File> _files;
        private readonly List<Models.DataBase.Dictionary> _dictionary;
        private readonly List<Models.DataBase.Quote> _quotes;
        private readonly List<Models.DataBase.Link> _links;
        private readonly List<Models.DataBase.Project> _projects;
        private readonly List<Models.DataBase.User> _users;
        private readonly List<Models.DataBase.OverTimeWorkReport> _overTimeWorkReports;

        private readonly IService _service;

        public TeamController(IConfiguration configuration,
            IHostingEnvironment hostingEnvironment, IRepository repository, IService service)
        {
            Configuration = configuration;

            var filesQuery = Configuration.GetValue<string>("SqlQueries:Files");
            var dictionaryQuery = Configuration.GetValue<string>("SqlQueries:Dictionary");
            var quotesQuery = Configuration.GetValue<string>("SqlQueries:Quote");
            var linksQuery = Configuration.GetValue<string>("SqlQueries:Links");
            var projectsQuery = Configuration.GetValue<string>("SqlQueries:Projects");
            var usersQuery = Configuration.GetValue<string>("SqlQueries:Users");
            var overTimeWorkReportsQuery = Configuration.GetValue<string>("SqlQueries:OverTimeWorkReports");

            _hostingEnvironment = hostingEnvironment;

            _repository = repository;

            _files = repository.GetData<Models.DataBase.File>(filesQuery).Result.ToList();
            _dictionary = repository.GetData<Models.DataBase.Dictionary>(dictionaryQuery).Result.ToList();
            _quotes = repository.GetData<Models.DataBase.Quote>(quotesQuery).Result.ToList();
            _links = repository.GetData<Models.DataBase.Link>(linksQuery).Result.ToList();
            _projects = repository.GetData<Models.DataBase.Project>(projectsQuery).Result.ToList();
            _users = repository.GetData<Models.DataBase.User>(usersQuery).Result.ToList();
            _overTimeWorkReports = repository.GetData<Models.DataBase.OverTimeWorkReport>(overTimeWorkReportsQuery).Result.ToList();

            _service = service;
        }

        public IActionResult Index()
        {
            var quoteRnd = new Random().Next(_quotes.Count);

            ViewData["IP"] = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            ViewData["MachineName"] = _service.GetMachineName(Request.HttpContext.Connection.RemoteIpAddress.ToString());
            ViewData["User"] = _users.Where(usr => usr.MachineName == ViewData["MachineName"].ToString()).FirstOrDefault()?.Name;
            Models.DataBase.Quote quote = _quotes[quoteRnd];
            ViewBag.Quote = quote;
            ViewBag.Links = _links;
            ViewBag.Projects = _projects;

            return View();
        }

        public IActionResult Dictionary()
        {
            ViewData["Dictionary"] = _dictionary;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Dictionary(Models.DataBase.Dictionary dictionary)
        {
            var dictionaryInsertQuery = Configuration.GetValue<string>("SqlQueries:DictionaryInsert");
            ViewData["Dictionary"] = _dictionary;

            try
            {
                _repository.SetData(dictionaryInsertQuery
                    , new List<Models.DataBase.Dictionary> { dictionary });

                return RedirectToAction(nameof(Dictionary));
            }
            catch (Exception ex)
            {
                return View();
            }

        }

        public IActionResult Files()
        {
            return View(_files);
        }

        public VirtualFileResult GetFile(string fileName)
        {
            var path = Path.Combine("~/Files", fileName);
            return File(path, "application/octet-stream", fileName);
        }

        public IActionResult Reports()
        {
            var overTimeWOrkReports = new List<OverTimeWork>();

            var _overTimeWorkReports = new List<OverTimeWorkReport> { new OverTimeWorkReport { Name = "Admin", LoadDtm = DateTime.Now, OverTimeHour = 12 } };

            foreach (var reportsUser in _overTimeWorkReports.GroupBy(r => r.Name))
            {
                var report = new OverTimeWork();

                report.Name = reportsUser.Key;

                foreach (var reportUser in reportsUser
                    .Where(r => r.LoadDtm.Month == DateTime.Now.Month))
                {
                    if (reportUser.OverTimeHour >= 0)
                        report.OverTime += reportUser.OverTimeHour;
                    else if (reportUser.OverTimeHour < 0)
                        report.TimeOff += reportUser.OverTimeHour;
                }

                foreach (var reportUser in reportsUser
                    .Where(r => r.LoadDtm.Month == DateTime.Now.AddMonths(-1).Month))
                {
                    if (reportUser.OverTimeHour >= 0)
                        report.LastOverTime += reportUser.OverTimeHour;
                    else if (reportUser.OverTimeHour < 0)
                        report.LastTimeOff += reportUser.OverTimeHour;
                }

                overTimeWOrkReports.Add(report);
            }

            ViewBag.OverTimeWorkReports = overTimeWOrkReports;

            return View();
        }

        [HttpPost]
        public IActionResult Reports(Models.DataBase.OverTimeWorkReport reports)
        {
            return View();
        }
    }
}