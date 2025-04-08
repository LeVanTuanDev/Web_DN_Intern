using Microsoft.AspNetCore.Mvc;

namespace Source_Demo.Controllers
{
    public class ImageNewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
