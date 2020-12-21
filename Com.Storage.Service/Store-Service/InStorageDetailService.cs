using Com.Storage.Models;
using Com.Storage.Repository.Store_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Service.Store_Service
{
    /// <summary>
    /// 入库详情（业务逻辑层）
    /// </summary>
    public class InStorageDetailService : BaseService<InStorDetail, InStorDetail>
    {
        /// <summary>
        /// 添加入库详情
        /// </summary>
        /// <param name="inStor"></param>
        /// <returns></returns>
        public bool AddDetail(InStorDetail inStor)
        {
            var inStorDetail = new InStorageDetailRespository();
            return inStorDetail.AddDetail(inStor);
        }

        /// <summary>
        /// 删除详情
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteDetail(int ID)
        {
            var inStorDetail = new InStorageDetailRespository();
            return inStorDetail.DeleteDetail(ID);
        }
    }
}
