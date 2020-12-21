using Com.Storage.Models;
using Com.Storage.Service.Store_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Com.Storage.WebApi.Controllers.Basic_Controllers
{
    /// <summary>
    /// 库存Api
    /// </summary>
    public class LocalProductController : ApiController
    {
      
        /// <summary>
        /// 查询最大的编号加1
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Get() 
        {
            var localProductService = new LocalProductService();
            var getMax = localProductService.GetAll().LastOrDefault().Sn;
            var maxGet =  Convert.ToInt32(getMax);
            maxGet++;
            return Json(maxGet);
        }

        /// <summary>
        /// 添加入库
        /// </summary>
        /// <param name="localProducts"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddLocationPor(List<LocalProduct> localProducts) 
        {
            var localProductService = new LocalProductService();
            bool isResult = true;
            try
            {
                localProducts.ForEach(item => {
                    var getMax = localProductService.GetAll().LastOrDefault().Sn;
                    var maxGet = Convert.ToInt32(getMax);
                    maxGet++;
                    item.Sn = Convert.ToString(maxGet);
                    item.StorageName = "产品仓库";
                    item.StorageNum = "DSP_0000";
                    item.CreateUser = "DA_0000";
                    item.CreateName = "administrator";
                    item.CreateTime = DateTime.Now;
                    localProductService.Add(item);
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
        /// 修改或新增库存产品
        /// </summary>
        /// <param name="moveDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddLocalNumOrEdit(List<MoveOrderDetail> moveDetails) 
        {
            var localProductService = new LocalProductService();
            var stockList = localProductService.GetAll();
            for (int i = 0; i < moveDetails.Count; i++) 
            {
                for (int j = 0; j < stockList.Count; j++)
                {
                                                        // 当前产品库位于库存表中库位一致      减少当前库位中的数量
                    if (moveDetails[i].BarCode == stockList[j].BarCode && moveDetails[i].FromLocalNum == stockList[j].LocalNum)
                    {
                        stockList[j].Num = stockList[j].Num - moveDetails[i].Num;
                        localProductService.EditNum(stockList[j]);
                    }
                                                        // 产品移入库位与库存表中库位一致  增加移入库位数量
                    if (moveDetails[i].BarCode == stockList[j].BarCode && moveDetails[i].ToLocalNum == stockList[j].LocalNum)
                    {
                        stockList[j].Num = stockList[j].Num + moveDetails[i].Num;
                        localProductService.EditNum(stockList[j]);
                    }
                                                        // 当前库位等于存在与库存表    并且移位的库位不存在库存表中就进行新增操作
                   /* if (moveDetails[i].FromLocalNum == stockList[j].LocalNum && moveDetails[i].ToLocalNum!=stockList[j].LocalNum) 
                    {
                        stockList[j].Num = stockList[j].Num - moveDetails[i].Num;
                        localProductService.EditNum(stockList[j]);
                        var getMax = localProductService.GetAll().LastOrDefault().Sn;
                        var maxGet = Convert.ToInt32(getMax);
                        maxGet++;
                        stockList[j].Sn = Convert.ToString(maxGet);
                        stockList[j].StorageName = "产品仓库";
                        stockList[j].StorageNum = "DSP_0000";
                        stockList[j].CreateUser = "DA_0000";
                        stockList[j].CreateName = "administrator";
                        stockList[j].CreateTime = DateTime.Now;
                        stockList[j].LocalNum = moveDetails[i].ToLocalNum;
                       
                       // localProductService.Add(stockList[j]);                   
                        // 添加一条移库产品记录
                    }*/
                }
            }
            return Json("");
        }
    }
}
