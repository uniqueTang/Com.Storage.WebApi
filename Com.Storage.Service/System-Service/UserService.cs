using Com.Storage.Service;
using Com.Storage.Models;
using Com.Storage.Repository.Sysyem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Service.System_Service
{
    /// <summary>
    /// 用户业务逻辑层
    /// </summary>
    public class UserService : BaseService<Admin, Admin>
    {
        /// <summary>
        ///  登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        public bool Login(string userName, string userPwd)
        {
            var userRepository = new UserRepository();
            return userRepository.Login(userName, userPwd);
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="realName">真名</param>
        /// <param name="departNum">部门</param>
        /// <param name="roleNum">角色</param>
        /// <returns></returns>
        public List<Admin> GetUserList(string realName, string departNum, string roleNum)
        {
            var userRepository = new UserRepository();
            Expression<Func<Admin, bool>> where = item => item.IsDelete != 1;
            if (!string.IsNullOrEmpty(realName)) where = where.And(item => item.RealName.IndexOf(realName) != -1);
            if (!string.IsNullOrEmpty(departNum)) where = where.And(item => item.DepartNum == departNum);
            if (!string.IsNullOrEmpty(roleNum)) where = where.And(item => item.RoleNum == roleNum);
            return userRepository.GetUserList(where);
        }

        /// <summary>
        /// 根据用户编号查询
        /// </summary>
        /// <param name="AdminNum"></param>
        /// <returns></returns>
        public Admin FindByUserNum(string UserNum)
        {
            return GetByWhere(item => item.UserCode.Equals(UserNum)).SingleOrDefault();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteUser(int ID)
        {
            var userRepository = new UserRepository();
            return userRepository.DeleteUser(ID);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public bool EditUser(Admin admin)
        {
            var userRepository = new UserRepository();
            return userRepository.EditUser(admin);
        }
    }
}
