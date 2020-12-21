using Com.Storage.Service;
using Com.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Storage.Repository.System_Repository;
using System.Linq.Expressions;

namespace Com.Storage.Service.System_Service
{
    public class RoleService : BaseService<SysRole, SysRole>
    {
        /// <summary>
        /// 根据角色编号查询
        /// </summary>
        /// <param name="roleNum"></param>
        /// <returns></returns>
        public SysRole FindByRoleNum(string roleNum)
        {
            return GetByWhere(item => item.RoleNum.Equals(roleNum)).SingleOrDefault();
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="byName"></param>
        /// <returns></returns>
        public List<SysRole> GetByRoleList(string byName)
        {
            var roleRepository = new RoleRepository();
            Expression<Func<SysRole, bool>> where = item => item.IsDelete != 1;
            if (!string.IsNullOrEmpty(byName)) where = where.And(item => item.RoleNum.IndexOf(byName) != -1 || item.RoleName.IndexOf(byName) != -1);
            return roleRepository.GetByRoleList(where);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteRole(int ID)
        {
            var roleRepository = new RoleRepository();
            return roleRepository.DeleteRole(ID);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sysRole"></param>
        /// <returns></returns>
        public bool EditRole(SysRole sysRole)
        {
            var roleRepository = new RoleRepository();
            return roleRepository.EditRole(sysRole);
        }
    }
}
