using Com.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Repository.System_Repository
{
   public class DepartmentRepository:BaseRepository<SysDepart, SysDepart>
    {

        /// <summary>
        /// 根据ID删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteDepartment(int ID)
        {
            var entities = new JooWMSEntities();
            var depar = entities.SysDepart.Find(ID);
            depar.IsDelete = 1;
            return entities.SaveChanges() > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sysDepart"></param>
        /// <returns></returns>
        public bool EditDepartment(SysDepart sysDepart)
        {
            var entities = new JooWMSEntities();
            var depart = entities.SysDepart.Find(sysDepart.ID);
            depart.DepartName = sysDepart.DepartName;
            return entities.SaveChanges() > 0;
        }
    }
}
