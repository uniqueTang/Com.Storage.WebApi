using Com.Storage.Service;
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
    /// 入库业务逻辑层
    /// </summary>
    public class InStorageService : BaseService<InStorage, InStorage>
    {

        public object GroupByPorduct()
        {
            var inStorageRepository = new InStorageRepository();
            return inStorageRepository.GroupByPorduct();
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="Status">审核状态</param>
        /// <param name="startTime">起始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="supName">供应商</param>
        /// <param name="ContractNum">关联单号</param>
        /// <param name="Intype">入库类型</param>
        /// <returns></returns>
        public List<InStorage> GetInStorageList(int Status, DateTime startTime, DateTime endTime, string supName, string ContractNum, int InType)
        {
            var inStorageRepository = new InStorageRepository();
            Expression<Func<InStorage, bool>> where = item => item.CreateTime >= startTime && item.CreateTime <= endTime && item.IsDelete != 1;
            if (Status != 0) where = where.And(item => item.Status == Status);
            if (!string.IsNullOrEmpty(supName)) where = where.And(item => item.SupName.IndexOf(supName) != -1);
            if (!string.IsNullOrEmpty(ContractNum)) where = where.And(item => item.ContractOrder.IndexOf(ContractNum) != -1);
            if (InType != 0) where = where.And(item => item.InType == InType);
            return inStorageRepository.GetInStorageList(where);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteInStorage(int ID)
        {
            var inStorageRepository = new InStorageRepository();
            return inStorageRepository.DeleteInStorage(ID);
        }

        /// <summary>
        ///  入户单号审核修改
        /// </summary>
        /// <returns></returns>
        public bool EditCheckInStorage(InStorage storage)
        {
            var inStorageRepository = new InStorageRepository();
            return inStorageRepository.EditCheckInStorage(storage);
        }

        /// <summary>
        /// 修改入库主单信息
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        public bool EditInStorage(InStorage storage)
        {
            var inStorageRepository = new InStorageRepository();
            return inStorageRepository.EditInStorage(storage);
        }
    }
}
