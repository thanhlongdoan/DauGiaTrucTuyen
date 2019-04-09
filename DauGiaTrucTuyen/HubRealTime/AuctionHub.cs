using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.DataBinding;
using Microsoft.AspNet.SignalR;
using System;
using System.Linq;
using System.Threading;

namespace DauGiaTrucTuyen.HubRealTime
{
    public class AuctionHub : Hub
    {
        Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();
        public static bool initialized = false;
        public static object initLock = new object();
        static private Timer timer;
        static private int counter = 0;
        static public Auction auction;
        public static int secs_10 = 1000;

        public AuctionHub()
        {

        }

        public void JoinGroupAuction(string productId)
        {
            var transaction = db.Transactions.Where(x => x.Product_Id == productId && x.Product.StatusProduct.Equals(StatusProduct.Auctioning)).FirstOrDefault();

            if (transaction != null)
            {
                Groups.Add(Context.ConnectionId, transaction.Transaction_Id);

                if (initialized)
                    return;

                lock (initLock)
                {
                    if (initialized)
                        return;

                    InitializeAuction(transaction.AuctionTime.Value.TotalSeconds);
                }
            }

            //else
            //    Clients.Caller.JoinGroupError("Phiên đấu giá này đã kết thúc !");
        }
        public void JoinAuction(string productId, string userId, decimal? price)
        {
            var transaction = db.Transactions.Where(x => x.Product_Id == productId && x.Product.StatusProduct.Equals(StatusProduct.Auctioning)).FirstOrDefault();

            decimal? priceing = db.TransactionAuctions.Where(x => x.Transaction_Id == transaction.Transaction_Id).Max(x => x.AuctionPrice);

            if (price > priceing && transaction != null)
            {
                TransactionAuction transactionAuction = new TransactionAuction();
                transactionAuction.Transaction_Id = transaction.Transaction_Id;
                transactionAuction.User_Id = userId;
                transactionAuction.AuctionDate = DateTime.Now;
                transactionAuction.AuctionPrice = price;
                db.TransactionAuctions.Add(transactionAuction);
                db.SaveChanges();

                Groups.Add(Context.ConnectionId, transaction.Transaction_Id);

                Clients.Group(transaction.Transaction_Id).Auctioning(price);
            }
            else
                Clients.Caller.AuctionError("Giá tiền phải lớn hơn giá tiền hiện tại !");
        }

        private void InitializeAuction(double seconds)
        {
            auction = new Auction(0, 10, DateTime.Now.AddSeconds(seconds), 0);

            timer = new Timer(TimerExpired, null, secs_10, 0);

            initialized = true;
        }

        public void AddMessage(string msg)
        {
            Clients.All.addMessage(msg);
        }

        public void TimerExpired(object state)
        {
            if (auction.TimeRemaining > 0)
            {
                AddMessage(string.Format("Push message from server {0} - {1:hh\\:mm\\:ss} - {2}", counter++, auction.GetTimeRemaining(), auction.TimeRemaining));
                Clients.All.GetTimeAuction(string.Format("{0:hh\\:mm\\:ss}", auction.GetTimeRemaining()));
                timer.Change(secs_10, 0);
            }
            else
            {
                timer.Dispose();
                AddMessage("End");
            }
        }
    }
}