using Com.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Com.Storage.Repository.Basic_Repository
{
    /// <summary>
    ///  库存
    /// </summary>
   public class LocalProductRepository:BaseRepository<LocalProduct, LocalProduct>
    {
       
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="localProduct"></param>
        /// <returns></returns>
        public bool EditProduct(LocalProduct localProduct)
        {
            var dbContext = new JooWMSEntities();
            var localProduct_ = dbContext.LocalProduct.Find(localProduct.ID);
            localProduct_.Num = localProduct_.Num - localProduct.Num;
            return dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 修改库存表中的数量
        /// </summary>
        /// <param name="localProduct"></param>
        /// <returns></returns>
        public bool EditNum(LocalProduct localProduct) 
        {
            var dbContext = new JooWMSEntities();
            var localProduct_ = dbContext.LocalProduct.FirstOrDefault(item => item.LocalNum == localProduct.LocalNum && item.BarCode == localProduct.BarCode);
            localProduct_.Num = localProduct.Num;
            return dbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 商品库存统计
        /// </summary>
        /// <returns></returns>
        public object ProductCount() 
        {
            var dbContext = new JooWMSEntities();
            var list = dbContext.LocalProduct.GroupBy(item =>new { item.ProductNum,item.ProductName}).Select(item=>new 
            {
                Count = item.Sum(i =>i.Num),
                Name = item.Key.ProductName
            });
            return list;
        }

        public object GetList() 
        {
            var dbContext = new JooWMSEntities();
            var list = dbContext.LocalProduct.GroupBy(item =>new { item.ProductNum, item.ProductName,}).Select(item=>new 
            {
                Num = item.Sum(i => i.Num),
                ProductNum = item.Key.ProductNum,
                ProductName = item.Key.ProductName
            });
            return list;
        }
    }
}
