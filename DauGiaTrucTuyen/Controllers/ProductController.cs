using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.DataBinding;
using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.IDataBinding;
using Microsoft.AspNet.SignalR;
using DauGiaTrucTuyen.HubRealTime;
using PagedList;

namespace DauGiaTrucTuyen.Controllers
{
    [System.Web.Mvc.Authorize]
    public class ProductController : Controller
    {
        // GET: Product
        private Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();

        private readonly IProduct _iProduct;

        public ProductController() : this(new ProductService())
        {
        }

        public ProductController(IProduct iProduct)
        {
            _iProduct = iProduct;
        }

        [AllowAnonymous]
        //Danh sách sản phẩm cho trang người dùng
        public ActionResult GetListProductFromCategory(string id, int? page)
        {
            int pageSize = 24;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            return View(_iProduct.GetListProductFromCategory(id).ToPagedList(pageIndex, pageSize));
        }

        [AllowAnonymous]
        //Chi tiết sản phẩm trang người dùng
        public ActionResult ProductDetail(string productId)
        {
            var result = _iProduct.DetailProduct(productId);
            return View(result);
        }
    }
}