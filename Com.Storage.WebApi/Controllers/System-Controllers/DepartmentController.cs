using Com.Storage.Models;
using Com.Storage.Service.System_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Com.Storage.WebApi.Controllers.System_Controllers
{
    /// <summary>
    /// 部门Api
    /// </summary>
    public class DepartmentController : ApiController
    {
        /// <summary>
        ///  查询所有为删除的部门
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetAllDepartment() 
        {
            var department = new DepartmentService();
            var departmentList = department.GetAll().Where(item=> item.IsDelete!=1).OrderByDescending(item => item.CreateTime);
            var result = new 
            {
                list = departmentList
            };
            return Json(result);
        }

        /// <summary>
        ///  查询所有未删除的部门
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetAllDepartments()
        {
            var department = new DepartmentService();
            var departmentList = department.GetAll().OrderByDescending(item => item.CreateTime).Where(item => item.IsDelete != 1);
            var result = new
            {
                list = departmentList.Select(item => new
                {
                    ID = item.ID,
                    DepartNum = item.DepartNum,
                    DepartName = item.DepartName,
                    ChildCount = item.ChildCount,
                    ParentNum = item.ParentNum,
                    Depth = item.Depth,
                    IsDelete = item.IsDelete,
                    CreateTime = item.CreateTime.ToString("yyyy-MM-dd,hh:mm:ss")
                })
            };
            return Json(result);
        }

        /// <summary>
        /// 查询最大部门编号
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetMaxNum()
        {
            var depart = new DepartmentService();

            try
            {
                var DepartNum = depart.GetAll().Select(item => item.DepartNum).LastOrDefault();

                return Json(DepartNum);
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sysDepart"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddDepartment(SysDepart sysDepart)
        {
            var service = new DepartmentService();

            var maxDepartNum = service.GetAll().Select(item => item.DepartNum).LastOrDefault();
            int maxNum = Int32.Parse(maxDepartNum);
            maxNum++;
            sysDepart.DepartNum = maxNum.ToString().PadLeft(6, '0');
            sysDepart.ChildCount = 0;
            sysDepart.IsDelete = 0;
            sysDepart.Depth = 0;
            sysDepart.CreateTime = DateTime.Now;
            var addDepart = service.Add(sysDepart);
            var result = new
            {
                Conut = addDepart ? "添加成功!" : "添加失败!"
            };
            return Json(result);
        }

        /// <summary>
        /// 根据Id删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DeleteDepartment(int ID)
        {
            var service = new DepartmentService();
            var delDepart = service.DeleteDepartment(ID);
            var result = new
            {
                Conut = delDepart ? "删除成功!" : "删除失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sysDepart"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult EditDepartment(SysDepart sysDepart)
        {
            var service = new DepartmentService();
            var editDepart = service.EditDepart(sysDepart);
            var result = new
            {
                Count = editDepart ? "修改成功!" : "修改失败"
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
            var depart = new DepartmentService();
            bool isResult = true;
            try
            {
                delArr.ForEach(item =>
                {
                    depart.DeleteDepartment(item);
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
