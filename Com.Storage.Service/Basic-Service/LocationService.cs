using Com.Storage.Models;
using Com.Storage.Repository.Basic_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Service.Basic_Service
{
    /// <summary>
    /// 库位管理（Location）业务逻辑层
    /// </summary>
    public class LocationService : BaseService<Location, Location>
    {
        /// <summary>
        /// 根据编号查询库位名称
        /// </summary>
        /// <param name="LocalNum"></param>
        /// <returns></returns>
        public Location FindByLocalNum(string LocalNum)
        {
            return GetByWhere(item => item.LocalNum.Equals(LocalNum)).SingleOrDefault();
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="StorageType"></param>
        /// <param name="LocalType"></param>
        /// <param name="LocalName"></param>
        /// <returns></returns>
        public List<Location> GetLocationList(int StorageType, int LocalType, string LocalName)
        {
            var locationRepository = new LocationRepository();
            Expression<Func<Location, bool>> where = item => item.IsDelete != 1;
            if (StorageType != 0) where = where.And(item => item.StorageType == StorageType);
            if (LocalType != 0) where = where.And(item => item.LocalType == LocalType);
            if (!string.IsNullOrEmpty(LocalName)) where = where.And(item => item.LocalName.IndexOf(LocalName) != -1);
            return locationRepository.GetLocationList(where);
        }

        /// <summary>
        /// 编辑库位
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public bool EditLocation(Location location)
        {
            var locationRepository = new LocationRepository();
            return locationRepository.EditLocation(location);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteLocation(int ID)
        {
            var locationRepository = new LocationRepository();
            return locationRepository.DeleteLocation(ID);
        }
    }
}
