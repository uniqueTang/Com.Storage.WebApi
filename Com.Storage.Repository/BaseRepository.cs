﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Storage.Models;
using System.Linq.Expressions;
using System.Data.Entity;

namespace Com.Storage.Repository
{
    /// <summary>
    /// 数据访问基类
    /// </summary>
    public class BaseRepository<T, Tkey> where T : class
    {
        DbContext dbContext = null;
        public DbContext MyDbContext
        {
            get
            {
                if (dbContext == null)
                {
                    //修改base中默认连接字符串
                    dbContext = new DbContext("name=JooWMSEntities");
                }
                return dbContext;
            }
        }

        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll()
        {
            return MyDbContext.Set<T>().ToList();
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<T> GetByWhere(Expression<Func<T, bool>> where)
        {
            return MyDbContext.Set<T>().Where(where).ToList();
        }

        /// <summary>
        /// 条件升序查询 带分页 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<T> GetByWhereAsc(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, ref int pageIndex, ref int count, ref int pageCount, int pageSize)
        {
            count = MyDbContext.Set<T>().Where(where).Count(); //总条数
            pageCount = count % pageSize == 0 ? count / pageSize : count / pageSize + 1; //总页数
            if (pageIndex <= 1 || count == 0) pageIndex = 1;
            else if (pageIndex >= pageCount) pageIndex = pageCount;

            var filterCount = (pageIndex - 1) * pageSize;
            return MyDbContext.Set<T>().Where(where).OrderBy(orderBy).Skip(filterCount).Take(pageSize).ToList();
        }

        /// <summary>
        /// 条件降序查询 带分页 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<T> GetByWhereDesc(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, ref int pageIndex, ref int count, ref int pageCount, int pageSize)
        {
            count = MyDbContext.Set<T>().Where(where).Count(); //总条数
            pageCount = count % pageSize == 0 ? count / pageSize : count / pageSize + 1; //总页数
            if (pageIndex <= 1 || count == 0) pageIndex = 1;
            else if (pageIndex >= pageCount) pageIndex = pageCount;

            var filterCount = (pageIndex - 1) * pageSize;
            return MyDbContext.Set<T>().Where(where).OrderByDescending(orderBy).Skip(filterCount).Take(pageSize).ToList();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(T model)
        {
            MyDbContext.Entry<T>(model).State = EntityState.Added;
            var result = MyDbContext.SaveChanges();
            return result > 0;
        }

        /// <summary>
        /// 修改
        /// 修改的实体 必须主键有值
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(T model)
        {
            MyDbContext.Entry<T>(model).State = EntityState.Modified;
            var result = MyDbContext.SaveChanges();
            return result > 0;
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(T model)
        {
            MyDbContext.Entry<T>(model).State = EntityState.Deleted;
            var result = MyDbContext.SaveChanges();
            return result > 0;
        }

        /// <summary>
        /// 查询返回单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find(object id)
        {
            return MyDbContext.Set<T>().Find(id);
        }
    }
}
