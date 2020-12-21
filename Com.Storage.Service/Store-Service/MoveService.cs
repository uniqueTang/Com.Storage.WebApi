using Com.Storage.Models;
using Com.Storage.Repository.Store_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Service.Store_Service
{
    public class MoveService : BaseService<MoveOrder, MoveOrder>
    {
        /// <summary>
        /// 调价查询
        /// </summary>
        /// <param name="Status"></param>
        /// <param name="OrderNum"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<MoveOrder> GetMoveOrderList(int Status, string OrderNum, DateTime startTime, DateTime endTime)
        {
            var moveOrder = new MoveRepository();
            Expression<Func<MoveOrder, bool>> where = item => item.CreateTime >= startTime && item.CreateTime <= endTime && item.IsDelete != 1 && item.IsDelete != 1;
            if (Status != 0) where = where.And(item => item.Status == Status);
            if (!string.IsNullOrEmpty(OrderNum)) where = where.And(item => item.OrderNum.IndexOf(OrderNum) != -1);
            return moveOrder.GetMoveOrderList(where);
        }

        /// <summary>
        /// 审核修改
        /// </summary>
        /// <param name="moveOrder"></param>
        /// <returns></returns>
        public bool EditCheckMove(MoveOrder moveOrder)
        {
            var moveOrderRes = new MoveRepository();
            return moveOrderRes.EditCheckMove(moveOrder);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteMove(int ID)
        {
            var moveOrder = new MoveRepository();
            return moveOrder.DeleteMove(ID);
        }

        /// <summary>
        /// 修改移库
        /// </summary>
        /// <param name="moveOrder"></param>
        /// <returns></returns>
        public bool EditMove(MoveOrder moveOrder)
        {
            var moveOrderRes = new MoveRepository();
            return moveOrderRes.EditMove(moveOrder);
        }
    }
}
