﻿using DauGiaTrucTuyen.Data;
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
            if (initialized)
                return;

            lock (initLock)
            {
                if (initialized)
                    return;

                InitializeAuction();
            }
        }

        public void JoinAuction(string productId, string userId, decimal? price)
        {
            var transaction = db.Transactions.Where(x => x.Product_Id == productId && x.Product.StatusProduct.Equals(StatusProduct.Approved)).FirstOrDefault();

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

        private void InitializeAuction()
        {
            auction = new Auction(0, 10, DateTime.Now.AddSeconds(30), 0);

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