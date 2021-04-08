﻿using System.Collections.ObjectModel;
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
        public ObservableCollection<CargoInfoModel> InsertOrUpdateCargo([FromBody]CargoInfoModel newCargo)
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
        public ObservableCollection<CargoInfoModel> GetAllCargos()
        {
            var Result = new ObservableCollection<CargoInfoModel>(Client.Select<CargoInfoModel>().ToList());
            return Result;
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