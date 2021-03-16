using FreeSql;

namespace MyMarket.DbOperate
{
    public static class DbConn
    {
        public static IFreeSql fsql = new FreeSqlBuilder()
            .UseConnectionString(DataType.Sqlite, @"Data Source=db1.db")
            .UseAutoSyncStructure(true) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
            .Build();
    }
}