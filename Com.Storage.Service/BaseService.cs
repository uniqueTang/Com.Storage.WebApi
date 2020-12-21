using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Storage.Models;
using Com.Storage.Repository;
using System.Linq.Expressions;

namespace Com.Storage.Service
{
    /// <summary>
    /// 业务逻辑基类
    /// </summary>
    public class BaseService<T, Tkey> where T : class
    {
        BaseRepository<T,Tkey> baseRepository = null;
        public BaseRepository<T, Tkey> MyRepository
        {
            get
            {
                if (baseRepository == null)
                {
                    baseRepository = new BaseRepository<T, Tkey>();
                }
                return baseRepository;
            }
        }
        /// <summary>
        /// 获取所有信息
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll()
        {
            return MyRepository.GetAll();
        }

        /// <summary>
        /// 条件查询
        /// 为了防止UI层可以随意构建条件，得到非法数据，故降低访问级别
        /// 如开放此方法，建议将条件构建为平面模型
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        protected List<T> GetByWhere(Expression<Func<T, bool>> where)
        {
            return MyRepository.GetByWhere(where);
        }
        /// <summary>
        /// 条件查询
        /// 为了防止UI层可以随意构建条件，得到非法数据，故降低访问级别
        /// 如开放此方法，建议将条件构建为平面模型
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        protected List<T> GetByWhereAsc(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, ref int pageIndex, ref int count, ref int pageCount, int pageSize)
        {
            return MyRepository.GetByWhereAsc(where, orderBy, ref pageIndex, ref count, ref pageCount, pageSize);
        }

        /// <summary>
        /// 条件降序查询 带分页 
        /// 为了防止UI层可以随意构建条件，得到非法数据，故降低访问级别
        /// 如开放此方法，建议将条件构建为平面模型
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        protected List<T> GetByWhereDesc(Expression<Func<T, bool>> where, Expression<Func<T, Tkey>> orderBy, ref int pageIndex, ref int count, ref int pageCount, int pageSize)
        {
            return MyRepository.GetByWhereDesc(where, orderBy, ref pageIndex, ref count, ref pageCount, pageSize);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(T model)
        {
            return MyRepository.Add(model);
        }

        /// <summary>
        /// 修改
        /// 修改的实体 必须主键有值
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(T model)
        {
            return MyRepository.Update(model);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(T model)
        {
            return MyRepository.Delete(model);
        }

        /// <summary>
        /// 查询返回单个实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Find(object id)
        {
            return MyRepository.Find(id);
        }
    }
}
