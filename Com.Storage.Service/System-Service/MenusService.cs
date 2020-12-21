using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Com.Storage.Service;
using Com.Storage.Models;
using Com.Storage.Repository.System_Repository;

namespace Com.Storage.Service.System_Service
{
    public class MenusService : BaseService<SysResource, object>
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteMenus(int ID)
        {
            var menusRepository = new MenusRepository();
            return menusRepository.DeleteMenus(ID);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sysResource"></param>
        /// <returns></returns>
        public bool EditMenus(SysResource sysResource)
        {
            var menusRepository = new MenusRepository();
            return menusRepository.EditMenus(sysResource);
        }

        /// <summary>
        /// 条件查询  新建一个对象model
        /// </summary>
        /// <param name="resNum"></param>
        /// <param name="resName"></param>
        /// <returns></returns>
        public List<SysResource> GetByWhere(string resNum, string resName)
        {
            Expression<Func<SysResource, bool>> where = item => item.ResNum.Equals(resNum) || item.ResName.Equals(resName);
            return GetByWhere(where);
        }


        public SysResource GetByNum(string resNum)
        {
            var repository = new MenusRepository();
            return repository.GetByNum(resNum);
        }

    }
}