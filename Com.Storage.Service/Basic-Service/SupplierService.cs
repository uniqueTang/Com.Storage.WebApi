using Com.Storage.Models;
using Com.Storage.Repository.Basic_Repository;
using Com.Storage.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Service.Basic_Service
{
    /// <summary>
    /// 供应商（Supplier）业务逻辑层
    /// </summary>
    public class SupplierService : BaseService<Supplier, Supplier>
    {
        public Supplier FindByRoleNum(string supName)
        {
            return GetByWhere(item => item.SupName.Equals(supName)).SingleOrDefault();
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="byName"></param>
        /// <returns></returns>
        public List<Supplier> GetBySupplierList(string byName)
        {
            var supplierRepository = new SupplierRepository();
            Expression<Func<Supplier, bool>> where = item => item.IsDelete != 1;
            if (!string.IsNullOrEmpty(byName)) where = where.And(item => item.SupNum.IndexOf(byName) != -1 || item.SupName.IndexOf(byName) != -1);
            return supplierRepository.GetBySupplierList(where);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns></returns>
        public bool EditSupplier(Supplier supplier)
        {
            var supplierRepository = new SupplierRepository();
            return supplierRepository.EditSupplier(supplier);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteSupplier(int ID)
        {
            var supplierRepository = new SupplierRepository();
            return supplierRepository.DeleteSupplier(ID);
        }
    }
}
