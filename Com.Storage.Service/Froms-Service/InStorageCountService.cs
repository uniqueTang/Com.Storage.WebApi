using Com.Storage.Repository.Froms_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Service.Froms_Service
{
  public class InStorageCountService
    {
        public object GetByTime(DateTime startTime, DateTime endTime)
        {
            var InStoCountRepository = new InStorageCountRepository();
            return InStoCountRepository.GetByTime(startTime,endTime);
        }
        }
}
