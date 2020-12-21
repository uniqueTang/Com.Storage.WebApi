using Com.Storage.Models;
using Com.Storage.Service.Basic_Service;
using Com.Storage.WebApi.Models.BasicDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Com.Storage.WebApi.Controllers.Basic_Controllers
{
    /// <summary>
    /// 库位管理 Api
    /// </summary>
    public class LocationController : ApiController
    {
        /// <summary>
        /// 查询所有未被删除的库位（不带条件）
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetLocationList() 
        {
            var locationService = new LocationService();
            var locationList = locationService.GetAll().Where(item => item.IsDelete != 1).OrderByDescending(item => item.CreateTime);
            var listReuslt = new List<object>();
            locationList.ToList().ForEach(item=> {
                string localTypeName ="", isForbid ="", isDefault ="";
                if (item.LocalType == 1) localTypeName = "正式库区"; 
                else if (item.LocalType == 2) localTypeName = "待入库区"; 
                else if (item.LocalType == 3) localTypeName = "待检库区";
                else if (item.LocalType == 4) localTypeName = "待出库区";
                else if (item.LocalType == 5) localTypeName = "报损库区";
                if (item.IsForbid == 0) isForbid = "禁用";
                else isForbid = "可用";
                if (item.IsDefault == 0) isDefault = "是";
                else isDefault = "否";
                var obj = new {
                    LocalBarCode = item.LocalBarCode,
                    LocalName = item.LocalName,
                    LocalNum = item.LocalNum,
                    LocalTypeName = localTypeName,
                    IsForbid = isForbid,
                    IsDefault = isDefault
                };
                listReuslt.Add(obj);
            });
            var result = new 
            {
                list = listReuslt
            };
            return Json(result);
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="locationDto"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetList([FromUri]LocationDto locationDto) 
        {
            var locationService = new LocationService();
            var locationList = locationService.GetLocationList(locationDto.StorageType,locationDto.LocalType,locationDto.LocalName).OrderByDescending(item => item.CreateTime);
            var listReuslt = new List<object>();
            locationList.ToList().ForEach(item => {
                string localTypeName = "", isForbid = "", isDefault = "",StorageNum ="";
                if (item.StorageType == 1) StorageNum = "产品仓库";
                if (item.LocalType == 1) localTypeName = "正式库区";
                else if (item.LocalType == 2) localTypeName = "待入库区";
                else if (item.LocalType == 3) localTypeName = "待检库区";
                else if (item.LocalType == 4) localTypeName = "待出库区";
                else if (item.LocalType == 5) localTypeName = "报损库区";
                if (item.IsForbid == 0) isForbid = "禁用";
                else isForbid = "可用";
                if (item.IsDefault == 0) isDefault = "是";
                else isDefault = "否";
                var obj = new
                {
                    ID = item.ID,
                    LocalNum = item.LocalNum,
                    StorageNum = item.StorageNum,
                    StorageType = StorageNum,
                    LocalType = localTypeName,
                    LocalBarCode = item.LocalBarCode,
                    CreateTime = item.CreateTime.ToString("yyyy-MM-dd HH:MM"),
                    LocalName = item.LocalName,
                    LocalTypeName = localTypeName,
                    IsForbid = isForbid,
                    IsDefault = isDefault
                };
                listReuslt.Add(obj);
            });
            var result = new
            {
                list = listReuslt
            };
            return Json(result);
        }
  
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddLocation(Location location) 
        {
            var locationService = new LocationService();
            var maxLocalNum = locationService.GetAll().Select(item => item.LocalNum).LastOrDefault();
            int localNum = Int32.Parse(maxLocalNum);
            localNum++; location.StorageNum = "DSP_0000";
            location.LocalNum = localNum.ToString().PadLeft(6,'0');
            location.Length = 0; location.Width = 0; location.Height = 0;
            location.X = 0; location.Y = 0; location.Z = 0;
            location.UnitNum = "";location.UnitName = "";
            location.IsForbid = 1;location.IsDelete = 0;
            location.CreateTime = DateTime.Now;
            var addLocation = locationService.Add(location);
            var result = new 
            {
                Count = addLocation?"添加成功":"添加失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult EditLocation(Location location) 
        {
            var locationService = new LocationService();
            var editLocation = locationService.EditLocation(location);
            var result = new 
            {
                Count = editLocation?"编辑成功":"编辑失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 单删
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IHttpActionResult DeleteLocation(int ID) 
        {
            var locationService = new LocationService();
            var deleteLocation = locationService.DeleteLocation(ID);
            var result = new 
            {
                Count = deleteLocation?"删除成功":"删除失败"
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
            var locationService = new LocationService();
            bool isResult = true;
            try
            {
                delArr.ForEach(item =>
                {
                    locationService.DeleteLocation(item);
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
