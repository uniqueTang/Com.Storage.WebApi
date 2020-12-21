using Com.Storage.Service;
using Com.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Storage.Repository.System_Repository;

namespace Com.Storage.Service.System_Service
{
   public class DepartmentService: BaseService<SysDepart, SysDepart>
    {
        /// <summary>
        ///根据编号查询部门
        /// </summary>
        /// <param name="departNum"></param>
        /// <returns></returns>
        public SysDepart FindByDepartNum(string departNum)
        {
            return GetByWhere(item => item.DepartNum.Equals(departNum)).SingleOrDefault();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteDepartment(int ID)
        {
            var departRepository = new DepartmentRepository();
            return departRepository.DeleteDepartment(ID);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sysDepart"></param>
        /// <returns></returns>
        public bool EditDepart(SysDepart sysDepart)
        {
            var departRepository = new DepartmentRepository();
            return departRepository.EditDepartment(sysDepart);
        }
    }
}
