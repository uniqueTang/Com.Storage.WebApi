using Com.Storage.Models;
using Com.Storage.Service.Basic_Service;
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
    ///  出库详情（Api）
    /// </summary>
    public class OutStorageDetailController : ApiController
    {
        /// <summary>
        /// 添加出库详情
        /// </summary>
        /// <param name="inStorDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOutStorageDetail(List<OutStoDetail> outStoDetails)
        {
            var outStor = new OutStorageDetailService();
            bool isResult = true;
            try
            {
                outStoDetails.ForEach(item => {
                    item.CreateTime = DateTime.Now;
                    item.IsPick = 1;
                    var maxSnNum = outStor.GetAll().Select(getNum => getNum.SnNum).LastOrDefault();
                    int maxCode = Int32.Parse(maxSnNum);
                    maxCode++;
                    if (item.BatchNum == "") {
                        item.BatchNum = "no-one";
                    }
                    item.SnNum = maxCode.ToString().PadLeft(6, '0');
                    item.StorageNum = "DSP_0000";
                    item.RealNum = 0;
                    outStor.Add(item);
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
        /// 根据主单编号查询详情
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetByOrderNum(string orderNum)
        {
            var outStor = new OutStorageDetailService();
            var outList = outStor.GetAll().Where(item => item.OrderNum == orderNum);
            var listObj = new List<Object>();
            outList.ToList().ForEach(item =>
            {
                var location = new LocationService();
                var localProduct = new LocalProductService();
                var locationName = location.FindByLocalNum(item.LocalNum);
                var obj = new
                {
                    ProductName = item.ProductName,
                    BarCode = item.BarCode,
                    OrderNum = item.OrderNum,
                    BatchNum = item.BatchNum,
                    OutPrice = item.OutPrice,
                    SnNum = item.SnNum,
                    Amount = item.Amount,
                    Num = item.Num,
                    location = locationName,
                };
                listObj.Add(obj);
            });
            var result = new
            {
                list = listObj
            };
            return Json(result);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="delArr"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DelArr(List<int> delArr)
        {
            var outStor = new OutStorageDetailService();
            bool isResult = true;
            try
            {
                delArr.ForEach(item =>
                {
                    outStor.deleteOutStorage(item);
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
