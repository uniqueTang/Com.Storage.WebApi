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
    /// 入库Api
    /// </summary>
    public class InStorageController : ApiController
    {
        /// <summary>
        /// 入库分组
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GroupByPorduct() 
        {
            var inStorage = new InStorageService();
            var list = inStorage.GroupByPorduct();
            var result = new 
            {
                list = list
            };
            return Json(result);
        }
        /// <summary>
        /// 单号查询
        /// </summary>
        /// <param name="OrderNum"></param>
        /// <returns></returns>
        public IHttpActionResult Get(string OrderNum) 
        {
            var inStorage = new InStorageService();
            var list = inStorage.GetAll().Where(item => item.OrderNum.IndexOf(OrderNum) != -1);
            return Json(list);
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetInStorageList([FromUri] InStoageDto stoageDto)
        {
            var inStorage = new InStorageService();
            var inStorageList = inStorage.GetInStorageList(stoageDto.Status, stoageDto.StartTime,
                                                           stoageDto.EndTime, stoageDto.supName, stoageDto.ContractNum, stoageDto.InType).OrderByDescending(item=>item.CreateTime);
            var listResult = new List<object>();
            var userService = new UserService();
            inStorageList.ToList().ForEach(item =>
            {
                var user = userService.FindByUserNum(item.CreateUser);
                string status, operate, inType = "";
                if (item.Status == 1) { status = "等待审核"; }
                else if (item.Status == 2) { status = "审核通过"; }
                else { status = "审核失败"; }
                if (item.OperateType == 1) { operate = "电脑"; }
                else { operate = "其他"; }
                if (item.InType == 1) { inType = "采购收获入库"; }
                else if (item.InType == 2) { inType = "销售退货入库"; }
                else if (item.InType == 3) { inType = "生产产品入库"; }
                else if (item.InType == 4) { inType = "领用退还入库"; }
                else if (item.InType == 5) { inType = "借货入库"; }
                else if (item.InType == 6) { inType = "借出入库"; }
                var obj = new
                {
                    ID = item.ID,
                    ContractOrder = item.ContractOrder,
                    CreateTime = item.CreateTime.ToString("yyyy-MM-dd HH:MM"),
                    OperateType = operate,
                    Amount = item.Amount + ".00元",
                    OrderNum = item.OrderNum,
                    Phone = item.Phone,
                    SupNum = item.SupNum,
                    ContactName = item.ContactName,
                    SupName = item.SupName,
                    Num = item.Num,
                    StatusName = status,
                    Reason = item.Reason,
                    Remark = item.Remark,
                    Status = item.Status,
                    InType = inType,
                    User = user,
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
        /// 查询最大的单号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetMaxOrderNum()
        {
            var inStorage = new InStorageService();
            var maxCode = inStorage.GetAll().Select(item => item.OrderNum).LastOrDefault();
            return Json(maxCode);
        }

        /// <summary>
        /// 添加入库账单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddInStorage(InStorage inStorage)
        {
            var inStorageService = new InStorageService();
            inStorage.ProductType = 2;
            inStorage.StorageNum = "DSP_0000";
            inStorage.ContractType = 0;
            inStorage.Status = 1;
            inStorage.IsDelete = 0;
            inStorage.NetWeight = 0;
            inStorage.CreateTime = DateTime.Now;
            inStorage.GrossWeight = 0;
            inStorage.OperateType = 1;
            inStorage.EquipmentNum = "null";
            inStorage.EquipmentCode = "null";
            var addInStorage = inStorageService.Add(inStorage);
            var result = new {
                Conut = addInStorage ? "添加成功!" : "添加失败!"
            };
            return Json(result);
        }

        /// <summary>
        /// 获取最大的编号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetMaxSnNum()
        {
            var inStor = new InStorageDetailService();
            var maxSnNum = inStor.GetAll().Select(item => item.SnNum).LastOrDefault();
            int i = Int32.Parse(maxSnNum);
            i++;
            string s = i.ToString().PadLeft(6, '0');
            return Json(s);
        }

        /// <summary>
        /// 添加入库详情
        /// </summary>
        /// <param name="inStorDetails"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddInStorageDetail(List<InStorDetail> inStorDetails)
        {
            var inStor = new InStorageDetailService();
            bool isResult = true;
            try
            {
                inStorDetails.ForEach(item => {
                    item.CreateTime = DateTime.Now;
                    item.IsPick = 1;
                    var maxSnNum = inStor.GetAll().Select(getNum => getNum.SnNum).LastOrDefault();
                    int maxCode = Int32.Parse(maxSnNum);
                    maxCode++;
                    item.SnNum = maxCode.ToString().PadLeft(6, '0');
                    inStor.AddDetail(item);
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
        /// 根据主单编号查询详情信息
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetByOrderNum(string orderNum)
        {
            var inStor = new InStorageDetailService();
            var detailList = inStor.GetAll().Where(item => item.OrderNum == orderNum);
            var listObj = new List<Object>();
            detailList.ToList().ForEach(item => {
                var location = new LocationService();
                var locationName = location.FindByLocalNum(item.LocalNum);
                var obj = new
                {
                    ID = item.ID,
                    ProductName = item.ProductName,
                    ProductNum = item.ProductNum,
                    BarCode = item.BarCode,
                    BatchNum = item.BatchNum,
                    Num = item.Num,
                    InPrice = item.InPrice + ".00元",
                    Amount = item.Amount + ".00元",
                    LocalName = locationName.LocalName,
                    LocalNum = locationName.LocalNum
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
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DeleteInStorage(int ID) 
        {
            var inStorageService = new InStorageService();
            var delInStor = inStorageService.DeleteInStorage(ID);
            var result = new 
            {
                Count = delInStor?"删除成功!":"删除失败"
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
            var inStorageService = new InStorageService();
            bool isResult = true;
            try
            {
                delArr.ForEach(item =>
                {
                    inStorageService.DeleteInStorage(item);
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
        /// 入库审核修改
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult EditCheckInStorage(InStorage storage) 
        {
            var inStorageService = new InStorageService();
            var editCheckInStor = inStorageService.EditCheckInStorage(storage);
            var result = new 
            {
                list = editCheckInStor?"修改成功":"修改失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 根据入库编号查询主单信息，以及详情信息
        /// </summary>
        /// <param name="OrderNum"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetByOrder(string OrderNum)
        {                                                               
            var inStorageService = new InStorageService();                  
            var inStorageList = inStorageService.GetAll().Where(item => item.OrderNum == OrderNum);                                                        
            var inStorageDetailService = new InStorageDetailService();      
            var inStorageDetailList = inStorageDetailService.GetAll().Where(item =>item.OrderNum ==OrderNum);
            var listObj = new List<Object>();
            inStorageDetailList.ToList().ForEach(item=> 
            {
                var location = new LocationService();
                var locationName = location.FindByLocalNum(item.LocalNum);
                var obj = new
                {
                    ID = item.ID,
                    OrderNum = item.OrderNum,
                    ProductName = item.ProductName,
                    ProductNum = item.ProductNum,
                    BarCode = item.BarCode,
                    BatchNum = item.BatchNum,
                    Num = item.Num,
                    InPrice = item.InPrice,
                    Amount = item.Amount,
                    LocalName = locationName.LocalName,
                    LocalNum = locationName.LocalNum,
                };
                listObj.Add(obj);
            });
            var result = new
            {
                InStorageList = inStorageList,
                InStorageDetailList = listObj
            };
            return Json(result);
        }

        /// <summary>
        /// 修改入库主单信息
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult EditInStorage(InStorage storage) 
        {
            var inStorageService = new InStorageService();
            var EditInStorage = inStorageService.EditInStorage(storage);
            var result = new 
            {
                Count = EditInStorage?"编辑成功":"编辑失败"
            };
            return Json(result);
        }

       
    }
}
