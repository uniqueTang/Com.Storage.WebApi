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
    ///  移库详情业务层
    /// </summary>
    public class MoveOrderDetailService : BaseService<MoveOrderDetail, MoveOrderDetail>
    {
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteDetail(int ID)
        {
            var moveOrderDetailRepository = new MoveOrderDetailRepository();
            return moveOrderDetailRepository.DeleteDetail(ID);
        }
     }
}
