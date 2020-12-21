using Com.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Repository.Store_Repository
{
    /// <summary>
    /// 移库数据访问层
    /// </summary>
    public class MoveRepository:BaseRepository<MoveOrder,MoveOrder>
    {
        /// <summary>
        /// 移库条件查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<MoveOrder> GetMoveOrderList(Expression<Func<MoveOrder, bool>> where)
        {
            var dbContext = new JooWMSEntities();
            return dbContext.MoveOrder.Where(where).ToList();
        }

        /// <summary>
        /// 修改审核入库单号
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        public bool EditCheckMove(MoveOrder moveOrder)
        {
            var dbContext = new JooWMSEntities();
            var moveOrder_ = dbContext.MoveOrder.Find(moveOrder.ID);
            moveOrder_.Status = moveOrder.Status;
            moveOrder_.Reason = moveOrder.Reason;
            return dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteMove(int ID)
        {
            var dbContext = new JooWMSEntities();
            var delMove = dbContext.MoveOrder.Find(ID);
            delMove.IsDelete = 1;
            return dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 修改移库信息
        /// </summary>
        /// <param name="moveOrder"></param>
        /// <returns></returns>
        public bool EditMove(MoveOrder moveOrder) 
        {
            var dbContext = new JooWMSEntities();
            var moveOrder_ = dbContext.MoveOrder.Find(moveOrder.ID);
            moveOrder_.MoveType = moveOrder.MoveType;
            moveOrder_.Remark = moveOrder.Remark;
            moveOrder_.ContractOrder = moveOrder_.ContractOrder;
            return dbContext.SaveChanges() > 0;
        }
    }
}
