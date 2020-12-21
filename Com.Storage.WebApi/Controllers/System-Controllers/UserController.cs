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
    /// 用户Api
    /// </summary>
    public class UserController : ApiController
    {

        /// <summary>
        /// 登录成功
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Login(string userName, string userPwd) 
        {
            var user = new UserService();
            var login = user.Login(userName,userPwd);
            var result = new 
            {
                Count = login?"登录成功":"登录失败"
            };
            return Json(result);
        }
        /// <summary>
        /// 查询所有未被删除的用户
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetUserList([FromUri] UserDto userDto)
        {
            var user = new UserService();
            var userList = user.GetUserList(userDto.RealName, userDto.DepartNum,userDto.RoleNum).OrderByDescending(item => item.CreateTime);
            var listReuslt = new List<object>();
            var roleService = new RoleService();
            var departmentService = new DepartmentService();
            userList.ToList().ForEach(item =>
            {
                var roleName = roleService.FindByRoleNum(item.RoleNum);
                var departmentName = departmentService.FindByDepartNum(item.DepartNum);
                var obj = new
                {
                    ID = item.ID,
                    LoginCount = item.LoginCount,
                    UpdateTime = item.UpdateTime.ToString("yyyy-MM-dd HH:MM"),
                    UserName = item.UserName,
                    UserCode = item.UserCode,
                    RealName = item.RealName,
                    Email = item.Email,
                    Phone = item.Phone,
                    Role = roleName,
                    Department = departmentName
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
        /// 查询最大用户编号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetMaxCode()
        {
            var user = new UserService();
            var maxCode = user.GetAll().Select(item => item.UserCode).LastOrDefault();
            return Json(maxCode);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddUser(Admin admin)
        {
            var user = new UserService();
            admin.CreateTime = DateTime.Now;
            admin.UpdateTime = DateTime.Now;
            admin.LoginCount = 0;
            admin.IsDelete = 0;
            admin.Status = 0;
            admin.ParentCode = "null";
            var addUser = user.Add(admin);
            var result = new
            {
                Conut = addUser ? "添加成功!" : "添加失败!"
            };
            return Json(result);
        }

        /// <summary>
        /// 删除，此删除不做物理删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DeleteUser(int ID)
        {
            var user = new UserService();
            var delUser = user.DeleteUser(ID);
            var result = new
            {
                Conut = delUser ? "删除成功!" : "删除失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult EditUser(Admin admin)
        {
            var user = new UserService();
            var editUser = user.EditUser(admin);
            var result = new
            {
                Count = editUser ? "修改成功!" : "修改失败"
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
            var user = new UserService();
            bool isResult = true;
            try
            {
                delArr.ForEach(item =>
                {
                    user.DeleteUser(item);
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
