using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebKozein.Data;
using WebKozein.Models.CodeFirst;
using WebKozein.Models;

namespace WebKozein.Controllers
{
    public class MainController : Controller
    {
        private readonly InformDbContext _context;

        public MainController(InformDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(SortState sortOrder = SortState.NameAsc)
        {
            ViewData["NameSort"] = sortOrder== SortState.NameAsc ? SortState.NameDesc: SortState.NameAsc;

            IQueryable<InformDataBase> dataBases = _context.InformDataBases;

            dataBases = sortOrder switch
            {
                SortState.NameAsc => dataBases.OrderBy(s=>s.Name),
                SortState.NameDesc => dataBases.OrderByDescending(s => s.Name),
            };

            return View(await dataBases.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(InformDataBase data)
        {

            await _context.InformDataBases.AddAsync(data);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id.HasValue)
            {
                return View(await _context.InformDataBases.FindAsync(id.Value));
            }

            return null;
        }

        [HttpPost]
        public async Task<IActionResult> Edit(InformDataBase data)
        {
            _context.InformDataBases.Update(data);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id.HasValue)
            {
                return View(await _context.InformDataBases.FindAsync(id.Value));
            }

            return null;
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id.HasValue)
            {
                var entity = await _context.InformDataBases.FindAsync(id);
                if (entity != null)
                {
                    _context.InformDataBases.Remove(entity);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }

            return null;
        }
    }
}
