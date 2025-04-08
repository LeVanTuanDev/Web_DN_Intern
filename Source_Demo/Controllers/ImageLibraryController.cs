using Microsoft.AspNetCore.Mvc;

namespace Source_Demo.Controllers
{
    public class ImageLibraryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
