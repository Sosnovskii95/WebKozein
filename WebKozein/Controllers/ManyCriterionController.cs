using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebKozein.Data;
using WebKozein.Models.CodeFirst;
using WebKozein.Models.ComboBox;
using WebKozein.Models.FilterSortView;
using WebKozein.Models.Weight;
using WebKozein.Models.Pareto;
using WebKozein.Models.Hierarchy;
using Microsoft.AspNetCore.Http;
using WebKozein.Models.Serialization;

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
                dataBases = dataBases.Where(fp => fp.PowerTime <= fPowerTime);
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
            WeightAlternativ weightAlternativ = new WeightAlternativ(await _context.TableComboBoxes.ToListAsync());

            /*List<InformDataBase> list;

            if (GetListPareto().Count > 0 && GetOperation() == 2 || GetOperation() == 3)
            {
                list = GetListPareto();
            }
            else
            {
                list = await dataBases.AsNoTracking().ToListAsync();
                SetListPareto(list);
                SetMassAlternativ(weightAlternativ.GetMassAlternativ());
                SetOperation(1);
            }*/

            IndexViewModel viewModel = new IndexViewModel
            {
                InformDataBases = await dataBases.AsNoTracking().ToListAsync(),
                TableComboBoxes = await _context.TableComboBoxes.ToListAsync(),
                FilterViewModel = new FilterViewModel(fCost, fElectricity, fPower, fPowerTime, false),
                SortViewModel = new SortViewModel(sortOrder),
                WeightModel = new WeightModel
                {
                    CostWeight = weightAlternativ.GetMassAlternativ()[0],
                    ElectricityWeight = weightAlternativ.GetMassAlternativ()[1],
                    PowerWeight = weightAlternativ.GetMassAlternativ()[2],
                    WaterWeight = weightAlternativ.GetMassAlternativ()[3],
                    AirWeignt = weightAlternativ.GetMassAlternativ()[4],
                    BestCriteria = weightAlternativ.GetMassAlternativ()[weightAlternativ.GetIndexBestAlternativ()],
                    NameBestCriteria = GetNameBestCriteria(weightAlternativ.GetIndexBestAlternativ())
                }
            };

            return View(viewModel);
        }

        private double[] GetMassMainAlternativ()
        {
            double[] mass = HttpContext.Session.Get<double[]>("mass");
            return mass;
        }

        private void SetMassAlternativ(double[] massAlternativ)
        {
            HttpContext.Session.Set<double[]>("mass", massAlternativ);
        }

        private List<InformDataBase> GetListPareto()
        {
            List<InformDataBase> list = HttpContext.Session.Get<List<InformDataBase>>("Pareto");
            if (list == null)
            {
                list = new List<InformDataBase>();
            }
            return list;
        }

        private void SetListPareto(List<InformDataBase> list)
        {
            HttpContext.Session.Set<List<InformDataBase>>("Pareto", list);
        }

        private int GetOperation()
        {
            int? operation = HttpContext.Session.Get<int>("Operation");
            if (!operation.HasValue)
            {
                operation = 0;
            }
            return operation.Value;
        }

        private void SetOperation(int operation)
        {
            HttpContext.Session.Set<int>("Operation", operation);
        }

        private string GetNameBestCriteria(int index)
        {
            string name = "";
            switch (index)
            {
                case 0: name = "Цена"; break;
                case 1: name = "Электричество"; break;
                case 2: name = "Мощность"; break;
                case 3: name = "Вода"; break;
                case 4: name = "Воздух"; break;
                default: break;
            }
            return name;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(InformDataBase data)
        {
            if (ModelState.IsValid)
            {
                data.Weight = 0;
                await _context.InformDataBases.AddAsync(data);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(data);
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id.HasValue)
            {
                return View(await _context.InformDataBases.FindAsync(id.Value));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(InformDataBase data)
        {
            if (ModelState.IsValid)
            {
                data.Weight = 0;
                _context.InformDataBases.Update(data);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id.HasValue)
            {
                return View(await _context.InformDataBases.FindAsync(id.Value));
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
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
            return RedirectToAction(nameof(Index));
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

        public IActionResult Pareto()
        {
            List<InformDataBase> list = GetListPareto();

            if (GetOperation() == 1)
            {
                if (list.Count > 0)
                {
                    Pareto pareto = new Pareto();
                    list = pareto.ParetoSort(list);
                    SetListPareto(list);
                    SetOperation(2);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Hierarchy()
        {
            List<InformDataBase> list = GetListPareto();
            if (GetOperation() == 2)
            {
                if (list.Count > 0)
                {
                    HierarchyMethod Hierarchy = new HierarchyMethod(list, GetMassMainAlternativ());
                    SetListPareto(Hierarchy.GetInformDataBases());
                    SetOperation(3);
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
