using Com.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Repository.Store_Repository
{
    /// <summary>
    ///  出库详情
    /// </summary>
   public class OutStorageDetailRepository:BaseRepository<OutStoDetail, OutStoDetail>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool deleteOutStorage(int ID) 
        {
            var dbContext = new JooWMSEntities();
            var outSto = dbContext.OutStoDetail.Find(ID);
            dbContext.OutStoDetail.Remove(outSto);
            return dbContext.SaveChanges() > 0;
        }
    }
}
