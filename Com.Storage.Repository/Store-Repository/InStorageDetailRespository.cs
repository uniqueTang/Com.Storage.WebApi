using Com.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Repository.Store_Repository
{
    /// <summary>
    /// 入库详情
    /// </summary>
    public class InStorageDetailRespository:BaseRepository<InStorDetail,InStorDetail>
    {
        /// <summary>
        /// 添加入库详情（数据访问层）
        /// </summary>
        /// <param name="inStor"></param>
        /// <returns></returns>
        public bool AddDetail(InStorDetail inStor) 
        {
            var dbContext = new JooWMSEntities();
            var sql = dbContext.InStorDetail.Add(inStor);
            return dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteDetail(int ID) 
        {
            var dbContext = new JooWMSEntities();
            var detail = dbContext.InStorDetail.Find(ID);
            dbContext.InStorDetail.Remove(detail);
            return dbContext.SaveChanges() > 0;
        }
    }
}
