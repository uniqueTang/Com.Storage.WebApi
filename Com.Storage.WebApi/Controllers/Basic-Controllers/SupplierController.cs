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
    /// 供应商Ap1
    /// </summary>
    public class SupplierController : ApiController
    {
        public IHttpActionResult Get() 
        {
            var supplierService = new SupplierService();
            var supplierList = supplierService.GetAll().Where(item => item.IsDelete != 1);
            return Json(supplierList);
        }
        /// <summary>
        /// 查询所有未被删除的供应商
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetSupplierList() 
        {
            var supplierService = new SupplierService();
            var supplierList = supplierService.GetAll().Where(item =>item.IsDelete!=1);
            var result = new 
            {
                list = supplierList
            };
            return Json(result);
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="ByName"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetBySupplierList(string byName) 
        {
            var supplierService = new SupplierService();
            var supplierList = supplierService.GetBySupplierList(byName).OrderByDescending(item => item.CreateTime);
            var result = new
            {
                list = supplierList.Select(item =>new { 
                    ID = item.ID,
                    SupNum = item.SupNum,
                    SupName = item.SupName,
                    SupType = item.SupType == 1?"虚拟供应商":"合作供应商",
                    Phone = item.Phone,
                    Fax = item.Fax,
                    Email = item.Email,
                    ContactName = item.ContactName,
                    Address = item.Address,
                    CreateUser = item.CreateUser,
                    Description = item.Description,
                })
            };
            return Json(result);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddSupplier(Supplier supplier) 
        {
            var supplierService = new SupplierService();
            var maxSupNum = supplierService.GetAll().Select(item => item.SupNum).LastOrDefault();
            int supNum = Int32.Parse(maxSupNum);
            supNum++;
            supplier.SupNum = supNum.ToString().PadLeft(6,'0');
            supplier.CreateTime = DateTime.Now;
            supplier.IsDelete = 0;
            var addSupplier = supplierService.Add(supplier);
            var result = new
            {
                Count = addSupplier ? "添加成功" : "添加失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult EditSupplier(Supplier supplier) 
        {
            var supplierService = new SupplierService();
            var editSupplier = supplierService.EditSupplier(supplier);
            var result = new 
            {
                Count = editSupplier?"修改成功!":"修改失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DeleteSupplier(int ID) 
        {
            var supplierService = new SupplierService();
            var deleteSupplier = supplierService.DeleteSupplier(ID);
            var result = new 
            {
                Count = deleteSupplier?"删除成功":"删除失败"
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
            var supplierService = new SupplierService();
            bool isResult = true;
            try
            {
                delArr.ForEach(item =>
                {
                    supplierService.DeleteSupplier(item);
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
    }
}
