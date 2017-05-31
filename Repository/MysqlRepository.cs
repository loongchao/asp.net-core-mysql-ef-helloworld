using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
   
    public class MysqlRepository<TEntity> : IDisposable where TEntity : class, IEntity
    {
        #region 属性
        public MysqlContext context;

        public DbSet<TEntity> dbSet;

        public MysqlRepository()
        {
            this.context = FactoryContext.Create();

            this.dbSet = context.Set<TEntity>();
        }

        internal DbSet<TEntity> Collection
        {
            get { return dbSet; }
        } 
        #endregion

        #region 查询
        public IQueryable<TEntity> Entities
        {
            get { return dbSet.AsQueryable(); }
        }

        public IQueryable<TEntity> QueryWhere(Expression<Func<TEntity, bool>> where)
        {
            //var list = context..Where(where).ToList();
            return dbSet.Where(where).AsQueryable();
        }

        /// <summary>
        /// 连表查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="navigationPropertyPaths">导航属性</param>
        /// <returns></returns>
        public IQueryable<TEntity> QueryJoin(Expression<Func<TEntity, bool>> where, string[] navigationPropertyPaths)
        {
            //1.0 参数合法性验证
            if (navigationPropertyPaths == null || navigationPropertyPaths.Any() == false)
            {
                throw new Exception("至少要有一个表");
            }

            //2.0 遍历tableNames
            IQueryable<TEntity> query = dbSet;
            foreach (var navigationPropertyPath in navigationPropertyPaths)
            {
                query = query.Include(navigationPropertyPath);
            }

            //3.0 条件连表查询
            return query.Where(where);
        }
     
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页码大小</param>
        /// <param name="Source">数据源</param>
        /// <returns></returns>
        public IEnumerable<TEntity> QueryByPage(int pageIndex
          , int pageSize,
         IQueryable<TEntity> Source)
        {
            //计算出要跳过的总条数
            int skipCount = (pageIndex - 1) * pageSize; 
            //2.0 返回分页数据
            return Source.ToList().Skip(skipCount).Take(pageSize);
        }

        /// <summary>
        /// 总条数
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> where)
        {

            return dbSet.Count(where);
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return (await (dbSet.Where(predicate)).ToListAsync());
        }

        #endregion

        #region 新增
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<int> InsertAsync(TEntity entity)
        {
            dbSet.Add(entity);
            return context.SaveChangesAsync();
        }
        /// <summary>
        /// 延迟新增
        /// </summary>
        /// <param name="entity"></param>
        public void DelayedInsertAsync(TEntity entity)
        {
            dbSet.Add(entity);          
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public Task InsertAsync(IEnumerable<TEntity> items)
        {
            dbSet.AddRange(items);
            return context.SaveChangesAsync();
        }
        /// <summary>
        /// 延迟批量添加
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public void Insert(IEnumerable<TEntity> items)
        {
            dbSet.AddRange(items);      
        }
        #endregion

        #region 删除
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<int> DeleteAsync(int key)
        {
            var newentity = dbSet.FirstOrDefault(c => c.Id == key);
            if (newentity != null)
            {
                dbSet.Remove(newentity);
            }
            return context.SaveChangesAsync();
        }


        /// <summary>
        /// 延迟删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void DelayedDeleteAsync(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task<int> DeleteAsync(TEntity entity)
        {
            return DeleteAsync(entity);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var newentity = dbSet.FirstOrDefault(predicate);
            if (newentity != null)
            {
                dbSet.Remove(newentity);
            }
            return context.SaveChangesAsync();
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public void  BatchDelete(Expression<Func<TEntity, bool>> predicate)
        {
            var newentitys = dbSet.AsQueryable().Where(predicate);
            if (newentitys != null)
            {
                dbSet.RemoveRange(newentitys);
            }
            context.SaveChanges();
        }
        /// <summary>
        /// 异步批量删除
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public Task BatchDeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var newentitys = dbSet.AsQueryable().Where(predicate);
            if (newentitys != null)
            {
                dbSet.RemoveRange(newentitys);
            }
          return  context.SaveChangesAsync();
        }
        #endregion

        #region 修改

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Update(TEntity entity)
        {          
            context.Update(entity);
             return  context.SaveChanges();
                  
        }

        #endregion

        #region 保存
        /// <summary>
        ///异步统一保存
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChangesAsync()
        {
            return context.SaveChangesAsync();
        }
        /// <summary>
        ///同步统一保存
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return context.SaveChanges();
        }
        #endregion

        #region Dispose

        /// <summary>
        /// 
        /// </summary>
        /// 
        public void Dispose()
        {
            context.Dispose();
        } 
        #endregion
    }
}
