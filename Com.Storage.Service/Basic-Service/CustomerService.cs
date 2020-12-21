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
    /// 客户管理（数据访问层）
    /// </summary>
    public class CustomerService : BaseService<Customer, Customer>
    {
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="byName"></param>
        /// <returns></returns>
        public List<Customer> GetCustomerList(string byName)
        {
            var customerRepository = new CustomerRepository();
            Expression<Func<Customer, bool>> where = item => item.IsDelete != 1;
            if (!string.IsNullOrEmpty(byName)) where = where.And(item => item.CusNum.IndexOf(byName) != -1 || item.CusName.IndexOf(byName) != -1);
            return customerRepository.GetCustomerList(where);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteCustomer(int ID)
        {
            var customerRepository = new CustomerRepository();
            return customerRepository.DeleteCustomer(ID);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public bool EditCustomer(Customer customer)
        {
            var customerRepository = new CustomerRepository();
            return customerRepository.EditCustomer(customer);
        }
    }
}
