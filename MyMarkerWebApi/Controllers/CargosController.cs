using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyLib;
using MyMarkerWebApi.DbOprate;

namespace MyMarkerWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CargosController : Controller
    {
        private readonly ILogger<CargosController> _logger;

        public CargosController(ILogger<CargosController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ObservableCollection<CargoInfoModel> InsertOrUpdateCargo([FromBody] CargoInfoModel newCargoInfoModel)
        {
            return DbHelper.InsertOrUpdateCargo(newCargoInfoModel);
        }

        [HttpPost]
        public ObservableCollection<CargosGroup> InsertOrUpdateCargoGroup(string newGroupName)
        {
            return DbHelper.InsertOrUpDateCargosGroups(newGroupName);
        }

        [HttpGet]
        public ObservableCollection<CargoInfoModel> GetCargosBySerchstring(string s)
        {
            return DbHelper.SearchCargosByString(s);
        }

        [HttpGet]
        public ObservableCollection<CargoInfoModel> GgeCargosByGroupname(string s)
        {
            return DbHelper.SearchCargosByGroup(s);
        }

        [HttpDelete]
        public ObservableCollection<CargoInfoModel> DeleCargo(CargoInfoModel cargo)
        {
            return DbHelper.DeleCargo(cargo);
        }
    }
}