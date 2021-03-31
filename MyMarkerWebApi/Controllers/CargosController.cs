using System.Collections.ObjectModel;
using System.Linq;
using FreeSql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyLib;

namespace MyMarkerWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CargosController : Controller
    {
        public static IFreeSql Client = new FreeSqlBuilder().UseConnectionString(DataType.MySql,
                "Data Source=192.168.0.191;Port=3306;User ID=root;Password=yangkang; Initial Catalog=MyMarket;Charset=utf8; SslMode=none;Min pool size=1")
            .UseAutoSyncStructure(true).Build();

        private readonly ILogger<CargosController> _logger;

        public CargosController(ILogger<CargosController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public ObservableCollection<CargoInfoModel> InsertOrUpdateCargo([FromBody] CargoInfoModel newCargo)
        {
            var cnt = Client.Select<CargoInfoModel>().Where(i => i.PDCode == newCargo.PDCode).First();
            if (cnt != null)
            {
                newCargo.ID = cnt.ID;
                Client.Update<CargoInfoModel>().Where(i => i.ID == cnt.ID).SetSource(newCargo)
                    .IgnoreColumns(a => new {a.PDCode, a.ID})
                    .ExecuteAffrows();
            }
            else
            {
                Client.Insert(newCargo)
                    .ExecuteAffrows();
            }

            return new ObservableCollection<CargoInfoModel>(Client.Select<CargoInfoModel>().ToList());
        }

        [HttpPost]
        public ObservableCollection<CargosGroup> InsertOrUpdateCargoGroup(string newGroupname)
        {
            if (Client.Select<CargosGroup>().Where(i => i.PDGroup == newGroupname).ToList().Count == 0)
                Client.Insert(new CargosGroup
                {
                    PDGroup = newGroupname
                }).ExecuteAffrows();
            return new ObservableCollection<CargosGroup>(Client.Select<CargosGroup>().ToList());
        }

        [HttpPost]
        public double CheckStock(string pdcode)
        {
            if (Client.Select<CargoInfoModel>().Where(i => i.PDCode == pdcode).First() != null)
                return Client.Select<CargoInfoModel>().Where(i => i.PDCode == pdcode).First().PDStock;
            return 0;
        }

        [HttpGet]
        public ObservableCollection<CargoInfoModel> GetCargosBySerchstring(string searchstr)
        {
            var result = new ObservableCollection<CargoInfoModel>();
            if (string.IsNullOrEmpty(searchstr))
            {
                result = new ObservableCollection<CargoInfoModel>(Client.Select<CargoInfoModel>().ToList());
            }
            else
            {
                var temp1 = Client.Select<CargoInfoModel>().Where(i => i.PDCode.Contains(searchstr)).ToList();
                var temp2 = Client.Select<CargoInfoModel>().Where(i => i.PDName.Contains(searchstr)).ToList();
                var temp3 = Client.Select<CargoInfoModel>().Where(i => i.PDSubName.Contains(searchstr)).ToList();
                var temp4 = temp1.Union(temp2).ToList();
                var temp5 = temp4.Union(temp3).ToList();
                foreach (var CargoInfoModelitem in temp5)
                    if (result.Where(i => i.PDCode == CargoInfoModelitem.PDCode).ToList().Count == 0)
                        result.Add(CargoInfoModelitem);
            }

            return result;
        }

        [HttpGet]
        public ObservableCollection<CargoInfoModel> GetCargosByGroupname(string groupname)
        {
            var result = new ObservableCollection<CargoInfoModel>();
            if (string.IsNullOrEmpty(groupname))
                result = new ObservableCollection<CargoInfoModel>(Client.Select<CargoInfoModel>().ToList());
            else
                result = new ObservableCollection<CargoInfoModel>(Client.Select<CargoInfoModel>()
                    .Where(i => i.PDGroup == groupname).ToList());
            return result;
        }

        [HttpGet]
        public ObservableCollection<CargosGroup> GetAllGroup()
        {
            return new(Client.Select<CargosGroup>().ToList());
        }

        [HttpDelete]
        public ObservableCollection<CargoInfoModel> DeleCargo(string pdcode)
        {
            Client.Delete<CargoInfoModel>().Where(i => i.PDCode == pdcode).ExecuteAffrows();
            return new ObservableCollection<CargoInfoModel>(Client.Select<CargoInfoModel>().ToList());
        }
    }
}