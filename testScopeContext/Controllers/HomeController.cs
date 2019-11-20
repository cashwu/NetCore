using Microsoft.AspNetCore.Mvc;

namespace testScopeContext.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("OK");
        }
    }
}