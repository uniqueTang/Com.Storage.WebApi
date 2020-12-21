using Com.Storage.Service.Froms_Service;
using Com.Storage.Service.Store_Service;
using Com.Storage.WebApi.Models.FromsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Com.Storage.WebApi.Controllers.Froms_Controllers
{
    public class InStorageCountController : ApiController
    {
        [HttpGet]
        public IHttpActionResult InStoCount([FromUri]InStoCountDto inStoCountDto) 
        {
            if (inStoCountDto.sky == 0)
            {
                inStoCountDto.StartTime = DateTime.Now.AddYears(-5);
                inStoCountDto.EndTime = DateTime.Now.AddYears(1);
            }
            else if (inStoCountDto.sky == 1)
            {
                inStoCountDto.StartTime = DateTime.Now.AddDays(-60);
                inStoCountDto.EndTime = DateTime.Now;
            }
            else if (inStoCountDto.sky == 2)
            {
                inStoCountDto.StartTime = DateTime.Now.AddDays(-30);
                inStoCountDto.EndTime = DateTime.Now;
            }
            else if(inStoCountDto.sky == 3)
            {
                inStoCountDto.StartTime = DateTime.Now.AddDays(-10);
                inStoCountDto.EndTime = DateTime.Now;
            }
            var InStoCountService = new InStorageCountService();
            var listSto = InStoCountService.GetByTime(inStoCountDto.StartTime,inStoCountDto.EndTime);
            var inStorage = new InStorageService();
            var InStolist = inStorage.GetAll().Where(item=>item.IsDelete!=1 && item.CreateTime >= inStoCountDto.StartTime && item.CreateTime <= inStoCountDto.EndTime).Select(item=>new 
            {
                OrderNum = item.OrderNum,
                SupName = item.SupName,
                ContactName = item.ContactName,
                Phone = item.Phone,
                CreateTime =item.CreateTime.ToString("yyyy-MM-dd"),
                Num = item.Num,
                Amount = item.Amount
            });
            var result = new 
            {
                list = listSto,
               InList = InStolist
            };
            return Json(result);
        }

    }
}
