using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebKozein.Data;
using WebKozein.Models.CodeFirst;
using WebKozein.Models.FilterSortView;

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
