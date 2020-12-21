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
    /// 产品（Product）数据访问层
    /// </summary>
    public class ProductRepository: BaseRepository<Product, Product>
    {
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public List<Product> GetProductList(Expression<Func<Product, bool>> where) 
        {
            var dbContext = new JooWMSEntities();
            return dbContext.Product.Where(where).ToList();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteProduct(int ID)
        {
            var dbContext = new JooWMSEntities();
            var product = dbContext.Product.Find(ID);
            product.IsDelete = 1;
            return dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public bool EditProduct(Product product) 
        {
            var dbContext = new JooWMSEntities();
            var product_ = dbContext.Product.Find(product.ID);
            product_.ProductName = product.ProductName;
            product_.MinNum = product.MinNum;
            product_.MaxNum = product.MaxNum;
            product_.UnitNum = product.UnitNum;
            product_.UnitName = product.UnitName;
            product_.CateNum = product.CateNum;
            product_.CateName = product.CateName;
            product_.Size = product.Size;
            product_.InPrice = product.InPrice;
            product_.OutPrice = product.InPrice;
            product_.AvgPrice = product.AvgPrice;
            product_.CusNum = product.CusNum;
            product_.CusName = product.CusName;
            product_.Remark = product.Remark;
            return dbContext.SaveChanges() > 0;
        }
    }
}
