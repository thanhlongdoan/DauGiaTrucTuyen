using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Web;
using System.Web.Script.Serialization;
using DauGiaTrucTuyen.Data;
using Microsoft.AspNet.SignalR;

namespace DauGiaTrucTuyen.HubRealTime
{
    public class AuctionHub : Hub
    {
        Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();

        public void JoinAuction(string productId, string userId, decimal? price)
        {
            var transaction = db.Transactions.Where(x => x.Product_Id == productId).FirstOrDefault();

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
        }

        public void TimerExpired()
        {
            var time = new Timer(10000);
            time.Start();
            Clients.All.a();
        }
    }
}