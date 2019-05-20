using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.DataBinding;
using DauGiaTrucTuyen.IDataBinding;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace DauGiaTrucTuyen.Controllers
{
    [Authorize]
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

        public bool EndAuction(string productId)
        {
            if (_iProduct.EndAuction(productId))
                return true;
            return false;
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
            //model.Image = file.ToString();
            if (ModelState.IsValid)
            {
                if (_iProduct.CreateForClient(model, file, file1, file2, User.Identity.GetUserId()))
                    return RedirectToAction("ConfirmAddProduct");
                return HttpNotFound();
            }
            else
            {
                ViewBag.Category_Id = new SelectList(db.Categorys, "Categorys_Id", "CategoryName");
                return View(model);
            }
        }

        [AllowAnonymous]
        public ActionResult Edit(string id)
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
            var result = _iProduct.GetViewEditProduct(id);
            return View(result);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Edit(EditProductViewModel model, HttpPostedFileBase file, HttpPostedFileBase file1, HttpPostedFileBase file2)
        {
            if (ModelState.IsValid)
            {
                _iProduct.Edit(model, file, file1, file2);
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Detail(string id)
        {
            var result = _iProduct.DetailProduct(id);
            if (result != null)
                return View(result);
            return HttpNotFound();
        }

        public ActionResult MailContent()
        {
            return View();
        }

        public ActionResult MailContentForUserAdd()
        {
            return View();
        }

        public ActionResult ConfirmAddProduct()
        {
            return View();
        }
    }
}