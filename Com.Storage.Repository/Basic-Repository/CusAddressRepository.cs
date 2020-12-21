using Com.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Repository.Basic_Repository
{
    /// <summary>
    /// 客户地址（数据访问层）
    /// </summary>
    public class CusAddressRepository: BaseRepository<CusAddress, CusAddress>
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteCusAddress(int ID)
        {
            var dbContext = new JooWMSEntities();
            var cusAddress = dbContext.CusAddress.Find(ID);
            dbContext.CusAddress.Remove(cusAddress);
            return dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 编辑用户地址
        /// </summary>
        /// <param name="cusAddress"></param>
        /// <returns></returns>
        public bool EditCusAddRess(CusAddress cusAddress) 
        {
            var dbContext = new JooWMSEntities();
            var cusAddress_ = dbContext.CusAddress.Find(cusAddress.ID);
            cusAddress_.Address = cusAddress.Address;
            cusAddress_.Contact = cusAddress.Contact;
            cusAddress_.Phone = cusAddress.Phone;
            return dbContext.SaveChanges() > 0;
        }
    }
}
