using Microsoft.EntityFrameworkCore.Query;
using Entity;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public abstract class BaseService<TEntity> : IDisposable where TEntity : class, IEntity
    {
        #region 属性
        /// <summary>
        /// 
        /// </summary>
        private MysqlRepository<TEntity> mysqlRepository;
        /// <summary>
        /// MySql
        /// </summary>
        protected MysqlRepository<TEntity> MysqlRepository
        {
            get { return mysqlRepository; }
        }

        public BaseService()
        {

            mysqlRepository = new MysqlRepository<TEntity>();
        }
        #endregion

        #region 查询
        /// <summary>
        ///表集合
        /// </summary>
        public virtual IQueryable<TEntity> Entities
        {
            get
            {
                var val = mysqlRepository.Entities;
                return val;
            }
        }
        public virtual IQueryable<TEntity> QueryWhere(Expression<Func<TEntity, bool>> where)
        {   
             return mysqlRepository.QueryWhere(where);
               
        }
        /// <summary>
        /// 连表查询
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="navigationPropertyPaths">导航属性</param>
        /// <returns></returns>
        public virtual IQueryable<TEntity> QueryJoin(Expression<Func<TEntity, bool>> where, string[] navigationPropertyPaths)
        {

            return mysqlRepository.QueryJoin(where, navigationPropertyPaths);

        }
    
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页码大小</param>
        /// <param name="Source">数据源</param>
        /// <returns></returns>
        public IEnumerable<TEntity> QueryByPage(int pageIndex
       , int pageSize
       , IQueryable<TEntity> Source)
        {
            return mysqlRepository.QueryByPage(pageIndex, pageSize,  Source);
        }
        /// <summary>
        /// 总条数
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <returns></returns>
        public int Count(Expression<Func<TEntity, bool>> where)
        {
            return mysqlRepository.Count(where);
        }
        /// <summary>
        /// 主键查询实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindByIdAsync(int id)
        {
            return (await mysqlRepository.FindAsync(p => p.Id == id)).FirstOrDefault();
        }

        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool AnyById(int id)
        {

            return mysqlRepository.Entities.Any(p => p.Id == id);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            //            Task<TEntity> dddd = (await mysqlRepository.FindAsync(predicate)).FirstOrDefault();
            //async Task<TEntity> dddd = (await mysqlRepository.FindAsync(p => p.Id == 1)).FirstOrDefault();
            return mysqlRepository.FindAsync(predicate);
        }
        #endregion

        #region 新增

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual Task<int> InsertAsync(TEntity item)
        {
            item.CreateTime = DateTime.Now;
            item.UpdateTime = DateTime.Now;
            return mysqlRepository.InsertAsync(item);
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual Task InsertAsync(IEnumerable<TEntity> items)
        {
            foreach (var item in items)
            {
                item.CreateTime = DateTime.Now;
                item.UpdateTime = DateTime.Now;
            }
            return mysqlRepository.InsertAsync(items);
        }

        /// <summary>
        /// 延迟新增
        /// </summary>
        /// <param name="entity"></param>
        public void DelayedInsertAsync(TEntity item)
        {
            item.CreateTime = DateTime.Now;
            item.UpdateTime = DateTime.Now;
            mysqlRepository.DelayedInsertAsync(item);
        }
        /// <summary>
        /// 延迟批量添加
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public void Insert(IEnumerable<TEntity> items)
        {
            mysqlRepository.Insert(items);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task<int> DeleteAsync(int id)
        {
            return mysqlRepository.DeleteAsync(id);
        }


        /// <summary>
        /// 异步批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual Task BatchDeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
           return  mysqlRepository.BatchDeleteAsync(predicate);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual void BatchDelete(Expression<Func<TEntity, bool>> predicate)
        {
            mysqlRepository.BatchDelete(predicate);
        }
        /// <summary>
        /// 延迟删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public void DelayedDeleteAsync(TEntity entity)
        {
            mysqlRepository.DelayedDeleteAsync(entity);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public virtual int Update(TEntity item)
        {
            item.UpdateTime = DateTime.Now;
            return mysqlRepository.Update(item);
        }
        #endregion

        #region 保存
        /// <summary>
        /// 统一保存
        /// </summary>
        /// <returns></returns>
        public Task<int> SaveChangesAsync()
        {
            return mysqlRepository.SaveChangesAsync();
        }
        /// <summary>
        ///同步统一保存
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return mysqlRepository.SaveChanges();
        }
        #endregion

        #region Dispose
        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {

        } 
        #endregion
    }
}
