using Com.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Repository.Basic_Repository
{
    /// <summary>
    /// 供应商（Supplier）数据访问层
    /// </summary>
    public class SupplierRepository : BaseRepository<Supplier, Supplier>
    {
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<Supplier> GetBySupplierList(Expression<Func<Supplier, bool>> where)
        {
            var dbContext = new JooWMSEntities();
            return dbContext.Supplier.Where(where).ToList();
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns></returns>
        public bool EditSupplier(Supplier supplier)
        {
            var dbContext = new JooWMSEntities();
            var supplier_ = dbContext.Supplier.Find(supplier.ID);
            supplier_.SupName = supplier.SupName;
            supplier_.SupType = supplier.SupType;
            supplier_.Phone = supplier.Phone;
            supplier_.Fax = supplier.Fax;
            supplier_.Email = supplier.Email;
            supplier_.ContactName = supplier.ContactName;
            supplier_.Address = supplier.Address;
            return dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteSupplier(int ID)
        {
            var dbContext = new JooWMSEntities();
            var supplier = dbContext.Supplier.Find(ID);
            supplier.IsDelete = 1;
            return dbContext.SaveChanges() > 0;
        }
    }
}
