using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DauGiaTrucTuyen.DataBinding;
using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.IDataBinding;

namespace DauGiaTrucTuyen.Controllers
{
    public class CategoryController : Controller
    {
        private Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();

        private readonly ICategory _iCategory;

        public CategoryController() : this(new CategoryService())
        {
        }

        public CategoryController(ICategory iCategory)
        {
            _iCategory = iCategory;
        }

        //Trả về danh mục sản phẩm ra slide bar
        public ActionResult GetCategorySideBar()
        {
            return PartialView(_iCategory.GetListCategoryForClient());
        }

        //Trả về danh mục sản phẩm ra footer
        public ActionResult GetCategoryFooter()
        {
            return PartialView(_iCategory.GetListCategoryForClient());
        }
    }
}