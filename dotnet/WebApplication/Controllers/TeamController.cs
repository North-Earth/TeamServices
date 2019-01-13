using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WebApplication.Models.Repositories;
using WebApplication.Models.Services;

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

        private readonly IService _service;

        public TeamController(IConfiguration configuration,
            IHostingEnvironment hostingEnvironment, IRepository repository, IService service)
        {
            Configuration = configuration;

            var filesQuery = Configuration.GetValue<string>("SqlQueries:Files");
            var dictionaryQuery = Configuration.GetValue<string>("SqlQueries:Dictionary");
            var quotesQuery = Configuration.GetValue<string>("SqlQueries:Quote");
            var linksQuery = Configuration.GetValue<string>("SqlQueries:Links");

            _hostingEnvironment = hostingEnvironment;

            _repository = repository;

            _files = repository.GetData<Models.DataBase.File>(filesQuery).Result.ToList();
            _dictionary = repository.GetData<Models.DataBase.Dictionary>(dictionaryQuery).Result.ToList();
            _quotes = repository.GetData<Models.DataBase.Quote>(quotesQuery).Result.ToList();
            _links = repository.GetData<Models.DataBase.Link>(linksQuery).Result.ToList();

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
            var quoteRnd = new Random().Next(_quotes.Count);

            ViewData["IP"] = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            ViewData["MachineName"] = _service.GetMachineName(Request.HttpContext.Connection.RemoteIpAddress.ToString());
            Models.DataBase.Quote quote = _quotes[quoteRnd];
            ViewBag.Quote = quote;
            ViewBag.Links = _links;

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
    }
}