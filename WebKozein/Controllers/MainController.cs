using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebKozein.Data;
using WebKozein.Models.ComboBox;

namespace WebKozein.Controllers
{
    public class MainController : Controller
    {
        private readonly InformDbContext _context;

        public MainController(InformDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(List<int> Id, List<int> BoxCostIdConst, List<int> BoxElectricityIdConst,
            List<int> BoxPowerIdConst, List<int> BoxWaterIdConst, List<int> BoxAirIdConst)
        {
            List<TableComboBox> list = new List<TableComboBox>(10);
            for(int i = 0; i < Id.Count; i++)
            {
                list.Add(GetTableComboBoxFromId(Id[i], BoxCostIdConst[i], BoxElectricityIdConst[i], BoxPowerIdConst[i], BoxWaterIdConst[i], BoxAirIdConst[i]));
            }
            _context.UpdateRange(list);
            _context.SaveChanges();
            return View();
        }

        private TableComboBox GetTableComboBoxFromId(int id, int costId, int electricityId, int powerId, 
            int waterId, int airId)
        {
            TableComboBox box = new TableComboBox
            {
                Id = id,
                BoxCostIdConst = costId,
                BoxElectricityIdConst = electricityId,
                BoxPowerIdConst = powerId,
                BoxWaterIdConst = waterId,
                BoxAirIdConst = airId
            };
            return box;
        }
    }
}
