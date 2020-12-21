using Com.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Repository.System_Repository
{
   public class RoleRepository:BaseRepository<SysRole, SysRole>
    {
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<SysRole> GetByRoleList(Expression<Func<SysRole, bool>> where)
        {
            var dbContext = new JooWMSEntities();
            return dbContext.SysRole.Where(where).ToList();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteRole(int ID) 
        {
            var dbContext = new JooWMSEntities();
            var deleteRole = dbContext.SysRole.Find(ID);
            deleteRole.IsDelete = 1;
            return dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sysRole"></param>
        /// <returns></returns>
        public bool EditRole(SysRole sysRole) 
        {
            var dbContext = new JooWMSEntities();
            var role_ = dbContext.SysRole.Find(sysRole.ID);
            role_.RoleName = sysRole.RoleName;
            role_.Remark = sysRole.Remark;
            return dbContext.SaveChanges() > 0;
        }
    }
}
