using Microsoft.AspNetCore.Mvc;

namespace Source_Demo.Controllers
{
    public class NewsManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
