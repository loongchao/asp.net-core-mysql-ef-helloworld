
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    public class SalesERPDAL : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

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
    }
}
