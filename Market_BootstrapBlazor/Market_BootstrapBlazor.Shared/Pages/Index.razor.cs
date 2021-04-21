using Microsoft.AspNetCore.Components;
using MyLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Market_BootstrapBlazor.Shared.Pages
{
    public partial class Index:ComponentBase
    {
        [Inject]
        public static string s1 { get; set; }
        private  ObservableCollection<CargoInfoModel> Cargos { get; set; }
        private static Task<CargoInfoModel> OnAddAsync() => Task.FromResult(new CargoInfoModel() { PDName = DateTime.Now.Day.ToString() });
        protected override void OnInitialized()
        {
            base.OnInitialized();
            WebApiOperate.StatiCargoInfoModels = WebApiOperate.GetAllCargoInfoModels().Result;
            WebApiOperate.StatiCargosGroups = WebApiOperate.GetAllGroup().Result;
            WebApiOperate.StatiCargosUnits = WebApiOperate.GetAllUnit().Result;
            Cargos = GetCollection().Result;
            s1 = "6666";
        }
        private Task<bool> OnSaveAsync(CargoInfoModel item)
        {
            // 增加数据演示代码
            if (item.PDId == 0)
            {
                item.PDId = Cargos.Max(i => i.PDId) + 1;
                Cargos.Add(item);
            }
            else
            {
                WebApiOperate.StatiCargoInfoModels = WebApiOperate.InserOrUpdateCargo(item).Result;
                    Cargos = WebApiOperate.StatiCargoInfoModels;
            }
            return Task.FromResult(true);
        }
        private Task<bool> OnDeleteAsync(IEnumerable<CargoInfoModel> items)
        {
            foreach (var item in items)
            {
                Cargos.Remove(item);
            }

            return Task.FromResult(true);
        }
        public static  Task<ObservableCollection<CargoInfoModel>> GetCollection()
        {
            var temp = new ObservableCollection<CargoInfoModel>();
            temp = WebApiOperate.StatiCargoInfoModels;
            return Task.FromResult(temp);
        }
    }
}
