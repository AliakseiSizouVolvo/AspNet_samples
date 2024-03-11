using Microsoft.AspNetCore.Mvc;
using NetAcademy.UI.Models;
using System.Diagnostics;
using NetAcademy.UI.ConfigSettings;

namespace NetAcademy.UI.Controllers
{
    //public class HomeController .... SomeName[Controller]
    //[Controller]
    //[NonController]

    //GET, POST, PUT, PATCH, DELETE, HEAD, OPTIONS
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        //[NonAction]
        [HttpGet]
        public IActionResult Index()
        {
            var dataFromQuery = Request.Query;
            return View();
        }
        //route, forms, body, headers 

        //[ActionName("SomeTest")]
        //[HttpPost]
        [HttpGet]
        public IActionResult Test()
        {
            //var settings = _configuration.GetSection("AppSettings").GetSection("Secret1").Value;
            //var settingSections = _configuration.GetChildren();
            //var connString = _configuration.GetConnectionString("Default");
            //var reloadToken = _configuration.GetReloadToken();

            //var secret1 = _configuration["AppSettings:Secret1"];
            //_configuration["AppSettings:Secret1"] = "123123";
            //var secret12 = _configuration["AppSettings:Secret1"];
            var secrets = _configuration.GetSection("AppSettings").Get<AppSettings>();
            return Ok("home/test");
        }

        [HttpGet]
        public IActionResult Test2(PersonModel person)
        {
                return Ok(
                    new
                    {
                        person.Name,
                        person.Age,
                        person.Email
                    });
        }

        [HttpGet]
        public IActionResult Test3(PersonModel[] persons)
        {
            return Ok(
                persons
                    .Select(person => 
                        new { person.Name, person.Age, person.Email })
                    .ToArray());
        }

        [HttpGet]
        public IActionResult Test4(Dictionary<string, string> data)
        {
            return Ok(data);

        }

        // GET, POST, DELETE, PUT, (PATCH?)
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
