﻿using System.Collections.ObjectModel;
using System.Windows;
using FreeSql;
using MyMarket.Models;
using Org.BouncyCastle.Bcpg.Sig;

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
            if (groupname=="*")
            {
                Result =new ObservableCollection<CargoInfoModel>(fsql.Select<CargoInfoModel>().ToList());
            }
            else
            {
                Result = new ObservableCollection<CargoInfoModel>(fsql.Select<CargoInfoModel>().Where(c=>c.PDGroup==groupname).ToList());
            }
            return Result;
        }
        public static ObservableCollection<CargoInfoModel> InsertCargoInfoModels(CargoInfoModel newcargo)
        {
            var Result = new ObservableCollection<CargoInfoModel>();
            if (fsql.Select<CargoInfoModel>().Where(c=>c.PDCode==newcargo.PDCode).ToList().Count > 0)
            {
                MessageBox.Show("商品序号重复！！！");
                Result = new ObservableCollection<CargoInfoModel>(fsql.Select<CargoInfoModel>().ToList());
            }
            else
            {
                fsql.Insert<CargoInfoModel>(new CargoInfoModel()
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
            return Result;
        }

        public static ObservableCollection<CargosGroup> GetCargosGroups()
        {
            return new ObservableCollection<CargosGroup>(fsql.Select<CargosGroup>().ToList());
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
                fsql.Insert<CargosGroup>(new CargosGroup()
                {
                    PDGroup = groupname
                }).ExecuteAffrows();
                Result = new ObservableCollection<CargosGroup>(fsql.Select<CargosGroup>().ToList());
            }
            return Result;
        }
    }
}