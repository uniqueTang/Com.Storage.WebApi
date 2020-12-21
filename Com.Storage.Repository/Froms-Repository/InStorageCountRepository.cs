using Com.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Repository.Froms_Repository
{
    public class InStorageCountRepository
    {
        public object GetByTime(DateTime startTime, DateTime endTime) 
        {
            var dbContext = new JooWMSEntities();
            var sql = dbContext.InStorage.ToList().
                        Where(item => item.CreateTime >= startTime && item.CreateTime <= endTime).
                        GroupBy(item => new { V = item.CreateTime.ToString("yyyy-MM-dd") }).
                        Select(item=> new
                        {
                            Num = item.Sum(i=>i.Num),
                            Amount = item.Sum(z => z.Amount),
                            CreateTime = item.Key.V
                        });
            return sql;
        }
    }
}
