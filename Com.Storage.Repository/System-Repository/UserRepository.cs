using Com.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Repository.Sysyem
{
    /// <summary>
    /// 用户（Admin）数据访问层
    /// </summary>
    public class UserRepository:BaseRepository<Admin,Admin>
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        public bool Login(string userName,string userPwd) 
        {
            var dbContext = new JooWMSEntities();
            var sql = dbContext.Admin.Where(item=>item.UserName == userName && item.PassWord == userPwd).ToList();
            return sql.Count > 0;
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<Admin> GetUserList(Expression<Func<Admin, bool>> where) 
        {
            var dbContext = new JooWMSEntities();
            return dbContext.Admin.Where(where).ToList();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteUser(int ID) 
        {
            var dbContext = new JooWMSEntities();
            var user = dbContext.Admin.Find(ID);
            user.IsDelete = 1;
            return dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool EditUser(Admin admin) 
        {
            var dbContext = new JooWMSEntities();
            var user_ = dbContext.Admin.Find(admin.ID);
            user_.UpdateTime = DateTime.Now;
            user_.DepartNum = admin.DepartNum;
            user_.RealName = admin.RealName;
            user_.UserName = admin.UserName;
            user_.RealName = admin.RealName;
            user_.Email = admin.Email;
            user_.Phone = admin.Phone;
            return dbContext.SaveChanges() > 0;
        }
    }
}
