using Microsoft.AspNetCore.Mvc;

namespace Source_Demo.Controllers
{
    public class LibraryManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
