using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebKozein.Data;
using WebKozein.Models;
using WebKozein.Models.CodeFirst;
using WebKozein.Models.FilterSortView;

namespace WebKozein.Controllers
{
    public class MainController : Controller
    {
        private ConstComboBoxViewModel model = new ConstComboBoxViewModel();

        public IActionResult Index()
        {
            List<List<List<ConstComboBox>>> test = new List<List<List<ConstComboBox>>>();
            for(int i=0; i<5; i++)
            {
                test[i] = new List<List<ConstComboBox>>(5);
                for(int j=0;j<5; j++)
                {
                    test[i][j] = model.getConstComboBoxes();
                }
            }
            return View(test);
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