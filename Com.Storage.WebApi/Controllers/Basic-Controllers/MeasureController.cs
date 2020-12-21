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
    /// 单位（Api）
    /// </summary>
    public class MeasureController : ApiController
    {
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="byName"></param>
        /// <returns></returns>
        public IHttpActionResult GetList(string byName) 
        {
            var masureService = new MeasureService();
            var mesureList = masureService.GetMeasureList(byName);
            var result = new 
            {
                list = mesureList
            };
            return Json(result);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="measure"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddMeasure(Measure measure) 
        {
            var measureService = new MeasureService();                      // 查询最大的编号
            var maxSn = measureService.GetAll().OrderByDescending(item => item.SN).Select(item => item.SN).FirstOrDefault();
            var maxNum = measureService.GetAll().OrderByDescending(item => item.MeasureNum).Select(item => item.MeasureNum).FirstOrDefault();
            int i = Int32.Parse(maxSn);                                     
            int j = Int32.Parse(maxNum);
            i = i+2; j = j + 2;                                             // 添加一次加2
            measure.SN = i.ToString().PadLeft(6, '0');
            measure.MeasureNum = j.ToString().PadLeft(6,'0');               
            var addMeasure = measureService.Add(measure);
            var result = new 
            {
                Count = addMeasure?"添加成功":"添加失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="measure"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult EditMeasure(Measure measure) 
        {
            var measureService = new MeasureService();
            var editMeasure = measureService.EditMeasure(measure);
            var result = new 
            {
                Count = editMeasure?"编辑成功":"编辑失败"
            };
            return Json(result);
        }
        /// <summary>
        /// 单删
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DeleteMeasure(int ID) 
        {
            var measureService = new MeasureService();
            var delMeasure = measureService.DeleteMeasure(ID);
            var result = new 
            {
                Count = delMeasure?"删除成功":"删除失败"
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
            var measureService = new MeasureService();
            bool isResult = true;
            try
            {
                delArr.ForEach(item =>
                {
                    measureService.DeleteMeasure(item);
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
