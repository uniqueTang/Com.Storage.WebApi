using Com.Storage.Models;
using Com.Storage.Repository.Basic_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Service.Basic_Service
{
    /// <summary>
    /// 单位业务逻辑层
    /// </summary>
    public class MeasureService : BaseService<Measure, Measure>
    {
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="byName"></param>
        /// <returns></returns>
        public List<Measure> GetMeasureList(string byName)
        {
            var measureRepository = new MeasureRepository();
            Expression<Func<Measure, bool>> where = item => item.ID != -1;
            if (!string.IsNullOrEmpty(byName)) where = where.And(item => item.MeasureNum.IndexOf(byName) != -1 || item.MeasureName.IndexOf(byName) != -1);
            return measureRepository.GetMeasureList(where);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteMeasure(int ID)
        {
            var measureRepository = new MeasureRepository();
            return measureRepository.DeleteMeasure(ID);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="measure"></param>
        /// <returns></returns>
        public bool EditMeasure(Measure measure)
        {
            var measureRepository = new MeasureRepository();
            return measureRepository.EditMeasure(measure);
        }
    }
}
