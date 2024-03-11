using Microsoft.AspNetCore.Mvc;
using NetAcademy.Services.Abstractions;
using NetAcademy.UI.Models;

namespace NetAcademy.UI.Controllers
{
    public class TestController : Controller
    {
        private static Cat _cat = new Cat()
        {
            Name = "Tom",
            Age = 5,
            Color = "Black"
        };
        private readonly ITest1Service _test1;
        private readonly ITest2Service _test2;

        public TestController(ITest1Service test1,
            ITest2Service test2)
        {
            _test1 = test1;
            _test2 = test2;
        }

        [HttpGet]
        public IActionResult TestModel()
        {
            var model = new Cat()
            {
                Name = "Tom",
                Color = "Grey",
                Age = 3
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult Home(int page, int pageSize)
        {
            return Ok("test/home");
        }


        [HttpGet]
        public IActionResult Lifecycle()
        {
            var test1ServiceValues = _test1.Do();
            var test2ServiceValues = _test2.Do();
            
            return Ok(new
            {
                Test1Transient = test1ServiceValues.Item1,
                Test1Scoped  = test1ServiceValues.Item2,
                Test1Singleton  = test1ServiceValues.Item3,
                Test2Transient = test2ServiceValues.Item1,
                Test2Scoped  = test2ServiceValues.Item2,
                Test2Singleton  = test2ServiceValues.Item3,
            });
        }
        
        
        [HttpGet]
        public IActionResult Test([FromQuery]int id)
        {
            return Ok(id);
        }

        [HttpPost]
        public IActionResult Process(PersonModel model)
        {
            return Ok(model);
        }

        [HttpPost]
        public IActionResult TestUpdate([FromQuery] int id, [FromForm] PersonModel person)
        {
            return null;
        }


        //ContentResult (return a string)

        //EmptyResult same as void
        
        //NoContentResult (Status 204)
        
        //FileResult (expect stream)
        //FileContentResult(expect byte[])
        //FileStreamResult()
        
        //ObjectResult - almost never used
        
        //set of results for generate View(UI) 
        //ViewResult -render view and send result of rendering as HTML page
        //ViewComponentResult - for Razor Pages
        //PartialViewResult 
        
        
        //set of action for redirect 
        //RedirectResult 
        //RedirectToActionResult
        //RedirectToRouteResult
        
        //set of statusCodeResults (usually used in WebApiProjects) 
        //OkResult
        //NotFoundResult
        //NoContentResult
        //NonAuthorizedResult
        //StatusCodeResult()


        public IActionResult RedirectTest()
        {
            //return Redirect("https://google.com"); //302
            //return Redirect($"~/Home/Index/");
            //return RedirectToRoute("default", new {controller = "Home", Action ="Privacy"});
            return RedirectToAction("Index", "Home");
        }

        public IActionResult StatusCode()
        {
            return Unauthorized();
            return NotFound();
            return BadRequest();
        }

        public IActionResult GetContext()
        {
            var context = HttpContext;
            var request = context.Request;
            var response = context.Response;
            return Ok(new { request, response });
        }

        [HttpGet]
        public IActionResult ViewCat()
        {
            return View(_cat);
        }

        [HttpGet]
        public IActionResult EditCat()
        {
            return View(_cat);
        }

        [HttpPost]
        public IActionResult EditCat(Cat cat)
        {
            return Ok(cat);
        }
    }
}
