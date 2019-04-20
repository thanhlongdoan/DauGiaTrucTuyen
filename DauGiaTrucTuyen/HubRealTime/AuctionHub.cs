using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.DataBinding;
using Microsoft.AspNet.SignalR;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Web;

namespace DauGiaTrucTuyen.HubRealTime
{
    public class AuctionHub : Hub
    {
        Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();

        public AuctionHub()
        {
        }

        //check phiên đấu giá kết thúc ngay tại trang chi tiết
        public void EndTime()
        {
            var transactions = db.Transactions.Where(x => x.Product.StatusProduct.Equals(StatusProduct.Auctioning)).ToList();
            foreach (var item in transactions)
            {
                if (DateTime.Now > (item.AuctionDateStart + item.TimeLine))
                {
                    Clients.Group(item.Transaction_Id).EndTime("End");
                }
            }
        }

        //kiểm tra phiên đấu giá kết thúc ngay tại trang hiển thị danh sách đấu giá của người dùng
        public void CheckEndTime()
        {
            var transactions = db.Transactions.Where(x => x.Product.StatusProduct.Equals(StatusProduct.Auctioning)).ToList();
            foreach (var item in transactions)
            {
                if (DateTime.Now > (item.AuctionDateStart + item.TimeLine))
                {
                    Clients.All.EndTimeInListView(item.Product_Id);
                }
            }
        }

        public void JoinGroupAuction(string productId)
        {
            var transaction = db.Transactions.Where(x => x.Product_Id == productId && x.Product.StatusProduct.Equals(StatusProduct.Auctioning)).FirstOrDefault();

            if (transaction != null)
            {
                Groups.Add(Context.ConnectionId, transaction.Transaction_Id);
            }
        }

        public void JoinAuction(string productId, string userId, decimal? price)
        {
            var transaction = db.Transactions.Where(x => x.Product_Id == productId && x.Product.StatusProduct.Equals(StatusProduct.Auctioning)).FirstOrDefault();

            decimal? priceing = db.TransactionAuctions.Where(x => x.Transaction_Id == transaction.Transaction_Id).Max(x => x.AuctionPrice);
            if (DateTime.Now > (transaction.AuctionDateStart + transaction.TimeLine))
            {
                Clients.Caller.AuctionError("Phiên đấu giá đã kết thúc !");
            }
            else
            {
                if (price > priceing && transaction != null)
                {
                    TransactionAuction transactionAuction = new TransactionAuction();
                    transactionAuction.Transaction_Id = transaction.Transaction_Id;
                    transactionAuction.User_Id = userId;
                    transactionAuction.AuctionTime = DateTime.Now;
                    transactionAuction.AuctionPrice = price;
                    db.TransactionAuctions.Add(transactionAuction);
                    db.SaveChanges();

                    Groups.Add(Context.ConnectionId, transaction.Transaction_Id);

                    Clients.Group(transaction.Transaction_Id).Auctioning(price, HttpContext.Current.User.Identity.Name);
                }
                else
                    Clients.Caller.AuctionError("Giá tiền phải lớn hơn giá tiền hiện tại !");
            }
        }
    }
}