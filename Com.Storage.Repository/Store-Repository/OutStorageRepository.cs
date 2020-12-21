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
    /// 出库数据访问层
    /// </summary>
    public class OutStorageRepository: BaseRepository<OutStorage, OutStorage>
    {
        public object GroupByPorduct()
        {
            var dbContext = new JooWMSEntities();
            var list = dbContext.OutStorage.GroupBy(item => new { item.CusNum, item.CusName, }).Select(item => new
            {
                Num = item.Sum(i => i.Num),
                CusNum = item.Key.CusNum,
                CusName = item.Key.CusName
            });
            return list;
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<OutStorage> GetOutStorageList(Expression<Func<OutStorage, bool>> where)
        {
            var dbContext = new JooWMSEntities();
            return dbContext.OutStorage.Where(where).ToList();
        }

        /// <summary>
        /// 出库单审核
        /// </summary>
        /// <param name="outStorage"></param>
        /// <returns></returns>
        public bool EditCheckOutStorage(OutStorage outStorage) 
        {
            var dbContext = new JooWMSEntities();
            var outStorage_ = dbContext.OutStorage.Find(outStorage.ID);
            outStorage_.Status = outStorage.Status;
            outStorage_.AuditeTime = DateTime.Now;
            outStorage_.Reason = outStorage.Reason;
            return dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteOutStorage(int ID)
        {
            var dbContext = new JooWMSEntities();
            var outStorage = dbContext.OutStorage.Find(ID);
            outStorage.IsDelete = 1;
            return dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 修改出库单数据
        /// </summary>
        /// <param name="outStorage"></param>
        /// <returns></returns>
        public bool EditOutStorage(OutStorage outStorage) 
        {
            var dbContext = new JooWMSEntities();
            var outStorage_ = dbContext.OutStorage.Find(outStorage.ID);
            outStorage_.ContractOrder = outStorage.ContractOrder;
            outStorage_.AuditeTime = DateTime.Now;
            outStorage_.CusNum = outStorage.CusNum;
            outStorage_.CusName = outStorage.CusName;
            outStorage_.Address = outStorage.Address;
            outStorage_.Contact = outStorage.Contact;
            outStorage_.Phone = outStorage.Phone;
            outStorage_.OutType = outStorage.OutType;
            outStorage_.Remark = outStorage.Remark;
            outStorage_.Num = outStorage.Num;
            outStorage_.Amount = outStorage.Amount;
            return dbContext.SaveChanges() > 0;
        }
    }
}
