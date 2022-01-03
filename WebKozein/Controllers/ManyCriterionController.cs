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

        public async Task<IActionResult> Index(SortState sortOrder, int? fCost, int? fElectricity, int? fPower, int? fPowerTime, bool fRule,
            bool pareto, bool hierarchy)
        {
            bool filter = false;
            IQueryable<InformDataBase> dataBases = _context.InformDataBases;

            if (GetFlags(fCost, fElectricity, fPower, fPowerTime))
            {
                dataBases = dataBases.Where(fc => fc.Cost <= fCost).
                    Where(fe => fe.Electricity <= fElectricity).
                    Where(fp => fp.Power >= fPower).
                    Where(fp => fp.PowerTime <= fPowerTime);

                if (fRule) { sortOrder = SortState.ElectricityAsc; }
                filter = true;
            }
            else
            {
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
                    dataBases = dataBases.Where(fp => fp.Power >= fPower);
                }
                if (fPowerTime.HasValue)
                {
                    dataBases = dataBases.Where(fp => fp.PowerTime <= fPowerTime);
                }
                filter = false;
            }

            if (pareto)
            {
                if (filter)
                {
                    Pareto paretoSort = new Pareto();
                    List<InformDataBase> paretoList = await dataBases.AsNoTracking().ToListAsync();
                    paretoList = paretoSort.ParetoSort(paretoList);

                    dataBases = dataBases.Where(p => paretoList.Select(s => s.Id).Contains(p.Id));
                }
                else
                {
                    ViewBag.FlagFilter = false;
                }
            }

            if (hierarchy)
            {
                if (filter)
                {
                    double[] mass = GetMass();

                    if (mass != null)
                    {
                        List<InformDataBase> hierarchyList = await dataBases.ToListAsync();
                        HierarchyMethod hierarchyMethod = new HierarchyMethod(hierarchyList, mass);
                        hierarchyList = hierarchyMethod.GetInformDataBases();

                        dataBases = dataBases.Where(p => hierarchyList.Select(s => s.Id).Contains(p.Id));
                        List<InformDataBase> dataList = await dataBases.ToListAsync();

                        for (int i = 0; i < hierarchyList.Count; i++)
                        {
                            dataList[i].Weight = hierarchyList[i].Weight;
                        }

                        _context.UpdateRange(dataList);
                        _context.SaveChanges();

                    }
                    else
                    {
                        ViewBag.Weight = false;
                    }
                }
                else
                {
                    ViewBag.FlagPareto = false;
                }
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
                SortState.WeightAsc => dataBases.OrderBy(s => s.Weight),
                SortState.WeightDesc => dataBases.OrderByDescending(s => s.Weight),
                _ => dataBases
            };

            IndexViewModel viewModel = new IndexViewModel
            {
                InformDataBases = await dataBases.ToListAsync(),
                FilterViewModel = new FilterViewModel(fCost, fElectricity, fPower, fPowerTime, fRule),
                SortViewModel = new SortViewModel(sortOrder),
            };

            return View(viewModel);
        }

        private bool GetFlags(int? cost, int? electricity, int? power, int? powerTime)
        {
            bool result = cost.HasValue ? true : false;
            result = electricity.HasValue ? true : false;
            result = power.HasValue ? true : false;
            result = powerTime.HasValue ? true : false;
            return result;
        }

        private double[] GetMass()
        {
            double[] mass = HttpContext.Session.Get<double[]>("Mass");
            return mass;
        }

        private void SetMass(double[] massAlternativ)
        {
            HttpContext.Session.Set<double[]>("Mass", massAlternativ);
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

            return RedirectToAction(nameof(Weight));
        }

        public async Task<IActionResult> Reset()
        {
            List<InformDataBase> list = await _context.InformDataBases.ToListAsync();

            foreach (var item in list)
            {
                item.Weight = 0;
            }

            _context.UpdateRange(list);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Weight()
        {
            ViewBag.constCombo = utilConstComboBox.getConstComboBoxes();
            WeightAlternativ weightAlternativ = new WeightAlternativ(await _context.TableComboBoxes.ToListAsync());

            WeightViewModel weightViewModel = new WeightViewModel
            {
                TableComboBoxes = await _context.TableComboBoxes.ToListAsync(),
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

            SetMass(weightAlternativ.GetMassAlternativ());

            return View(weightViewModel);
        }
    }
}
