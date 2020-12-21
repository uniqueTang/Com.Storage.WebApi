using Com.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Repository.Store_Repository
{
    /// <summary>
    /// 移库详情数据访问层
    /// </summary>
    public class MoveOrderDetailRepository:BaseRepository<MoveOrderDetail, MoveOrderDetail>
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteDetail(int ID)
        {
            var dbContext = new JooWMSEntities();
            var detail = dbContext.MoveOrderDetail.Find(ID);
            dbContext.MoveOrderDetail.Remove(detail);
            return dbContext.SaveChanges() > 0;
        }
    }
}
