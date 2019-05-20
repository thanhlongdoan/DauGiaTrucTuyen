using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DauGiaTrucTuyen.Areas.Admin.Models
{
    public class ListProductViewModel
    {
        public string Products_Id { get; set; }

        [DisplayName("Tên sản phẩm")]
        public string ProductName { get; set; }

        [DisplayName("Thời gian đấu giá")]
        [Required(ErrorMessage = "Thời gian đấu giá là bắt buộc")]
        public TimeSpan? TimeLine { get; set; }

        [DisplayName("Giá khởi điểm")]
        [Required(ErrorMessage = "Giá khởi điểm là bắt buộc")]
        public decimal PriceStart { get; set; }

        [DisplayName("Trạng thái")]
        public string Status { get; set; }

        [DisplayName("Tên danh mục sản phẩm")]
        [Required(ErrorMessage = "Tên danh mục sản phẩm là bắt buộc")]
        public string CategoryName { get; set; }

        public DateTime? CreateDate { get; set; }
    }

    public class ListProductForPageClientViewModel
    {
        public string Products_Id { get; set; }

        public TimeSpan? TimeLine { get; set; }

        public DateTime? AuctionDateStart { get; set; }

        public string Image { get; set; }

        public decimal PriceStart { get; set; }
    }

    public class ListProductFullViewModel
    {
        public string Products_Id { get; set; }

        public TimeSpan? TimeLine { get; set; }

        public DateTime? AuctionDateStart { get; set; }

        public long TimeRemaining { get; set; }

        public string Image { get; set; }

        public decimal PriceStart { get; set; }

        public string Status { get; set; }
    }

    public class AddProductViewModel
    {
        public string Products_Id { get; set; }

        [DisplayName("Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        public string ProductName { get; set; }

        [DisplayName("Hình ảnh")]
        //[Required(ErrorMessage = "Hình ảnh là bắt buộc")]
        public string Image { get; set; }

        public string ImageMore1 { get; set; }

        public string ImageMore2 { get; set; }

        [AllowHtml]
        [DisplayName("Mô tả")]
        [Required(ErrorMessage = "Mô tả là bắt buộc")]
        public string Description { get; set; }

        [DisplayName("Thời gian đấu giá")]
        [Required(ErrorMessage = "Thời gian đấu giá là bắt buộc")]
        public long TimeLine { get; set; }

        [DisplayName("Giá khởi điểm")]
        [Required(ErrorMessage = "Giá khởi điểm là bắt buộc")]
        public decimal PriceStart { get; set; }

        [DisplayName("Bước giá")]
        [Required(ErrorMessage = "Bước giá là bắt buộc")]
        public int StepPrice { get; set; }

        [DisplayName("Trạng thái")]
        public int Status { get; set; }

        [DisplayName("Tên danh mục sản phẩm")]
        [Required(ErrorMessage = "Tên danh mục sản phẩm là bắt buộc")]
        public string Category_Id { get; set; }

        public string User_Id { get; set; }
    }

    public class EditProductViewModel
    {
        public string Products_Id { get; set; }

        [DisplayName("Tên sản phẩm")]
        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        public string ProductName { get; set; }

        [DisplayName("Hình ảnh")]
        public string Image { get; set; }

        public string ImageMore1 { get; set; }

        public string ImageMore2 { get; set; }

        [AllowHtml]
        [DisplayName("Mô tả")]
        [Required(ErrorMessage = "Mô tả là bắt buộc")]
        public string Description { get; set; }

        [DisplayName("Thời gian đấu giá")]
        [Required(ErrorMessage = "Thời gian đấu giá là bắt buộc")]
        public long TimeLine { get; set; }

        [DisplayName("Giá khởi điểm")]
        [Required(ErrorMessage = "Giá khởi điểm là bắt buộc")]
        public decimal? PriceStart { get; set; }

        [DisplayName("Bước giá")]
        [Required(ErrorMessage = "Bước giá là bắt buộc")]
        public int? StepPrice { get; set; }

        [DisplayName("Tên danh mục sản phẩm")]
        [Required(ErrorMessage = "Tên danh mục sản phẩm là bắt buộc")]
        public string Category_Id { get; set; }

        public string User_Id { get; set; }
    }

    public class DetailProductViewModel
    {
        public string Products_Id { get; set; }

        [DisplayName("Tên sản phẩm")]
        public string ProductName { get; set; }

        [DisplayName("Hình ảnh")]
        public string Image { get; set; }

        [DisplayName("Hình ảnh mở rộng 1")]
        public string ImageMore1 { get; set; }

        [DisplayName("Hình ảnh mở rộng 2")]
        public string ImageMore2 { get; set; }

        [AllowHtml]
        [DisplayName("Mô tả")]
        public string Description { get; set; }

        public long TimeRemaining { get; set; }

        [DisplayName("Thời gian đấu giá")]
        public TimeSpan? TimeLine { get; set; }

        [DisplayName("Giá khởi điểm")]
        public decimal? PriceStart { get; set; }

        [DisplayName("Bước giá")]
        public int? StepPrice { get; set; }

        [DisplayName("Tên danh mục sản phẩm")]
        public string CategoryName { get; set; }

        [DisplayName("Mã phiên đấu giá")]
        public string Transaction_Id { get; set; }

        [DisplayName("Giá cao nhất")]
        public decimal? AuctionPrice { get; set; }

        public string Status { get; set; }

        public List<ListTopAuction> ListTopAuction { get; set; }
    }

    public class ListTopAuction
    {
        public string UserName { get; set; }

        public decimal? PriceAuction { get; set; }
    }
}