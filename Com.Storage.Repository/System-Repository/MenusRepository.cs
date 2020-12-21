using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Com.Storage.Models;

namespace Com.Storage.Repository.System_Repository
{
    public class MenusRepository : BaseRepository<SysResource, object>
    {



        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteMenus(int ID)
        {
            var entities = new JooWMSEntities();
            var menus = entities.SysResource.Find(ID);
            menus.IsDelete = 1;
            return entities.SaveChanges() > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sysResource"></param>
        /// <returns></returns>
        public bool EditMenus(SysResource sysResource)
        {
            var entities = new JooWMSEntities();
            var menues = entities.SysResource.Find(sysResource.ID);
            menues.ResName = sysResource.ResName;
            menues.ParentNum = sysResource.ParentNum;
            menues.Sort = sysResource.Sort;
            menues.Url = sysResource.Url;
            menues.CssName = sysResource.CssName;
            menues.ResType = sysResource.ResType;
            return entities.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据父级菜单编号查询名称
        /// </summary>
        /// <param name="resNum"></param>
        /// <returns></returns>
        public SysResource GetByNum(string resNum)
        {
            var entities = new JooWMSEntities();
            return entities.SysResource.Find(resNum);
        }
    }
}
