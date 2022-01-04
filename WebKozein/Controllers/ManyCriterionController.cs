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

        public async Task<IActionResult> Index(SortState sortOrder, int? fCost, int? fElectricity, int? fPower, int? fPowerTime,
            bool flagFilter, bool flagPareto, bool flagHeararchy, bool fRule)
        {
            IQueryable<InformDataBase> dataBases = _context.InformDataBases;

            if (GetFlags(fCost, fElectricity, fPower, fPowerTime))
            {
                dataBases = dataBases.Where(fc => fc.Cost <= fCost).
                    Where(fe => fe.Electricity <= fElectricity).
                    Where(fp => fp.Power >= fPower).
                    Where(fp => fp.PowerTime <= fPowerTime);

                if (fRule) { sortOrder = SortState.ElectricityAsc; }
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

            if (flagFilter)
            {
                ViewBag.FlagFilter = flagFilter;
            }
            else if (GetFlags(fCost, fElectricity, fPower, fPowerTime))
            {
                ViewBag.FlagFilter = true;
            }
            else
            {
                ViewBag.FlagFilter = false;
            }
            if (flagPareto)
            {
                ViewBag.FlagPareto = flagPareto;
            }
            else
            {
                ViewBag.FlagPareto = flagPareto;
            }
            if (flagHeararchy)
            {
                ViewBag.Weight = flagHeararchy;
            }
            else
            {
                ViewBag.Weight = false;
            }

            /*if (flagFilter.HasValue)
            {
                    ViewBag.FlagFilter = flagFilter.Value;
            }
            else if (GetFlags(fCost, fElectricity, fPower, fPowerTime))
            {
                ViewBag.FlagFilter = true;
            }
            else
            {
                ViewBag.FlagFilter = null;
            }
            if (flagPareto.HasValue)
            {
                ViewBag.FlagPareto = flagPareto.Value;
            }
            if (flagHeararchy.HasValue)
            {
                ViewBag.Weight = flagHeararchy.Value;
            }*/

            /*ViewBag.FlagSearch = GetFlags(fCost, fElectricity, fPower, fPowerTime);

            if (flagPareto.HasValue)
            {
                ViewBag.Filter = flagPareto.Value;
                //ViewData["flagPareto"] = flagPareto.Value;
            }
            if (flagHeararchy.HasValue)
            {
                ViewBag.Pareto = flagHeararchy.Value;
                ViewBag.Weight = flagHeararchy.Value;
            }*/

            List<InformDataBase> list;

            if (flagPareto.HasValue)
            {
                if (flagPareto.Value == true)
                {
                    list = GetList();
                }
                else
                {
                    list = await dataBases.AsNoTracking().ToListAsync();
                    SetList(list);
                }
            }
            else if (flagHeararchy.HasValue)
            {
                if (flagHeararchy.Value == true)
                {
                    list = GetList();
                }
                else
                {
                    list = await dataBases.AsNoTracking().ToListAsync();
                    SetList(list);
                }
            }
            else
            {
                list = await dataBases.AsNoTracking().ToListAsync();
                SetList(list);
            }

            IndexViewModel viewModel = new IndexViewModel
            {
                InformDataBases = list,
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

        private List<InformDataBase> GetList()
        {
            List<InformDataBase> list = HttpContext.Session.Get<List<InformDataBase>>("List");
            if (list == null)
            {
                list = new List<InformDataBase>();
            }
            return list;
        }

        private void SetList(List<InformDataBase> list)
        {
            HttpContext.Session.Set<List<InformDataBase>>("List", list);
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

        public IActionResult Pareto(bool? flagFilter, int? fCost, int? fElectricity, int? fPower, int? fPowerTime)
        {
            bool? result = null;

            if (flagFilter.HasValue)
            {
                if (flagFilter.Value)
                {
                    List<InformDataBase> list = GetList();

                    if (list.Count > 0)
                    {
                        result = true;

                        Pareto pareto = new Pareto();
                        list = pareto.ParetoSort(list);
                        SetList(list);
                    }
                }
            }

            /*bool flag = Convert.ToBoolean(flagFilter);
            if (flag)
            {
                List<InformDataBase> list = GetList();

                if (list.Count > 0)
                {
                    Pareto pareto = new Pareto();
                    list = pareto.ParetoSort(list);
                    SetList(list);
                }
                else
                {
                    flag = false;
                }
            }*/
            return RedirectToAction(nameof(Index), new { fCost = fCost, fElectricity = fElectricity, fPower = fPower, fPowerTime = fPowerTime, flagFilter = flagFilter, flagPareto = result });
        }

        public IActionResult Hierarchy(bool? flagFilter, bool? flagPareto, int? fCost, int? fElectricity, int? fPower, int? fPowerTime)
        {
            bool result = false;
            if (flagFilter.HasValue)
            {
                if (flagFilter.Value)
                {
                    if (flagPareto.HasValue)
                    {
                        if (flagPareto.Value)
                        {
                            List<InformDataBase> list = GetList();
                            double[] mass = GetMass();

                            if (list.Count > 0)
                            {
                                if (mass != null)
                                {
                                    result = true;

                                    HierarchyMethod hierarchy = new HierarchyMethod(list, mass);
                                    list = hierarchy.GetInformDataBases();
                                    SetList(list);
                                }
                            }
                        }
                    }
                }
            }

            /*bool? flag = Convert.ToBoolean(flagPareto);
            bool result = false;

            if (flag.HasValue)
            {
                if (flag.Value)
                {
                    List<InformDataBase> list = GetList();
                    double[] mass = GetMass();

                    if (list.Count > 0)
                    {
                        result = true;
                        if (mass != null)
                        {
                            result = true;
                            HierarchyMethod Hierarchy = new HierarchyMethod(list, mass);
                            list = Hierarchy.GetInformDataBases();
                            SetList(list);
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    else
                    {
                        result = false;
                    }
                }
            }*/
            return RedirectToAction(nameof(Index), new { fCost = fCost, fElectricity = fElectricity, fPower = fPower, fPowerTime = fPowerTime, flagFilter = flagFilter, flagPareto = flagPareto, flagHeararchy = result });
        }

        public IActionResult Reset()
        {
            SetList(new List<InformDataBase>());
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
