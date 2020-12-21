using Com.Storage.Models;
using Com.Storage.Service.Basic_Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Com.Storage.WebApi.Controllers.Basic_Controllers
{
    /// <summary>
    /// 产品类别（Api）
    /// </summary>
    public class ProductCategoryController : ApiController
    {
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="byName"></param>
        /// <returns></returns>
        public IHttpActionResult GetByList(string byName) 
        {
            var productCategoryService = new ProductCategoryService();
            var CategoryList = productCategoryService.GetbyList(byName).OrderByDescending(item => item.CreateTime);
            var result = new 
            {
                list = CategoryList
            };
            return Json(result);
        }

        /// <summary>
        /// /添加
        /// </summary>
        /// <param name="productCategory"></param>
        /// <returns></returns>
        public IHttpActionResult AddCategory(ProductCategory productCategory) 
        {
            var productCategoryService = new ProductCategoryService();
            var maxCateNum = productCategoryService.GetAll().Select(item => item.CateNum).LastOrDefault();
            int i = Int32.Parse(maxCateNum);
            i++;
            productCategory.CateNum = i.ToString().PadLeft(6, '0');
            productCategory.IsDelete = 0;
            productCategory.CreateTime = DateTime.Now;
            productCategory.CreateUser = "DA_0000";
            var addCategory = productCategoryService.Add(productCategory);
            var result = new 
            {
                Count = addCategory
            };
            return Json(result);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        public IHttpActionResult DeleteProduct(int ID) 
        {
            var productCategoryService = new ProductCategoryService();
            var deleteCategorg = productCategoryService.DeleteCategory(ID);
            var result = new 
            {
                Count = deleteCategorg?"删除成功":"删除失败"
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
            var productCategoryService = new ProductCategoryService();
            bool isResult = true;
            try
            {
                delArr.ForEach(item =>
                {
                    productCategoryService.DeleteCategory(item);
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
        /// <param name="productCategory"></param>
        /// <returns></returns>
        [HttpPut]
        public IHttpActionResult EditCategory(ProductCategory productCategory) 
        {
            var productCategoryService = new ProductCategoryService();
            var editCategory = productCategoryService.EditCategory(productCategory);
            var result = new 
            {
                Count = editCategory?"修改成功":"修改失败"
            };
            return Json(result);
        }
    }
}
