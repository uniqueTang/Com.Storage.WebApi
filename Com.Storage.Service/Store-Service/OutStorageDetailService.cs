using Com.Storage.Models;
using Com.Storage.Repository.Store_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Service.Store_Service
{
    /// <summary>
    /// 出库逻辑层
    /// </summary>
   public class OutStorageDetailService:BaseService<OutStoDetail, OutStoDetail>
    {
        public bool deleteOutStorage(int ID)
        {
            var outStorageDetailRepository = new OutStorageDetailRepository();
            var delete = outStorageDetailRepository.deleteOutStorage(ID);
            return delete;
        }
   }
}
