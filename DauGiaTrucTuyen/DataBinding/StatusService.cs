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
        public const string Transactioning = "Đang giao dịch";
        public const string Transaction = "Đã giao dịch";
    }

    public static class StatusCategory
    {
        public const string Opened = "Mở";
        public const string Closed = "Đóng";
    }
}