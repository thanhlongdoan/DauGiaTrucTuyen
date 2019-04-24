using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.IDataBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DauGiaTrucTuyen.DataBinding
{
    public class MyAuctionService : IMyAuction
    {
        Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();

        public List<ListAuctioningViewModel> ListAuctioning(string sessionUserId)
        {
            return (from product in db.Products
                    join productDetail in db.ProductDetails on product.Products_Id equals productDetail.Product_Id
                    join transaction in db.Transactions on product.Products_Id equals transaction.Product_Id
                    join transactionAuction in db.TransactionAuctions on transaction.Transaction_Id equals transactionAuction.Transaction_Id
                    where transactionAuction.User_Id == sessionUserId
                        && product.StatusProduct.Equals(StatusProduct.Auctioning)
                    select new ListAuctioningViewModel
                    {
                        Product_Id = product.Products_Id,
                        ProductName = productDetail.ProductName,
                        Transaction_Id = transaction.Transaction_Id,
                        AuctionPrice = (double)db.TransactionAuctions.Where(x => x.Transaction.Transaction_Id == transaction.Transaction_Id).Max(x => x.AuctionPrice),
                    }).ToList();
        }

        public List<ListAuctionWinViewModel> ListAuctionWin(string sessionUserId)
        {
            return (from product in db.Products
                    join productDetail in db.ProductDetails on product.Products_Id equals productDetail.Product_Id
                    join transaction in db.Transactions on product.Products_Id equals transaction.Product_Id
                    join transactionAuction in db.TransactionAuctions on transaction.Transaction_Id equals transactionAuction.Transaction_Id
                    where transactionAuction.Status.Equals(StatusTransactionAuction.Win)
                        && transactionAuction.User_Id == sessionUserId
                        && product.StatusProduct.Equals(StatusProduct.Transactioning)
                        || transactionAuction.User_Id == sessionUserId
                        && product.StatusProduct.Equals(StatusProduct.Transactioned)
                    select new ListAuctionWinViewModel
                    {
                        Product_Id = product.Products_Id,
                        ProductName = productDetail.ProductName,
                        Transaction_Id = transaction.Transaction_Id,
                        AuctionPrice = (double)db.TransactionAuctions.Where(x => x.Transaction.Transaction_Id == transaction.Transaction_Id).Max(x => x.AuctionPrice),
                    }).ToList();
        }

        public List<ListAuctionLostViewModel> ListAuctionLost(string sessionUserId)
        {
            return (from product in db.Products
                    join productDetail in db.ProductDetails on product.Products_Id equals productDetail.Product_Id
                    join transaction in db.Transactions on product.Products_Id equals transaction.Product_Id
                    join transactionAuction in db.TransactionAuctions on transaction.Transaction_Id equals transactionAuction.Transaction_Id
                    where transactionAuction.Status.Equals(StatusTransactionAuction.Lost)
                          && transactionAuction.User_Id == sessionUserId
                          && product.StatusProduct.Equals(StatusProduct.Transactioning)
                          || transactionAuction.User_Id == sessionUserId
                          && product.StatusProduct.Equals(StatusProduct.Transactioned)
                    select new ListAuctionLostViewModel
                    {
                        Product_Id = product.Products_Id,
                        ProductName = productDetail.ProductName,
                        Transaction_Id = transaction.Transaction_Id,
                        AuctionPrice = (double)db.TransactionAuctions.Where(x => x.Transaction.Transaction_Id == transaction.Transaction_Id).Max(x => x.AuctionPrice),
                    }).ToList();
        }
    }
}