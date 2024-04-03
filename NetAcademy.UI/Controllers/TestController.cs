using Microsoft.AspNetCore.Mvc;
using NetAcademy.Services.Abstractions;

namespace NetAcademy.UI.Controllers
{
    public class TestController : Controller
    {
        private readonly ITest1Service _test1;
        private readonly ITest2Service _test2;

        public TestController(ITest1Service test1,
            ITest2Service test2)
        {
            _test1 = test1;
            _test2 = test2;
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
        
    }
}
