using Microsoft.AspNetCore.Mvc;

namespace Endpoint.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
