using Com.Storage.Models;
using Com.Storage.Repository.Basic_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Com.Storage.Service.Store_Service
{
    /// <summary>
    /// 查询库存数量
    /// </summary>
    public class LocalProductService : BaseService<LocalProduct, LocalProduct>
    {
        public bool EditProduct(LocalProduct localProduct) 
        {
            var localProductRepository = new LocalProductRepository();
            return localProductRepository.EditProduct(localProduct);
        }

        public LocalProduct GetByProductNum(string productNum) 
        {
            return GetByWhere(item => item.ProductNum.Equals(productNum)).SingleOrDefault();
        }

        public bool EditNum(LocalProduct localProduct)
        {
            var localProductRepository = new LocalProductRepository();
            return localProductRepository.EditNum(localProduct);
        }

        public object ProductCount()
        {
            var localProductRepository = new LocalProductRepository();
            return localProductRepository.ProductCount();
        }

        public object GetList() 
        {
            var localProductRepository = new LocalProductRepository();
            return localProductRepository.GetList();
        }
        }
}
