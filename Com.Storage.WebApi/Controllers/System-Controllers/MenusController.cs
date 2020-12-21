using Com.Storage.Models;
using Com.Storage.Service.System_Service;
using Com.Storage.WebApi.Models.SystemDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Com.Storage.WebApi.Controllers.System_Controllers
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    public class MenusController : ApiController
    {
        /// <summary>
        /// 查询所有未被删除的用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetMenusList()
        {
            var menusService = new MenusService();
            var menusList = menusService.GetAll().OrderByDescending(item => item.UpdateTime).Where(item => item.IsDelete != 1).OrderByDescending(item => item.CreateTime);


            var result = new
            {
                list = menusList.Select(item => new
                {
                    ID = item.ID,
                    ResNum = item.ResNum,
                    ResName = item.ResName,
                    ParentNum = item.ParentNum,
                    ResType = item.ResType,
                    CssName = item.CssName,
                    Sort = item.Sort,
                    Url = item.Url,
                    CreateTime = item.CreateTime.ToString("yyyy-MM-dd,hh:mm:ss")
                })
            };
            return Json(result);
        }

        [HttpGet]
        public IHttpActionResult GetByWhere(MenusDto menusDto)
        {
            var service = new MenusService();
            if (!string.IsNullOrEmpty(menusDto.ResNum) || !string.IsNullOrEmpty(menusDto.ResName))
            {
                var menusList = service.GetByWhere(menusDto.ResNum, menusDto.ResName);
                var result = new
                {
                    list = menusList.Select(item => new
                    {
                        ID = item.ID,
                        ResNum = item.ResNum,
                        ResName = item.ResName,
                        ParentNum = item.ParentNum,
                        ResType = item.ResType,
                        CssName = item.CssName,
                        Sort = item.Sort,
                        Url = item.Url,
                        CreateTime = item.CreateTime.ToString("yyyy-MM-dd,hh:mm:ss"),
                    })
                };
                return Json(result);
            }
            else
            {
                return GetMenusList();
            }
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sysResource"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddMenus(SysResource sysResource)
        {
            var menus = new MenusService();
            var maxMenusNum = menus.GetAll().Select(item => item.ResNum).LastOrDefault();
            int maxNum = int.Parse(maxMenusNum);
            maxNum++;
            sysResource.ResNum = maxNum.ToString().PadLeft(6, '0');
            sysResource.Depth = 0;
            sysResource.ChildCount = 0;
            sysResource.IsHide = 1;
            sysResource.IsDelete = 0;
            sysResource.Depart = 0;
            sysResource.CreateTime = DateTime.Now;
            sysResource.UpdateTime = DateTime.Now;
            var addMenus = menus.Add(sysResource);
            var result = new
            {
                Count = addMenus ? "添加成功" : "添加失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 删除咯
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DeleteMeuns(int ID)
        {
            var service = new MenusService();
            var delMeuns = service.DeleteMenus(ID);
            var result = new
            {
                Count = delMeuns ? "删除成功" : "删除失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 修改***
        /// </summary>
        /// <param name="sysResource"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult EditMenus(SysResource sysResource)
        {
            var service = new MenusService();
            var editMenus = service.EditMenus(sysResource);
            var result = new
            {
                Count = editMenus ? "修改成功" : "修改失败"
            };
            return Json(result);
        }

    }
}