using Com.Storage.Models;
using Com.Storage.Service.Basic_Service;
using Com.Storage.Service.Store_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Com.Storage.WebApi.Controllers.Basic_Controllers
{
    /// <summary>
    /// 产品 Api
    /// </summary>
    public class ProductController : ApiController
    {

        /// <summary>
        /// 获取所有未被删除的产品
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetProductList() 
        {
            var productService = new ProductService();
            var productList = productService.GetAll().Where(item=>item.IsDelete!=1).OrderByDescending(item => item.CreateTime);
            var result = new 
            {
                list = productList
            };
            return Json(result);
        }

        /// <summary>
        /// 查产品数量价格以及库位
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult GetByLocalProductList() 
        {          
            var localProductService = new LocalProductService();
            var listReuslt = localProductService.GetAll();
            var listObj = new List<Object>();
            listReuslt.ToList().ForEach(item =>
            {
                var productService = new ProductService();
                var newProduct = productService.FindByBarCode(item.BarCode);
                var obj = new 
                {
                    ID = item.ID,
                    Num = item.Num ==0?"库存不足":""+item.Num+"",
                    ProductName = item.ProductName,
                    BarCode = item.BarCode,
                    ProductNum = item.ProductNum,
                    BatchNum = item.BatchNum,
                    LocalNum = item.LocalNum,
                    LocalName = item.LocalName,
                    StorageNum = item.StorageNum,
                    Size = newProduct.Size,
                    InPrice = newProduct.InPrice,
                };
                listObj.Add(obj);
            });
            var result = new
            {
                list = listObj
            };
            return Json(result);
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="byName"></param>
        /// <param name="cateNum"></param>
        /// <returns></returns>
        public IHttpActionResult GetByProductList(string byName, string cateNum) 
        {
            var productService = new ProductService();
            var productList = productService.GetProductList(byName,cateNum).OrderByDescending(item => item.CreateTime);
            var result = new 
            {
                list = productList
            };
            return Json(result);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public IHttpActionResult AddProduct(Product product) 
        {
            var productService = new ProductService();
            var maxSnNum = productService.GetAll().Select(item => item.SnNum).LastOrDefault();
            int i = Int32.Parse(maxSnNum);
            i++;
            product.SnNum = i.ToString().PadLeft(6, '0');
            product.Num = 0;    product.OutPrice = product.InPrice;
            product.AvgPrice = product.InPrice;   product.NetWeight = 0;
            product.GrossWeight = 0; product.IsDelete = 0;
            product.CreateTime = DateTime.Now;
            var addProduct = productService.Add(product);
            var result = new 
            {
                Count = addProduct?"添加成功":"添加失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DeleteProduct(int ID)
        {
            var productService = new ProductService();
            var delProduct = productService.DeleteProduct(ID);
            var result = new
            {
                Count = delProduct ? "删除成功!" : "删除失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="delArr"></param>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DelArr(List<int> delArr)
        {
            var productService = new ProductService();
            bool isResult = true;
            try
            {
                delArr.ForEach(item =>
                {
                    productService.DeleteProduct(item);
                });
            }
            catch (Exception)
            {
                isResult = false;
                throw;
            }
            var result = new
            {
                Msg = isResult ? "删除成功" : "删除失败"
            };
            return Json(result);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult EditProduct(Product product) 
        {
            var productService = new ProductService();
            var editProduct = productService.EditProduct(product);
            var result = new 
            {
                Count = editProduct?"编辑成功!":"编辑失败"
            };
            return Json(result);
        }
    }
}
