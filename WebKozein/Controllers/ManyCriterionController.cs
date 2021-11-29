using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebKozein.Data;
using WebKozein.Models.CodeFirst;
using WebKozein.Models.ComboBox;
using WebKozein.Models.FilterSortView;
using WebKozein.Models;

namespace WebKozein.Controllers
{
    public class ManyCriterionController : Controller
    {
        private readonly InformDbContext _context;
        private readonly UtilConstComboBox utilConstComboBox = new UtilConstComboBox();
        private readonly UtilTableComboBox utilTableComboBox = new UtilTableComboBox();

        public ManyCriterionController(InformDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(SortState sortOrder, int? fCost, int? fElectricity, int? fPower, int? fPowerTime)
        {
            IQueryable<InformDataBase> dataBases = _context.InformDataBases;

            if (fCost.HasValue)
            {
                dataBases = dataBases.Where(fc => fc.Cost <= fCost);
            }
            if (fElectricity.HasValue)
            {
                dataBases = dataBases.Where(fe => fe.Electricity <= fElectricity);
            }
            if (fPower.HasValue)
            {
                dataBases = dataBases.Where(fp => fp.Power <= fPower);
            }
            if (fPowerTime.HasValue)
            {
                dataBases = dataBases.Where(fp => fp.PowerTime >= fPowerTime);
            }

            dataBases = sortOrder switch
            {
                SortState.IdAsc => dataBases.OrderBy(s => s.Id),
                SortState.IdDesc => dataBases.OrderByDescending(s => s.Id),
                SortState.NameAsc => dataBases.OrderBy(s => s.Name),
                SortState.NameDesc => dataBases.OrderByDescending(s => s.Name),
                SortState.CostAsc => dataBases.OrderBy(s => s.Cost),
                SortState.CostDesc => dataBases.OrderByDescending(s => s.Cost),
                SortState.ElectricityAsc => dataBases.OrderBy(s => s.Electricity),
                SortState.ElectricityDesc => dataBases.OrderByDescending(s => s.Electricity),
                SortState.WaterAsc => dataBases.OrderBy(s => s.Water),
                SortState.WaterDesc => dataBases.OrderByDescending(s => s.Water),
                SortState.AirAsc => dataBases.OrderBy(s => s.Air),
                SortState.AirDesc => dataBases.OrderByDescending(s => s.Air),
                SortState.PowerAsc => dataBases.OrderBy(s => s.Power),
                SortState.PowerDesc => dataBases.OrderByDescending(s => s.Power),
                SortState.PowerTimeAsc => dataBases.OrderBy(s => s.PowerTime),
                SortState.PowerTimeDesc => dataBases.OrderByDescending(s => s.PowerTime),
                _ => dataBases
            };

            ViewBag.constCombo = utilConstComboBox.getConstComboBoxes();
            TestClass testClass = new TestClass(await _context.TableComboBoxes.ToListAsync());

            IndexViewModel viewModel = new IndexViewModel
            {
                InformDataBases = await dataBases.AsNoTracking().ToListAsync(),
                TableComboBoxes = await _context.TableComboBoxes.ToListAsync(),
                FilterViewModel = new FilterViewModel(fCost, fElectricity, fPower, fPowerTime, false),
                SortViewModel = new SortViewModel(sortOrder),
                WeightModel = new WeightModel
                {
                    CostWeight = testClass.getMainAl()[0],
                    ElectricityWeight = testClass.getMainAl()[1],
                    PowerWeight = testClass.getMainAl()[2],
                    WaterWeight = testClass.getMainAl()[3],
                    AirWeignt = testClass.getMainAl()[4],
                    BestCriteria = testClass.getMainAl()[5],
                    NameBestCriteria = ""
                }
            };

            return View(viewModel);
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

        [HttpPost]
        public async Task<IActionResult> UpdateTableComboBox(List<int> Id, List<int> BoxCostIdConst,
            List<int> BoxElectricityIdConst, List<int> BoxPowerIdConst, List<int> BoxWaterIdConst,
            List<int> BoxAirIdConst)
        {
            _context.TableComboBoxes.UpdateRange(utilTableComboBox.getListByIdList(Id, BoxCostIdConst,
                BoxElectricityIdConst, BoxPowerIdConst, BoxWaterIdConst, BoxAirIdConst));
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
