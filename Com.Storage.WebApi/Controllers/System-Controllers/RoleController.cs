using Com.Storage.Models;
using Com.Storage.Service.System_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Com.Storage.WebApi.Controllers.System_Controllers
{
    /// <summary>
    /// 角色Api
    /// </summary>
    public class RoleController : ApiController
    {
        /// <summary>
        /// 查询所有未被删除角色
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetAllRole() 
        {
            var roleService = new RoleService();
            var roleList = roleService.GetAll().Where(item => item.IsDelete!=1).OrderByDescending(item => item.CreateTime);
            var result = new 
            {
                list = roleList
            };
            return Json(result);
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="byName"></param>
        /// <returns></returns>
        public IHttpActionResult GetList(string byName) 
        {
            var roleService = new RoleService();
            var roleList = roleService.GetByRoleList(byName);
            var result = new 
            {
                list = roleList
            };
            return Json(result);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DeleteRole(int ID) 
        {
            var roleService = new RoleService();
            var deleteRole = roleService.DeleteRole(ID);
            var result = new 
            {
                Count = deleteRole?"删除成功":"删除失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sysRole"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddRole(SysRole sysRole) 
        {
            var roleService = new RoleService();
            var maxRoleNum = roleService.GetAll().Select(item => item.RoleNum).LastOrDefault();
            int RoleNum = Int32.Parse(maxRoleNum);
            RoleNum++;
            sysRole.CreateTime = DateTime.Now;
            sysRole.IsDelete = 0;
            sysRole.RoleNum = RoleNum.ToString().PadLeft(6, '0');
            var addRole = roleService.Add(sysRole);
            var result = new 
            {
                Count = addRole?"添加成功":"添加失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sysRole"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult EditRole(SysRole sysRole) 
        {
            var roleService = new RoleService();
            var editRole = roleService.EditRole(sysRole);
            var result = new 
            {
                Count = editRole?"修改成功":"修改失败"
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
            var roleService = new RoleService();
            bool isResult = true;
            try
            {
                delArr.ForEach(item =>
                {
                    roleService.DeleteRole(item);
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
