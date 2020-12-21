using Com.Storage.Models;
using Com.Storage.Service.Basic_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Com.Storage.WebApi.Controllers.Basic_Controllers
{
    /// <summary>
    /// 客户管理（Api）
    /// </summary>
    public class CustomerController : ApiController
    {
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="byName"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetList(string byName) 
        {
            var customerService = new CustomerService();
            var customerList = customerService.GetCustomerList(byName);
            var result = new
            {
                list = customerList.Select(item =>new 
                {
                    CreateTime = item.CreateTime.ToString("yyyy-MM-dd HH:MM"),
                    CusName = item.CusName,
                    CusNum = item.CusNum,
                    Remark = item.Remark,
                    Phone = item.Phone,
                    Email = item.Email,
                    Fax =item.Fax,
                    ID = item.ID
                })
            };
            return Json(result);
        }

        /// <summary>
        /// 查询最大的用户编号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetMaxCusNum() 
        {

            var customerService = new CustomerService();
            var maxSnNum = customerService.GetAll().Select(item => item.CusNum).LastOrDefault();
            int i = Int32.Parse(maxSnNum);
            i++;
            string s = i.ToString().PadLeft(6, '0');
            return Json(s);
        }

        /// <summary>
        /// 添加客户
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddCustomer(Customer customer) 
        {
            var customerService = new CustomerService();
            customer.IsDelete = 0; customer.CreateTime = DateTime.Now;
            var addCustomer = customerService.Add(customer);
            var result = new 
            {
                Count = addCustomer?"添加成功":"添加失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DeleteCustome(int ID)
        {
            var customerService = new CustomerService();
            var delCustomerr = customerService.DeleteCustomer(ID);
            var result = new
            {
                Count = delCustomerr ? "删除成功!" : "删除失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="delArr"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DelArr(List<int> delArr)
        {
            var customerService = new CustomerService();
            bool isResult = true;
            try
            {
                delArr.ForEach(item =>
                {
                    customerService.DeleteCustomer(item);
                });
            }
            catch (Exception)
            {
                isResult = false;
                throw;
            }
            var result = new
            {
                Msg = isResult ? "删除成功" : "删除失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult EditCustomer(Customer customer) 
        {
            var customerService = new CustomerService();
            var editCustomer = customerService.EditCustomer(customer);
            var result = new 
            {
                Conut = editCustomer?"修改成功":"修改失败"
            };
            return Json(result);
        }
    }
}
