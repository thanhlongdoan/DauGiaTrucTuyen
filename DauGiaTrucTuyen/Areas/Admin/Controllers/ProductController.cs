using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.DataBinding;
using DauGiaTrucTuyen.IDataBinding;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DauGiaTrucTuyen.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();

        private readonly IProduct _iProduct;

        public ProductController() : this(new ProductService())
        {
        }

        public ProductController(IProduct iProduct)
        {
            _iProduct = iProduct;
        }

        // GET: Admin/Product
        /// <summary>
        /// Danh sách sản phẩm đang đấu giá
        /// </summary>
        /// <param name="status">status == Auctioning </param>
        /// <returns></returns>
        public ActionResult Index(string status)
        {
            return View(_iProduct.GetListProduct(status));
        }
        /// <summary>
        /// Danh sách sản phẩm đang chờ phê duyệt
        /// </summary>
        /// <param name="status">status == Review </param>
        /// <returns></returns>
        public ActionResult GetListProductStatusReView(string status)
        {
            return View(_iProduct.GetListProduct(status));
        }

        //Tạo mới sản phẩm (GET)
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Category_Id = new SelectList(db.Categorys, "Categorys_Id", "CategoryName");
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "1:00 s", Value = "600000000" });
            items.Add(new SelectListItem { Text = "3:00 s", Value = "1800000000" });
            items.Add(new SelectListItem { Text = "5:00 s", Value = "3000000000" });
            items.Add(new SelectListItem { Text = "10:00 s", Value = "6000000000" });
            items.Add(new SelectListItem { Text = "30:00 s", Value = "18000000000" });
            items.Add(new SelectListItem { Text = "1:00:00 s", Value = "36000000000" });
            items.Add(new SelectListItem { Text = "2:00:00 s", Value = "72000000000" });
            items.Add(new SelectListItem { Text = "4:00:00 s", Value = "144000000000" });
            items.Add(new SelectListItem { Text = "8:00:00 s", Value = "288000000000" });
            items.Add(new SelectListItem { Text = "12:00:00 s", Value = "432000000000" });
            items.Add(new SelectListItem { Text = "24:00:00 s", Value = "863999999999" });
            ViewBag.SelectedItems = items;
            return View();
        }

        //Tạo mới sản phẩm (POST)
        [HttpPost]
        public ActionResult Create(AddProductViewModel model, HttpPostedFileBase file, HttpPostedFileBase file1, HttpPostedFileBase file2)
        {
            if (ModelState.IsValid)
            {
                if (_iProduct.Create(model, file, file1, file2, User.Identity.GetUserId()))
                    return RedirectToAction("Index", new { status = StatusProduct.Approved });
                return HttpNotFound();
            }
            else
            {
                ViewBag.Category_Id = new SelectList(db.Categorys, "Categorys_Id", "CategoryName");
                return View(model);
            }
        }

        [HttpGet]
        public bool Approved(string id)
        {
            return _iProduct.ApprovedProduct(id) == true ? true : false;
        }

        [HttpGet]
        public bool Delete(string productId)
        {
            return _iProduct.Delele(productId) == true ? true : false;
        }

        [HttpGet]
        public bool UnApproved(string productId)
        {
            return _iProduct.UnApproved(productId) == true ? true : false;
        }

        public ActionResult Detail(string id)
        {
            var result = _iProduct.DetailProduct(id);
            if (result != null)
                return View(result);
            return HttpNotFound();
        }

        [AllowAnonymous]
        public ActionResult Set()
        {
            var transaction = db.Transactions.Where(x => x.Product.StatusProduct.Equals(StatusProduct.Auctioning) || x.Product.StatusProduct.Equals(StatusProduct.Transactioning)).ToList();

            var transactionAuction = db.TransactionAuctions.ToList();
            foreach (var item in transactionAuction)
            {
                db.TransactionAuctions.Remove(item);
                db.SaveChanges();
            }
            foreach (var item in transaction)
            {
                item.AuctionDateStart = DateTime.Now;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();

                TransactionAuction itemTransactionAuction = new TransactionAuction()
                {
                    Transaction_Id = item.Transaction_Id,
                    User_Id = item.Product.User_Id,
                    AuctionPrice = item.PriceStart,
                    AuctionTime = DateTime.Now
                };
                db.TransactionAuctions.Add(itemTransactionAuction);
                db.SaveChanges();
            }

            var product = db.Products.Where(x => x.StatusProduct.Equals(StatusProduct.Transactioning)).ToList();
            foreach (var item in product)
            {
                item.StatusProduct = StatusProduct.Auctioning;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
            return View();
        }
    }
}