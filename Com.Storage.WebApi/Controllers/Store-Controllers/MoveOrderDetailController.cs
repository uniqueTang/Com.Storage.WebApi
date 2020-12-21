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
    /// 移库详情（Api）
    /// </summary>
    public class MoveOrderDetailController : ApiController
    {
        /// <summary>
        /// 添加移库详情
        /// </summary>
        /// <param name="moveDetail"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddMoveDetail(List<MoveOrderDetail> moveDetails)
        {
            var movedDetailService = new MoveOrderDetailService();
            bool isResult = true;
            try
            {
                moveDetails.ForEach(item =>
                {
                    var getMax = movedDetailService.GetAll().LastOrDefault().SnNum;
                    var maxGet = Convert.ToInt32(getMax);
                    maxGet++;
                    var max = maxGet.ToString().PadLeft(6, '0');
                    item.SnNum = max;
                    item.IsPick = 0;
                    item.RealNum = item.Num;
                    item.CreateTime = DateTime.Now;
                    movedDetailService.Add(item);
                });
            }
            catch (Exception)
            {
                isResult = false;
                throw;
            }
            var result = new
            {
                Msg = isResult ? "添加成功" : "添加失败"
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
            var movedDetailService = new MoveOrderDetailService();
            bool isResult = true;
            try
            {
                delArr.ForEach(item =>
                {
                    movedDetailService.DeleteDetail(item);
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
