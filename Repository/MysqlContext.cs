using Microsoft.EntityFrameworkCore;
using Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class MysqlContext : DbContext
    {
        #region 属性
        public MysqlContext(DbContextOptions<MysqlContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employee { set; get; }
        #endregion

        #region 模型构造器
        /// <summary>
        /// 模型构造器
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 指定实体对应的数据库表
            modelBuilder.Entity<Employee>().ToTable("T_Employee");
            #endregion

            base.OnModelCreating(modelBuilder);
        }
        #endregion

    }
}
