using Com.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Repository.Store_Repository
{
    /// <summary>
    /// 入库（InStorage）数据访问层
    /// </summary>
   public class InStorageRepository:BaseRepository<InStorage,InStorage>
   {
        public object GroupByPorduct() 
        {
            var dbContext = new JooWMSEntities();
            var list = dbContext.InStorage.GroupBy(item => new { item.SupNum, item.SupName, }).Select(item => new
            {
                Num = item.Sum(i => i.Num),
                SupNum = item.Key.SupNum,
                SupName = item.Key.SupName
            });
            return list;
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<InStorage> GetInStorageList(Expression<Func<InStorage, bool>> where) 
        {
            var dbContext = new JooWMSEntities();
            return dbContext.InStorage.Where(where).ToList();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteInStorage(int ID) 
        {
            var dbContext = new JooWMSEntities();
            var inStorage = dbContext.InStorage.Find(ID);
            inStorage.IsDelete = 1;
            return dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 修改审核入库单号
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        public bool EditCheckInStorage(InStorage storage) 
        {
            var dbContext = new JooWMSEntities();
            var storage_ = dbContext.InStorage.Find(storage.ID);
            storage_.Status = storage.Status;
            storage_.AuditeTime = DateTime.Now;
            storage_.Reason = storage.Reason;
            return dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 修改订单主表信息
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        public bool EditInStorage(InStorage storage) 
        {
            var dbContext = new JooWMSEntities();
            var storage_ = dbContext.InStorage.Find(storage.ID);
            storage_.InType = storage.InType;
            storage_.SupNum = storage.SupNum;
            storage_.Num = storage.Num;
            storage_.Amount = storage.Amount;
            storage_.SupName = storage.SupName;
            storage_.ContactName = storage.ContactName;
            storage_.Phone = storage.Phone;
            storage_.Remark = storage.Remark;
            return dbContext.SaveChanges() > 0;
        }
   }
}
