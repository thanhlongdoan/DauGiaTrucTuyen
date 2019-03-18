using AutoMapper;
using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Data;
using System.Net;
using System.Web.Mvc;

namespace DauGiaTrucTuyen.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();
        private DataBinding.Category _iCategory = new DataBinding.Category();
        // GET: Admin/Category
        public ActionResult Index()
        {
            return View(_iCategory.GetListCategory());
        }

        public ActionResult Details(string id)
        {
            var result = _iCategory.DetailUser(id);
            if (result != null)
                return View(result);
            return HttpNotFound();
        }

        public ActionResult Create()
        {
            return View();
        }

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
            return View(model);
        }

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
