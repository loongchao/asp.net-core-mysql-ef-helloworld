using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySQL.Data.EntityFrameworkCore.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class FactoryContext
    {
        public static string Connection;

        /// <summary>
        /// 创建一个数据库连接
        /// </summary>
        /// <returns></returns>
        public static MysqlContext Create()
        {
            //string connectionString = @"server=test-db.paiming.net;userid=root;pwd=6CE3644EDD7B868B;charset='gbk';port=3306;database=test_shove_netwin_website;sslmode=none";

            var optionsBuilder = new DbContextOptionsBuilder<MysqlContext>();

            optionsBuilder.UseMySQL(Connection);

            //Ensure database creation
            var mysqlContext = new MysqlContext(optionsBuilder.Options);

            mysqlContext.Database.EnsureCreated();

            return mysqlContext;
        }
    }
}
