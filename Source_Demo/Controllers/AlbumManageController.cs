using Microsoft.AspNetCore.Mvc;

namespace Source_Demo.Controllers
{
    public class AlbumManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
