using AutoMapper;
using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.DataBinding;
using DauGiaTrucTuyen.IDataBinding;
using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;

namespace DauGiaTrucTuyen.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();
        // GET: Admin/Category

        private readonly ICategory _iCategory;

        public CategoryController() : this(new CategoryService())
        {
        }

        public CategoryController(ICategory iCategory)
        {
            _iCategory = iCategory;
        }

        //Danh sách danh mục
        public ActionResult Index()
        {
            return View(_iCategory.GetListCategory());
        }


        //Chi tiết danh mục
        public ActionResult Details(string id)
        {
            var result = _iCategory.DetailCategory(id);
            if (result != null)
                return View(result);
            return HttpNotFound();
        }

        //Tạo mới danh mục (GET)
        public ActionResult Create()
        {
            return View();
        }

        //Tạo mới danh mục (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                _iCategory.AddCategory(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        //Cập nhật danh mục (GET)
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Data.Category category = db.Categorys.Find(id);
            var model = Mapper.Map<EditCategoryViewModel>(category);
            if (model == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Mở", Value = "Mở" });
            items.Add(new SelectListItem { Text = "Đóng", Value = "Đóng" });
            ViewBag.SelectedDefault = category.StatusCategory;
            ViewBag.SelectedItems = items;
            return View(model);
        }

        //Cập nhật danh mục (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                _iCategory.EditCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        //Xóa danh mục
        public bool Delete(string id)
        {
            if (_iCategory.DeleteCategory(id))
                return true;
            return false;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
