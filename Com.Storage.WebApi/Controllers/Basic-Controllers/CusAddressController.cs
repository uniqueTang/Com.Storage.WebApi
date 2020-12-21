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
    /// 客户地址（Api）
    /// </summary>
    public class CusAddressController : ApiController
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="listCusAddress"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddCusAddress(List<CusAddress> listCusAddress) 
        {
            var cusAddressService = new CusAddressService();
            bool isResult = true;
            try
            {
                listCusAddress.ForEach(item => {
                    var maxSnNum = cusAddressService.GetAll().Select(getNum => getNum.SnNum).LastOrDefault();
                    int maxSn = Int32.Parse(maxSnNum);
                    maxSn++;
                    item.SnNum = maxSn.ToString().PadLeft(6, '0');
                    item.CreateTime = DateTime.Now;
                    item.IsDelete = 0;
                    cusAddressService.Add(item);
                });
            }
            catch (Exception)
            {
                isResult = false;
                throw;
            }
            var result = new
            {
                Result = isResult,
                Msg = isResult ? "添加成功" : "添加失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 添加地址单个
        /// </summary>
        /// <param name="cusAddress"></param>
        /// <returns></returns>
        public IHttpActionResult AddEditAddRess(CusAddress cusAddress) 
        {
            var cusAddressService = new CusAddressService();
            var maxSnNum = cusAddressService.GetAll().Select(getNum => getNum.SnNum).LastOrDefault();
            int maxSn = Int32.Parse(maxSnNum);
            maxSn++;
            cusAddress.SnNum = maxSn.ToString().PadLeft(6, '0');
            cusAddress.CreateTime = DateTime.Now;
            cusAddress.IsDelete = 0;
            var add = cusAddressService.Add(cusAddress);
            var result = new 
            {
                Count = add?"添加成功":"添加失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DeleteCusAddRess(int ID) 
        {
            var cusAddressService = new CusAddressService();
            var deleteCusAddRess = cusAddressService.DeleteCusAddress(ID);
            var result = new 
            {
                Count = deleteCusAddRess?"删除成功":"删除失败"
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
            var cusAddressService = new CusAddressService();
            bool isResult = true;
            try
            {
                delArr.ForEach(item =>
                {
                    cusAddressService.DeleteCusAddress(item);
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
        /// 根据客户编号查询地址信息
        /// </summary>
        /// <param name="CusNum"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetByList(string CusNum) 
        {
            var cusAddressService = new CusAddressService();
            var cusAddressList = cusAddressService.GetAll().Where(item => item.CusNum == CusNum);
            var result = new 
            {
                list = cusAddressList
            };
            return Json(result);
        }

        /// <summary>
        /// 根据客户编号查询地址信息
        /// </summary>
        /// <param name="CusNum"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAddRessList(string AddRessNum)
        {
            var cusAddressService = new CusAddressService();
            var AddressList = cusAddressService.GetAll().Where(item => item.SnNum == AddRessNum);
            var result = new
            {
                list = AddressList
            };
            return Json(result);
        }

        /// <summary>
        /// 编辑地址
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult EditCusAddRess(CusAddress cusAddress) 
        {
            var cusAddressService = new CusAddressService();
            var editCusAddRess = cusAddressService.EditCusAddRess(cusAddress);
            var result = new 
            {
                Count = editCusAddRess?"修改成功":"修改失败"
            };
            return Json(result);
        }
    }
}
