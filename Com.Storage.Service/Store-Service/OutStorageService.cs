using Com.Storage.Models;
using Com.Storage.Repository.Store_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Service.Store_Service
{
    /// <summary>
    /// 出库业务层
    /// </summary>
    public class OutStorageService : BaseService<OutStorage, OutStorage>
    {
        public object GroupByPorduct()
        {
            var outStorageRepository = new OutStorageRepository();
            return outStorageRepository.GroupByPorduct();
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="cusName"></param>
        /// <param name="ContractOrder"></param>
        /// <param name="OutType"></param>
        /// <returns></returns>
        public List<OutStorage> GetOutStorageList(int Status, DateTime startTime, DateTime endTime, string cusName, string ContractOrder, int OutType)
        {
            var outStorageRepository = new OutStorageRepository();
            Expression<Func<OutStorage, bool>> where = item => item.CreateTime >= startTime && item.CreateTime <= endTime && item.IsDelete != 1;
            if (Status != 0) where = where.And(item => item.Status == Status);
            if (!string.IsNullOrEmpty(cusName)) where = where.And(item => item.CusName.IndexOf(cusName) != -1);
            if (!string.IsNullOrEmpty(ContractOrder)) where = where.And(item => item.ContractOrder.IndexOf(ContractOrder) != -1);
            if (OutType != 0) where = where.And(item => item.OutType == OutType);
            return outStorageRepository.GetOutStorageList(where);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="outStorage"></param>
        /// <returns></returns>
        public bool EditCheckOutStorage(OutStorage outStorage)
        {
            var outStorageRepository = new OutStorageRepository();
            return outStorageRepository.EditCheckOutStorage(outStorage);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteOutStorage(int ID)
        {
            var outStorageRepository = new OutStorageRepository();
            return outStorageRepository.DeleteOutStorage(ID);
        }

        /// <summary>
        /// 修改出库单信息
        /// </summary>
        /// <param name="outStorage"></param>
        /// <returns></returns>
        public bool EditOutStorage(OutStorage outStorage)
        {
            var outStorageRepository = new OutStorageRepository();
            return outStorageRepository.EditOutStorage(outStorage);
        }
    }
}
