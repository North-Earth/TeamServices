using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        private readonly List<Models.DataBase.File> _files;
        private readonly List<Models.DataBase.Dictionary> _dictionary;
        private readonly List<Models.DataBase.Quote> _quotes;
        private readonly List<Models.DataBase.Link> _links;
        private readonly List<Models.DataBase.Project> _projects;
        private readonly List<Models.DataBase.User> _users;
        private readonly List<Models.DataBase.OvertimeWorkReport> _overtimeWorkReports;

        private readonly User currentUser;

        private readonly IService _service;

        public TeamController(IConfiguration configuration,
            IHostingEnvironment hostingEnvironment, IRepository repository, IService service, IDataLoader dataLoader)
        {
            Configuration = configuration;

            var filesQuery = Configuration.GetValue<string>("SqlQueries:Files");
            var dictionaryQuery = Configuration.GetValue<string>("SqlQueries:Dictionary");
            var quotesQuery = Configuration.GetValue<string>("SqlQueries:Quote");
            var linksQuery = Configuration.GetValue<string>("SqlQueries:Links");
            var projectsQuery = Configuration.GetValue<string>("SqlQueries:Projects");
            var usersQuery = Configuration.GetValue<string>("SqlQueries:Users");
            var overtimeWorkReportsQuery = Configuration.GetValue<string>("SqlQueries:OvertimeWorkReports");

            _hostingEnvironment = hostingEnvironment;

            _repository = repository;

            _dataLoader = dataLoader;

            _files = repository.GetData<Models.DataBase.File>(filesQuery).Result.ToList();
            _dictionary = repository.GetData<Models.DataBase.Dictionary>(dictionaryQuery).Result.ToList();
            _quotes = repository.GetData<Models.DataBase.Quote>(quotesQuery).Result.ToList();
            _links = repository.GetData<Models.DataBase.Link>(linksQuery).Result.ToList();
            _projects = repository.GetData<Models.DataBase.Project>(projectsQuery).Result.ToList();
            _users = repository.GetData<Models.DataBase.User>(usersQuery).Result.ToList();
            _overtimeWorkReports = repository.GetData<Models.DataBase.OvertimeWorkReport>(overtimeWorkReportsQuery).Result.ToList();

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

        public async Task<IActionResult> WorkReports()
        {
            var data = await _dataLoader.GetDataAsync(_repository);
            var overTimeWorkReports = data.OvertimeWorkReports ?? new List<OvertimeWorkReport>();

            var service = _service as Service;
            var reports = service.ParseToOvertimeReport(overTimeWorkReports);

            ViewBag.OverTimeWorkReports = reports;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> WorkReports(ViewModelOverTimeWorkReport report)
        {
            var machineName = _service.GetMachineName(Request.HttpContext.Connection.RemoteIpAddress.ToString());
            var currentUser = _users.Where(usr => usr.MachineName == machineName.ToString()).FirstOrDefault();

            if (report.Id == 2)
                report.Time = report.Time * -1;

            var newReports = new List<OvertimeWorkReport>
            {
                new OvertimeWorkReport
                {
                    Name = currentUser.Name,
                    UserName = currentUser.MachineName,
                    Description = report.Description,
                    LoadDtm = DateTime.Now,
                    OvertimeHour =report.Time
                }
            };

            var data = await _dataLoader.GetDataAsync(_repository);

            var queries = data.GetQueries(Configuration);

            var query = queries.Where(q => q.Name == "OverTimeWorkReportsInsert").FirstOrDefault().Query;

            _repository.SetData(query, newReports);

            return RedirectToAction(nameof(WorkReports));
        }
    }
}