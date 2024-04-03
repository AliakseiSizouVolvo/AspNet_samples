using Microsoft.AspNetCore.Mvc;
using NetAcademy.UI.Models;
using System.Diagnostics;
using NetAcademy.UI.ConfigSettings;
using NetAcademy.UI.Filters;

namespace NetAcademy.UI.Controllers
{
   
    //GET, POST, PUT, PATCH, DELETE, HEAD, OPTIONS
    [IEFilter]
    //[TypeFilter(typeof(DIActionFilterAttribute))]
    //[ServiceFilter(typeof(DIActionFilterAttribute))]
    [CustomException]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;
        
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        //[CheckData]
        //[SecretToken("_very secret data, don't tell anyone_")]
        //[WhitespaceRemover]
        public IActionResult Index(/*int id*/)
        {
            _logger.LogTrace("[TRACE] Hello from main page logger");
            _logger.LogDebug("[DEBUG] Hello from main page logger");
            _logger.LogInformation("[INFORMATION] Hello from main page logger");
            _logger.LogWarning("[WARNING] Hello from main page logger");
            _logger.LogError("[ERROR] Hello from main page logger");
            _logger.LogCritical("[CRITICAL] Hello from main page logger");
            var dataFromQuery = Request.Query;
            return View();
        }
        //route, forms, body, headers 

       
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
            try
            {
                var x = 0; var y = 15;
                var result = y / x;
                return View();
            }
            catch (Exception e)
            {
                _logger.LogError(e,"Divide by zero");
                throw;
            }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
