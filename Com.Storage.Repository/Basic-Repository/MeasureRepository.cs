using Com.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Repository.Basic_Repository
{
    /// <summary>
    /// 单位（数据访问层）
    /// </summary>
    public class MeasureRepository: BaseRepository<Measure, Measure>
    {
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<Measure> GetMeasureList(Expression<Func<Measure, bool>> where)
        {
            var dbContext = new JooWMSEntities();
            return dbContext.Measure.Where(where).ToList();
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteMeasure(int ID)
        {
            var dbContext = new JooWMSEntities();
            var delMeasure = dbContext.Measure.Find(ID);
            dbContext.Measure.Remove(delMeasure);
            return dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="measure"></param>
        /// <returns></returns>
        public bool EditMeasure(Measure measure) 
        {
            var dbContext = new JooWMSEntities();
            var measure_ = dbContext.Measure.Find(measure.ID);
            measure_.MeasureName = measure.MeasureName;
            return dbContext.SaveChanges() > 0;
        }
    }
}
