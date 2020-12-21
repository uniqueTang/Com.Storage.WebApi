using Com.Storage.Models;
using Com.Storage.Service.Basic_Service;
using Com.Storage.Service.Store_Service;
using Com.Storage.Service.System_Service;
using Com.Storage.WebApi.Models.StoreDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Com.Storage.WebApi.Controllers.Store_Controllers
{
    /// <summary>
    /// 出库Api
    /// </summary>
    public class OutStorageController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GroupByPorduct()
        {
            var outStorage = new OutStorageService();
            var list = outStorage.GroupByPorduct();
            var result = new
            {
                list = list
            };
            return Json(result);
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="outStorageDto"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetOutInStorageList([FromUri] OutStorageDto outStorageDto) 
        {
            var outStorageService = new OutStorageService();
            var outStorageList = outStorageService.GetOutStorageList(outStorageDto.Status,outStorageDto.StartTime,outStorageDto.EndTime,
                                                                     outStorageDto.cusName,outStorageDto.ContractOrder,outStorageDto.OutType).OrderByDescending(item => item.CreateTime);
            var listResult = new List<object>();
            var userService = new UserService();
            outStorageList.ToList().ForEach(item => 
            {
                var user = userService.FindByUserNum(item.CreateUser);
                string status, operate, outType = "";
                if (item.OperateType == 1) operate = "电脑";
                else operate = "其他";
                if (item.Status == 1) { status = "等待审核"; }
                else if (item.Status == 2) { status = "审核通过"; }
                else { status = "审核失败"; }
                if (item.OutType == 1) { outType = "采购退货出库"; }
                else if (item.OutType == 2) { outType = "销售退货出库"; }
                else if (item.OutType == 3) { outType = "领用出库"; }
                else if (item.OutType == 4) { outType = "借货出库"; }
                else if (item.OutType == 5) { outType = "借入还出"; }
                var obj = new 
                {
                    ID = item.ID,
                    OrderNum = item.OrderNum,
                    OutType = item.OutType,
                    OutTypeName = outType,
                    CusNum = item.CusNum,
                    CusName = item.CusName,
                    ContractOrder = item.ContractOrder,
                    Num = item.Num,
                    Amount = item.Amount+".00元",
                    Status = item.Status,
                    StatusName = status,
                    OperateType = operate,
                    Address = item.Address,
                    Contact = item.Contact,
                    Phone = item.Phone,
                    CreateTime = item.CreateTime.ToString("yyyy-MM-dd HH:MM"),
                    User =user
                };
                listResult.Add(obj);
            });
            var result = new
            {
                list = listResult
            };
            return Json(result);
        }

        /// <summary>
        /// 查询最大的编号加1
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetMaxOrder() 
        {
            var outStorageService = new OutStorageService();
            var maxOrderNum = outStorageService.GetAll().Select(item => item.OrderNum).LastOrDefault();
            int maxCode = Int32.Parse(maxOrderNum);
            maxCode++;
            var max = maxCode.ToString().PadLeft(6, '0');
            return Json(max);
        }

        /// <summary>
        /// 添加出库账单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddOutStorage(OutStorage outStorage)
        {
            var outStorageService = new OutStorageService();
            outStorage.ProductType = 2;
            outStorage.StorageNum = "DSP_0000";
            outStorage.Status = 1;
            outStorage.IsDelete = 0;
            outStorage.CreateTime = DateTime.Now;
            outStorage.CreateUser = "DA_0000";
            outStorage.OperateType = 1;
            outStorage.SendDate = DateTime.Now;
            outStorage.AuditeTime = DateTime.Now;
            outStorage.EquipmentNum = "null";
            outStorage.EquipmentCode = "null";
            var addOutStorage = outStorageService.Add(outStorage);
            var result = new
            {
                Conut = addOutStorage ? "添加成功!" : "添加失败!"
            };
            return Json(result);
        }

        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="outStorage"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult EditCheckOutStorage(OutStorage outStorage) 
        {
            var outStorageService = new OutStorageService();
            var editCheck = outStorageService.EditCheckOutStorage(outStorage);
            var result = new 
            {
                Count = editCheck?"修改成功":"修改失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 修改出库信息
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult EditOutStorage(OutStorage outStorage) 
        {
            var outStorageService = new OutStorageService();
            var editOut = outStorageService.EditOutStorage(outStorage);
            var result = new 
            {
                Count = editOut?"修改成功!":"修改失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DeleteOutStorage(int ID)
        {
            var outStorageService = new OutStorageService();
            var delInStor = outStorageService.DeleteOutStorage(ID);
            var result = new
            {
                Count = delInStor ? "删除成功!" : "删除失败"
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
            var outStorageService = new OutStorageService();
            bool isResult = true;
            try
            {
                delArr.ForEach(item =>
                {
                    outStorageService.DeleteOutStorage(item);
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
        /// 根据出库编号查询主单信息，以及详情信息
        /// </summary>
        /// <param name="OrderNum"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetByOrder(string OrderNum)
        {
            var outStorageService = new OutStorageService();
            var outStorageList = outStorageService.GetAll().Where(item => item.OrderNum == OrderNum);
            var outStorageDetailService = new OutStorageDetailService();
            var outStorageDetailList = outStorageDetailService.GetAll().Where(item => item.OrderNum == OrderNum);
            var listObj = new List<Object>();
            outStorageDetailList.ToList().ForEach(item => {
                var locationService = new LocationService();
                var locationName = locationService.FindByLocalNum(item.LocalNum);
                var obj = new 
                {
                    OrderNum = item.OrderNum,
                    LocalName = locationName.LocalName,
                    LocalNum = locationName.LocalNum,
                    ID = item.ID,
                    ProductName = item.ProductName,
                    ProductNum = item.ProductNum,
                    BarCode = item.BarCode,
                    BatchNum = item.BatchNum,
                    Num = item.Num,
                    OutPrice = item.OutPrice,
                    Amount = item.Amount,
                };
                listObj.Add(obj);
            });
            var result = new
            {
                outStorageList = outStorageList,
                outStorageDetailList = listObj
            };
            return Json(result);
        }

        /// <summary>
        /// 修改库存
        /// </summary>
        /// <param name="localProduct"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult EditKcNum(List<LocalProduct> localProducts) 
        {
            var localProductService = new LocalProductService();
            bool isResult = true;
            try
            {
                localProducts.ForEach(item =>
                {
                    localProductService.EditProduct(item);
                });
            }
            catch (Exception)
            {
                isResult = false;
                throw;
            }
            var result = new
            {
                Count = isResult ? "删除成功" : "删除失败"
            };
            return Json(result);
        }
    }
}
