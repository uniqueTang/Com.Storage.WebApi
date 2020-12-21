using Com.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Repository.Basic_Repository
{
    /// <summary>
    /// 库位管理（Location）数据访问层
    /// </summary>
    public class LocationRepository : BaseRepository<Location, Location>
    {
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<Location> GetLocationList(Expression<Func<Location, bool>> where)
        {
            var dbContext = new JooWMSEntities();
            return dbContext.Location.Where(where).ToList();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool EditLocation(Location location)
        {
            var dbContext = new JooWMSEntities();
            var location_ = dbContext.Location.Find(location.ID);
            location_.LocalName = location.LocalName;
            location_.LocalBarCode = location.LocalBarCode;
            location_.LocalType = location.LocalType;
            location_.IsDefault = location.IsDefault;
            return dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteLocation(int ID)
        {
            var dbContext = new JooWMSEntities();
            var location = dbContext.Location.Find(ID);
            location.IsDelete = 1;
            return dbContext.SaveChanges() > 0;
        }
    }
}