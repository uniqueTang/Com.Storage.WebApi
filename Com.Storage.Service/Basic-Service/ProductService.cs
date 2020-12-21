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
    /// 产品（Product）业务逻辑层
    /// </summary>
    public class ProductService : BaseService<Product, Product>
    {
        public Product FindByRoleNum(string proNum)
        {
            return GetByWhere(item => item.SnNum.Equals(proNum)).SingleOrDefault();
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="byName"></param>
        /// <param name="cateNum"></param>
        /// <returns></returns>
        public List<Product> GetProductList(string byName, string cateNum)
        {
            var productRepository = new ProductRepository();
            Expression<Func<Product, bool>> where = item => item.IsDelete != 1;
            if (!string.IsNullOrEmpty(byName)) where = where.And(item => item.ProductName.IndexOf(byName) != -1);
            if (!string.IsNullOrEmpty(cateNum)) where = where.And(item => item.CateNum == cateNum);
            return productRepository.GetProductList(where);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool DeleteProduct(int ID)
        {
            var productRepository = new ProductRepository();
            return productRepository.DeleteProduct(ID);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public bool EditProduct(Product product)
        {
            var productRepository = new ProductRepository();
            return productRepository.EditProduct(product);
        }

        /// <summary>
        /// 产品编号查询
        /// </summary>
        /// <param name="barCode"></param>
        /// <returns></returns>
        public Product FindByBarCode(string barCode) 
        {
            return GetByWhere(item => item.BarCode == barCode).SingleOrDefault();
        }
    }
}
