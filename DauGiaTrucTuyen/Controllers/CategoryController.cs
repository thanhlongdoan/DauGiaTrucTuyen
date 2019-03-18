using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DauGiaTrucTuyen.DataBinding;
using DauGiaTrucTuyen.Data;

namespace DauGiaTrucTuyen.Controllers
{
    public class CategoryController : Controller
    {
        private DataBinding.Category _iCategory = new DataBinding.Category();
        private Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();

        //Get danh mục sản phẩm ra slide bar
        public ActionResult GetCategorySideBar()
        {
            return PartialView(_iCategory.GetListCategory());
        }

        //Get danh mục sản phẩm ra footer
        public ActionResult GetCategoryFooter()
        {
            return PartialView(_iCategory.GetListCategory());
        }
    }
}