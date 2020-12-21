using Com.Storage.Models;
using Com.Storage.Service.Basic_Service;
using Com.Storage.Service.Store_Service;
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
    /// 移库Api
    /// </summary>
    public class MoveController : ApiController
    {
        /// <summary>
        /// 查询最大的移库编号加1
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetMaxOrder() 
        {
            var moveService = new MoveService();
            var maxOrderNum = moveService.GetAll().Select(item => item.OrderNum).LastOrDefault();
            int maxCode = Int32.Parse(maxOrderNum);
            maxCode++;
            var max = maxCode.ToString().PadLeft(6, '0');
            return Json(max);
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetMoveOrderList([FromUri] MoveDto modeDto) 
        {
            var moveService = new MoveService();
            var moveList = moveService.GetMoveOrderList(modeDto.Status,modeDto.OrderNum,modeDto.StartTime,modeDto.EndTime).OrderByDescending(item => item.CreateTime);
            var listObj = new List<Object>();
            moveList.ToList().ForEach(item => 
            {
                string MoveName, Status = "", OperateName;
                if (item.MoveType == 1) MoveName = "移库上架";
                else if (item.MoveType == 2) MoveName = "仓库移库";
                else MoveName = "报损移库";
                if (item.Status == 1) { Status = "等待审核"; }
                else if (item.Status == 2) { Status = "审核通过"; }
                else { Status = "审核失败"; }
                if (item.OperateType == 1) OperateName = "电脑";
                else OperateName = "其他";
                var obj = new 
                {
                    ID = item.ID,
                    OrderNum = item.OrderNum,
                    MoveType = item.MoveType,
                    MoveTypeName = MoveName,
                    Status = item.Status,
                    StatusName = Status,
                    Num = item.Num,
                    Amout = item.Amout,
                    CreateTime = item.CreateTime.ToString("yyyy-MM-dd HH:MM"),
                    OperateType = OperateName,
                    ContractOrder = item.ContractOrder,
                    Remark = item.Remark
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
        /// 添加移库主单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddMove(MoveOrder moveOrder) 
        {
            var moveService = new MoveService();
            moveOrder.ProductType = 2;
            moveOrder.StorageNum = "DSP_0000";
            moveOrder.Status = 1;
            moveOrder.IsDelete = 0;
            moveOrder.CreateTime = DateTime.Now;
            moveOrder.OperateType = 1;
            moveOrder.EquipmentCode = "";
            moveOrder.EquipmentNum = "";
            var addMove = moveService.Add(moveOrder);
            var result = new 
            {
                Count = addMove?"添加成功":"添加失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 查看移库主单信息以及详情信息
        /// </summary>
        /// <param name="orderNum"></param>
        /// <returns></returns>
        public IHttpActionResult GetByOrderNum(string orderNum)
        {
            var moveService = new MoveService();
            var moveList = moveService.GetAll().Where(item=>item.OrderNum == orderNum);
            var moveDetailService = new MoveOrderDetailService();
            var moveDetailList = moveDetailService.GetAll().Where(item=>item.OrderNum == orderNum).OrderByDescending(item => item.CreateTime);
            var listObj = new List<Object>();
            moveDetailList.ToList().ForEach(item=> 
            {
                var locationService = new LocationService();
                var Fromlocation = locationService.FindByLocalNum(item.FromLocalNum);
                var Tolocation = locationService.FindByLocalNum(item.ToLocalNum);
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
                    Amout = item.Amout,
                    FromLocalNum = item.FromLocalNum,
                    LocalName = Fromlocation.LocalName,
                    ToLocalNum = item.ToLocalNum,
                    ToLocalName = Tolocation.LocalName
                };
                listObj.Add(obj);
            });
            var result = new 
            {
                MoveList = moveList,
                MoveDetailList = listObj
            };
            return Json(result);
        }

        /// <summary>
        /// 审核修改
        /// </summary>
        /// <param name="moveOrder"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult EditCheckMove(MoveOrder moveOrder)
        {
            var moveService = new MoveService();
            var editCheckMove = moveService.EditCheckMove(moveOrder);
            var result = new
            {
                Count = editCheckMove ? "修改成功" : "修改失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DeleteMove(int ID)
        {
            var moveService = new MoveService();
            var delMove = moveService.DeleteMove(ID);
            var result = new
            {
                Count = delMove ? "删除成功!" : "删除失败"
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
            var moveService = new MoveService();
            bool isResult = true;
            try
            {
                delArr.ForEach(item =>
                {
                    moveService.DeleteMove(item);
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
        /// 修改移库
        /// </summary>
        /// <param name="moveOrder"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult EditMove(MoveOrder moveOrder) 
        {
            var moveService = new MoveService();
            var editMove = moveService.EditMove(moveOrder);
            var result = new 
            {
                Count = editMove?"修改成功":"修改失败"
            };
            return Json(result);
        }
    }
}
