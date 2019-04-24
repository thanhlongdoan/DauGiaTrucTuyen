using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DauGiaTrucTuyen.Areas.Admin.Models
{
    public class ListAuctioningViewModel
    {
        public string Transaction_Id { get; set; }

        public string Product_Id { get; set; }

        [Display(Name = "Tiên sản phẩm")]
        public string ProductName { get; set; }

        [Display(Name = "Giá tiền hiện tại")]
        public double AuctionPrice { get; set; }
    }

    public class ListAuctionWinViewModel
    {
        public string Transaction_Id { get; set; }

        public string Product_Id { get; set; }

        [Display(Name = "Tiên sản phẩm")]
        public string ProductName { get; set; }

        [Display(Name = "Giá tiền cao nhất")]
        public double AuctionPrice { get; set; }
    }

    public class ListAuctionLostViewModel
    {
        public string Transaction_Id { get; set; }

        public string Product_Id { get; set; }

        [Display(Name = "Tiên sản phẩm")]
        public string ProductName { get; set; }

        [Display(Name = "Giá tiền")]
        public double AuctionPrice { get; set; }
    }
}