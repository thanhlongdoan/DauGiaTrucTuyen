namespace DauGiaTrucTuyen.DataBinding
{
    public static class StatusProduct
    {
        public const string Review = "Đang chờ duyệt";
        public const string Auctioning = "Đang đấu giá";
        public const string Transactioning = "Đang giao dich";
        public const string Transactioned = "Đã giao dich";
    }

    public static class StatusTransactionAuction
    {
        public const string Win = "Thắng";
        public const string Lost = "Thua";
    }

    public static class StatusCategory
    {
        public const string Opened = "Mở";
        public const string Closed = "Đóng";
    }
    public static class StatusReport
    {
        public const string Responed = "Đã phản hồi";
        public const string NotResponed = "Chưa Phản Hồi";
    }

    public static class StatusBlockAuction
    {
        public const string Open = "Không khóa";
        public const string Close = "Khóa";
    }

    public static class StatusBlockUser
    {
        public const string Open = "Không khóa";
        public const string Close = "Khóa";
    }
}