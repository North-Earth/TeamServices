using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication.Models.Repositories;
using WebApplication.Models.Services;

namespace WebApplication.Controllers
{
    public class TeamController : Controller
    {
        public IConfiguration Configuration { get; }

        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly IRepository _repository;
       //private readonly List<Models.Employee> _employees;
        private readonly List<Models.File> _files;
        private readonly List<Models.Dictionary> _dictionary;

        private readonly IService _service;

        public TeamController(IConfiguration configuration, IHostingEnvironment hostingEnvironment,
            IFileRepository fileRepository, IRepository repository, IService service)
        {
            Configuration = configuration;
            //var employeesQuery = Configuration.GetValue<string>("SqlQueries:Employees");
            var filesQuery = Configuration.GetValue<string>("SqlQueries:Files");
            var dictionaryQuery = Configuration.GetValue<string>("SqlQueries:Dictionary");

            _hostingEnvironment = hostingEnvironment;

            _repository = repository;
           // _employees = repository.GetData<Models.Employee>(employeesQuery).Result.ToList();
            _files = repository.GetData<Models.File>(filesQuery).Result.ToList();
            _dictionary = repository.GetData<Models.Dictionary>(dictionaryQuery).Result.ToList();

            _service = service;

            /* Статические данные для демо без работы БД*/

            //_employees = new List<Models.Employee>
            //{
            //    new Models.Employee { Name = "Новый Пользователь", IpAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString() }
            //};
            //_files = new List<Models.File>
            //{
            //    new Models.File { Name = "Отсутствие на рабочем месте", Description = "Заявление об отсутствии сотрудника на рабочем месте.", FileName = "Шаблон отсутствие на рабочем месте.docx" },
            //    new Models.File { Name = "Неоплачиваемый отпуск", Description = "Заявление на неоплачиваемый отпуск", FileName = "Шаблон ежегодный оплачиваемый отпуск.docx" },
            //    new Models.File { Name = "Оплачиваемый отпуск", Description = "Заявление на ежегодный отпуск с сохранением заработной платы.", FileName = "Шаблон неоплачиваемый отпуск.docx" }
            //};
            //_dictionary = new List<Models.Dictionary>
            //{
            //    new Models.Dictionary { Id = 0, Name = "Тестовая запись", Description = "Тестовое описание", SqlExpression = "SELECT 'Test'"}
            //};
        }

        public IActionResult Index()
        {
            ViewData["IP"] = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            ViewData["MachineName"] = _service.GetMachineName(Request.HttpContext.Connection.RemoteIpAddress.ToString());
            //ViewData["User"] = _employees.Where(emp => emp.MachineName == _service.GetMachineName(Request.HttpContext.Connection.RemoteIpAddress.ToString())).FirstOrDefault().Name;
            return View();
        }

        public IActionResult Dictionary()
        {
            return View(_dictionary);
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
    }
}