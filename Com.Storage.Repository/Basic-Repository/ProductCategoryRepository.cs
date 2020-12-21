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
    /// 产品类别（数据访问层）
    /// </summary>
    public class ProductCategoryRepository:BaseRepository<ProductCategory, ProductCategory>
    {
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<ProductCategory> GetbyList(Expression<Func<ProductCategory, bool>> where) 
        {
            var dbContext = new JooWMSEntities();
            return dbContext.ProductCategory.Where(where).ToList();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteCategory(int ID)
        {
            var dbContext = new JooWMSEntities();
            var productCategory = dbContext.ProductCategory.Find(ID);
            productCategory.IsDelete = 1;
            return dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="productCategory"></param>
        /// <returns></returns>
        public bool EditCategory(ProductCategory productCategory) 
        {
            var dbContext = new JooWMSEntities();
            var productCategory_ = dbContext.ProductCategory.Find(productCategory.ID);
            productCategory_.CateName = productCategory.CateName;
            productCategory_.Remark = productCategory.Remark;
            return dbContext.SaveChanges() > 0;
        }

    }
}
