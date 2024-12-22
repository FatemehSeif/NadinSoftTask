using Microsoft.AspNetCore.Mvc;

namespace Endpoint.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
