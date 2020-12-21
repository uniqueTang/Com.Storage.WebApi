using Com.Storage.Models;
using Com.Storage.Service.Store_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Com.Storage.WebApi.Controllers.Store_Controllers
{
    /// <summary>
    /// 入库详情
    /// </summary>
    public class InStorageDetailController : ApiController
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="delArr"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DelArr(List<int> delArr)
        {
            var inStorageDetailService = new InStorageDetailService();
            bool isResult = true;
            try
            {
                delArr.ForEach(item =>
                {
                    inStorageDetailService.DeleteDetail(item);
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
        /// 添加入库详情
        /// </summary>
        /// <param name="inStorDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddInStorDetail(InStorDetail inStorDetail)
        {
            var inStorService = new InStorageDetailService();
            inStorDetail.CreateTime = DateTime.Now;
            inStorDetail.IsPick = 1;
            var maxSnNum = inStorService.GetAll().Select(getNum => getNum.SnNum).LastOrDefault();
            int maxCode = Int32.Parse(maxSnNum);
            maxCode++;
            inStorDetail.SnNum = maxCode.ToString().PadLeft(6, '0');
            var addInStor = inStorService.AddDetail(inStorDetail);
            var result = new
            {
                Count = addInStor ? "添加成功" : "添加失败"
            };
            return Json(result);
        }
    }
}
