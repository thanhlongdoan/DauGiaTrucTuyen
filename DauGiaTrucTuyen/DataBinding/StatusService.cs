using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DauGiaTrucTuyen.DataBinding
{
    public static class StatusProduct
    {
        public const string Review = "Đang chờ duyệt";
        public const string Approved = "Đã duyệt";
        public const string Auctioning = "Đang đấu giá";
        public const string Transactioning = "Đang giao dich";
        public const string Transactioned = "Đã giao dich";
    }

    public static class StatusCategory
    {
        public const string Opened = "Mở";
        public const string Closed = "Đóng";
    }
}