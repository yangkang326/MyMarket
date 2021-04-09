using System.Collections.ObjectModel;
using System.Threading.Tasks;
using FreeSql;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyLib;

namespace MyWebAPI.Controllers
{
    [ApiController, Route("api/[controller]/[action]")]
    public class CargosController : Controller
    {
        public static IFreeSql Client = new FreeSqlBuilder().UseConnectionString(DataType.Sqlite, @"Data Source=db1.db").UseAutoSyncStructure(true).Build();
        private readonly ILogger<CargosController> _Logger;

        public CargosController(ILogger<CargosController> logger)
        {
            _Logger = logger;
        }

        [HttpPost]
        public Task<ObservableCollection<CargoInfoModel>> InsertOrUpdateCargo([FromBody]CargoInfoModel newCargo)
        {
            var Cnt = Client.Select<CargoInfoModel>().Where(i => i.PDCode == newCargo.PDCode).First();
            if (Cnt != null)
            {
                newCargo.PDId = Cnt.PDId;
                Client.Update<CargoInfoModel>().Where(i => i.PDId == Cnt.PDId).SetSource(newCargo).IgnoreColumns(a => new
                {
                    a.PDCode,
                    ID = a.PDId
                }).ExecuteAffrows();
            }
            else
            {
                Client.Insert(newCargo).ExecuteAffrows();
            }

            return Task.FromResult(new ObservableCollection<CargoInfoModel>(Client.Select<CargoInfoModel>().ToList()));
        }

        [HttpPost]
        public Task<ObservableCollection<CargosGroup>> InsertOrUpdateCargoGroup(string newGroupname)
        {
            if (Client.Select<CargosGroup>().Where(i => i.PDGroup == newGroupname).ToList().Count == 0)
                Client.Insert(new CargosGroup
                {
                    PDGroup = newGroupname
                }).ExecuteAffrows();
            return Task.FromResult(new ObservableCollection<CargosGroup>(Client.Select<CargosGroup>().ToList()));
        }

        [HttpPost]
        public Task<double> CheckStock(string pdcode)
        {
            double stock = 0;
            if (Client.Select<CargoInfoModel>().Where(i => i.PDCode == pdcode).First() != null)
                stock = Client.Select<CargoInfoModel>().Where(i => i.PDCode == pdcode).First().PDStock;
            return Task.FromResult(stock);
        }

        [HttpGet]
        public Task<ObservableCollection<CargoInfoModel>> GetAllCargos()
        {
            var Result = new ObservableCollection<CargoInfoModel>(Client.Select<CargoInfoModel>().ToList());
            return Task.FromResult(Result);
        }


        [HttpGet]
        public Task<ObservableCollection<CargosGroup>> GetAllGroup()
        {
            return Task.FromResult(new ObservableCollection<CargosGroup>(Client.Select<CargosGroup>().ToList()));
        }

        [HttpPost]
        public Task<ObservableCollection<CargoInfoModel>> DeleCargo(string pdcode)
        {
            Client.Delete<CargoInfoModel>().Where(i => i.PDCode == pdcode).ExecuteAffrows();
            return Task.FromResult(new ObservableCollection<CargoInfoModel>(Client.Select<CargoInfoModel>().ToList()));
        }

        [HttpPost("UploadFile"), Consumes("multipart/form-data")] //这里写你自己上传文件用的url地址
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var a = file.FileName;
            using (var fs = System.IO.File.Create(@"C:\\Users\\Administrator\\Documents\\Image\\" + a)) //filePath写文件保存到哪
            {
                await file.CopyToAsync(fs);
            }

            return NoContent();
        }
    }
}