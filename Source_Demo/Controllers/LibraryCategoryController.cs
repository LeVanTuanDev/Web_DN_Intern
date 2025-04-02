using Microsoft.AspNetCore.Mvc;

namespace Source_Demo.Controllers
{
    public class LibraryCategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
