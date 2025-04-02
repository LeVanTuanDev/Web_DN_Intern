using Microsoft.AspNetCore.Mvc;

namespace Source_Demo.Controllers
{
    public class ImageManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
