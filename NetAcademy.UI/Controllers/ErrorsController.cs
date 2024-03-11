using Microsoft.AspNetCore.Mvc;

namespace NetAcademy.UI.Controllers
{
    public class ErrorsController : Controller
    {
        public IActionResult Error404()
        {
            return Ok("Not ok");
        }
    }
}
