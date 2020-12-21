using Com.Storage.Models;
using Com.Storage.Service.Basic_Service;
using Com.Storage.Service.Store_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Com.Storage.WebApi.Controllers.Froms_Controllers
{
    /// <summary>
    /// 货品统计
    /// </summary>
    public class GoodsCountController : ApiController
    {
        /// <summary>
        /// 货品统计
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GoodsSum() 
        {
            var localProductService = new LocalProductService();
            var listLocation = localProductService.ProductCount();
            var result = new 
            {
                list = listLocation
            };
            return Json(result);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetList()
        {
            var localProductService = new LocalProductService();
            var locatProList = localProductService.GetList(); 
            var result = new
            {
                list = locatProList
        };
            return Json(result);
        }
    }
}
