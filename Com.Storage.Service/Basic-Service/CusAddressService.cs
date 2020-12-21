using Com.Storage.Models;
using Com.Storage.Repository.Basic_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Service.Basic_Service
{
    /// <summary>
    /// 客户地址（业务逻辑层）
    /// </summary>
    public class CusAddressService : BaseService<CusAddress, CusAddress>
    {
        /// <summary>
        /// 删除地址
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteCusAddress(int ID)
        {
            var cusAddressRepository = new CusAddressRepository();
            return cusAddressRepository.DeleteCusAddress(ID);
        }

        /// <summary>
        /// 编辑用户地址
        /// </summary>
        /// <param name="cusAddress"></param>
        /// <returns></returns>
        public bool EditCusAddRess(CusAddress cusAddress)
        {
            var cusAddressRepository = new CusAddressRepository();
            return cusAddressRepository.EditCusAddRess(cusAddress);
        }
    }
}
