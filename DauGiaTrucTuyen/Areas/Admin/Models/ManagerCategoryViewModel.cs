using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DauGiaTrucTuyen.Areas.Admin.Models
{
    public class ListCategoryViewModel
    {
        public string Categorys_Id { get; set; }

        [DisplayName("Tên danh mục")]
        public string CategoryName { get; set; }
    }
    public class DetailCategoryViewModel
    {
        public string Categorys_Id { get; set; }

        [DisplayName("Tên danh mục")]
        public string CategoryName { get; set; }
    }

    public class AddCategoryViewModel
    {
        [DisplayName("Tên danh mục")]
        [Required(ErrorMessage = "Tên danh mục là bắt buộc")]
        public string CategoryName { get; set; }
    }
    public class EditCategoryViewModel
    {
        public string Categorys_Id { get; set; }

        [DisplayName("Tên danh mục")]
        [Required(ErrorMessage = "Tên danh mục là bắt buộc")]
        public string CategoryName { get; set; }
    }
}