using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Common;
using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.DataBinding;
using DauGiaTrucTuyen.IDataBinding;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DauGiaTrucTuyen.Controllers
{
    public class ManagerAuctionController : Controller
    {
        private Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();
        private readonly IProduct _iProduct;

        public ManagerAuctionController() : this(new ProductService())
        {
        }

        public ManagerAuctionController(IProduct product)
        {
            _iProduct = product;
        }

        // GET: ManagerAuction
        public ActionResult Index()
        {
            return View(_iProduct.GetListProductForClient(User.Identity.GetUserId()));
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
            ViewBag.SelectedItems = items;
            return View();
        }

        //Tạo mới sản phẩm (POST)
        [HttpPost]
        public ActionResult Create(AddProductViewModel model, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (_iProduct.CreateForClient(model, file, User.Identity.GetUserId()))
                    return RedirectToAction("Index", new { status = StatusProduct.Approved });
                return HttpNotFound();
            }
            else
            {
                ViewBag.Category_Id = new SelectList(db.Categorys, "Categorys_Id", "CategoryName");
                return View(model);
            }
        }
    }
}