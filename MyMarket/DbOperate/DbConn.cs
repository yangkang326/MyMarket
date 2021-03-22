using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using FreeSql;
using MyMarket.Models;

namespace MyMarket.DbOperate
{
    public static class DbConn
    {
        public static IFreeSql fsql = new FreeSqlBuilder()
            .UseConnectionString(DataType.Sqlite, @"Data Source=db1.db")
            .UseAutoSyncStructure(true) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
            .Build();

        public static ObservableCollection<CargoInfoModel> GetCargoInfoModels(string groupname)
        {
            var Result = new ObservableCollection<CargoInfoModel>();
            if (groupname == "*" || groupname == "")
                Result = new ObservableCollection<CargoInfoModel>(fsql.Select<CargoInfoModel>().ToList());
            else
                Result = new ObservableCollection<CargoInfoModel>(fsql.Select<CargoInfoModel>()
                    .Where(c => c.PDGroup == groupname).ToList());
            return Result;
        }

        public static ObservableCollection<CargoInfoModel> GetCargoInfoModelsByString(string Inputstring)
        {
            var Result = new ObservableCollection<CargoInfoModel>();
            if (Inputstring == "*")
            {
                Result = new ObservableCollection<CargoInfoModel>(fsql.Select<CargoInfoModel>().ToList());
            }
            else
            {
                var temp1 = fsql.Select<CargoInfoModel>().Where(i => i.PDCode.Contains(Inputstring)).ToList();
                var temp2 = fsql.Select<CargoInfoModel>().Where(i => i.PDName.Contains(Inputstring)).ToList();
                var temp3 = fsql.Select<CargoInfoModel>().Where(i => i.PDSubName.Contains(Inputstring)).ToList();
                var temp4 = temp1.Union(temp2).ToList();
                var temp5 = temp4.Union(temp3).ToList();
                foreach (var CargoInfoModelitem in temp5)
                    if (Result.Where(i => i.PDCode == CargoInfoModelitem.PDCode).ToList().Count == 0)
                        Result.Add(CargoInfoModelitem);
            }

            return Result;
        }

        public static ObservableCollection<CargoInfoModel> InsertCargoInfoModels(CargoInfoModel newcargo)
        {
            var Result = new ObservableCollection<CargoInfoModel>();
            if (newcargo.PDCode.Length > 0 && newcargo.PDName.Length > 0 && newcargo.PDSubName.Length > 0 &&
                newcargo.PDSellPrice > 0)
            {
                if (fsql.Select<CargoInfoModel>().Where(c => c.PDCode == newcargo.PDCode).ToList().Count > 0)
                {
                    MessageBox.Show("商品序号重复！！！");
                    Result = new ObservableCollection<CargoInfoModel>(fsql.Select<CargoInfoModel>().ToList());
                }
                else
                {
                    fsql.Insert(new CargoInfoModel
                    {
                        IsCommunicationNeeded = newcargo.IsCommunicationNeeded,
                        IsVipDiscount = newcargo.IsVipDiscount,
                        IsVipPointInc = newcargo.IsVipPointInc,
                        IsWeighedNeeded = newcargo.IsWeighedNeeded,
                        PDCode = newcargo.PDCode,
                        PDCost = newcargo.PDCost,
                        PDGroup = newcargo.PDGroup,
                        PDName = newcargo.PDName,
                        PDProfit = newcargo.PDProfit,
                        PDSubName = newcargo.PDSubName,
                        PDStock = newcargo.PDStock,
                        PDSellPrice = newcargo.PDSellPrice,
                        PDSupplier = newcargo.PDSupplier,
                        WeighSN = newcargo.WeighSN,
                        PicPath = newcargo.PicPath,
                        PDUnit = newcargo.PDUnit
                    }).ExecuteAffrows();
                    Result = new ObservableCollection<CargoInfoModel>(fsql.Select<CargoInfoModel>().ToList());
                }
            }
            else
            {
                MessageBox.Show("商品信息输入不全");
            }

            return Result;
        }

        public static ObservableCollection<CargosGroup> GetCargosGroups()
        {
            return new(fsql.Select<CargosGroup>().ToList());
        }

        public static ObservableCollection<CargosGroup> InsertCargosGroups(string groupname)
        {
            var Result = new ObservableCollection<CargosGroup>();
            if (fsql.Select<CargoInfoModel>().Where(c => c.PDCode == groupname).ToList().Count > 0)
            {
                MessageBox.Show("产品组名重复！！！");
                Result = new ObservableCollection<CargosGroup>(fsql.Select<CargosGroup>().ToList());
            }
            else
            {
                fsql.Insert(new CargosGroup
                {
                    PDGroup = groupname
                }).ExecuteAffrows();
                Result = new ObservableCollection<CargosGroup>(fsql.Select<CargosGroup>().ToList());
            }

            return Result;
        }

        public static void SellCargos(
            ObservableCollection<CartItem> soldCargosCartItems)
        {
            foreach (var CartItem in soldCargosCartItems)
            {
                var item = fsql.Select<CargoInfoModel>().Where(i => i.PDCode == CartItem.PDSN).First();
                fsql.Update<CargoInfoModel>(item.ID).Set(i => i.PDStock, item.PDStock - CartItem.Count)
                    .ExecuteAffrows();
            }
        }
    }
}