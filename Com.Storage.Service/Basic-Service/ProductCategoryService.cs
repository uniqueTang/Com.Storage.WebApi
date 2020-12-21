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
    ///  产品类别（业务层）
    /// </summary>
    public class ProductCategoryService : BaseService<ProductCategory, ProductCategory>
    {
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="byName"></param>
        /// <returns></returns>
        public List<ProductCategory> GetbyList(string byName) 
        {
            var productCategoryRepository = new ProductCategoryRepository();
            Expression<Func<ProductCategory, bool>> where = item => item.IsDelete != 1;
            if (!string.IsNullOrEmpty(byName)) where = where.And(item => item.CateNum.IndexOf(byName)!=-1 || item.CateName.IndexOf(byName)!=-1);
            return productCategoryRepository.GetbyList(where);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteCategory(int ID) 
        {
            var productCategoryRepository = new ProductCategoryRepository();
            return productCategoryRepository.DeleteCategory(ID);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="productCategory"></param>
        /// <returns></returns>
        public bool EditCategory(ProductCategory productCategory) 
        {
            var productCategoryRepository = new ProductCategoryRepository();
            return productCategoryRepository.EditCategory(productCategory);
        }
    }
}
