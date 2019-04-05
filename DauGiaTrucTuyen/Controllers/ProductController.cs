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

namespace DauGiaTrucTuyen.Controllers
{
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


        //Danh sách sản phẩm cho trang người dùng
        public ActionResult GetListProductForPageClient()
        {
            return PartialView(_iProduct.GetListProductForPageClient());
        }

        //Danh sách sản phẩm cho trang người dùng
        public ActionResult GetListProductFromCategory(string id)
        {
            return View(_iProduct.GetListProductFromCategory(id));
        }

        //Chi tiết sản phẩm trang người dùng
        public ActionResult ProductDetail(string productId)
        {
            var result = _iProduct.DetailProduct(productId);
            return View(result);
        }
    }
}