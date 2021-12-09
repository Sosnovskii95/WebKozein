using Microsoft.AspNetCore.Mvc;
using WebKozein.Models.Hierarchy;

namespace WebKozein.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
