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
    /// 客户（数据访问层）
    /// </summary>
    public class CustomerRepository: BaseRepository<Customer, Customer>
    {
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<Customer> GetCustomerList(Expression<Func<Customer, bool>> where)
        {
            var dbContext = new JooWMSEntities();
            return dbContext.Customer.Where(where).ToList();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteCustomer(int ID)
        {
            var dbContext = new JooWMSEntities();
            var inStorage = dbContext.Customer.Find(ID);
            inStorage.IsDelete = 1;
            return dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool EditCustomer(Customer customer)
        { 
            var dbContext = new JooWMSEntities();
            var customer_ = dbContext.Customer.Find(customer.ID);
            customer_.CusName = customer.CusName;
            customer_.Email = customer.Email;
            customer_.Fax = customer.Fax;
            customer_.Phone = customer.Phone;
            customer_.Remark = customer.Remark;
            return dbContext.SaveChanges() > 0;
        }
    }
}
