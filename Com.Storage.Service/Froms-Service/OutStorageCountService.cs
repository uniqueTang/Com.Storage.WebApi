using Com.Storage.Repository.Froms_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Service.Froms_Service
{
   public class OutStorageCountService
    {
        public object GetByTime(DateTime startTime, DateTime endTime)
        {
            var outStoCountRepository = new OutStorageCountRepository();
            return outStoCountRepository.GetByTime(startTime, endTime);
        }
    }
}
