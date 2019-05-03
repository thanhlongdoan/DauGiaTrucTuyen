using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.IDataBinding;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
                        && transactionAuction.Status != null
                    select new ListAuctioningViewModel
                    {
                        Product_Id = product.Products_Id,
                        ProductName = productDetail.ProductName,
                        Transaction_Id = transactionAuction.Transaction_Id,
                        AuctionPrice = (long)db.TransactionAuctions.Where(x => x.Transaction.Transaction_Id == transaction.Transaction_Id).Max(x => x.AuctionPrice),
                    }).Distinct().ToList();
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
                    //&& product.StatusProduct.Equals(StatusProduct.Transactioned)
                    select new ListAuctionWinViewModel
                    {
                        Product_Id = product.Products_Id,
                        ProductName = productDetail.ProductName,
                        Transaction_Id = transaction.Transaction_Id,
                        AuctionPrice = (long)db.TransactionAuctions.Where(x => x.Transaction.Transaction_Id == transaction.Transaction_Id).Max(x => x.AuctionPrice),
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
                    //&& product.StatusProduct.Equals(StatusProduct.Transactioned)
                    select new ListAuctionLostViewModel
                    {
                        Product_Id = product.Products_Id,
                        ProductName = productDetail.ProductName,
                        Transaction_Id = transaction.Transaction_Id,
                        AuctionPrice = (long)db.TransactionAuctions.Where(x => x.Transaction.Transaction_Id == transaction.Transaction_Id).Max(x => x.AuctionPrice),
                    }).Distinct().ToList();
        }

        public bool ConfirmTransaction(string productId)
        {
            var product = db.Products.FirstOrDefault(x => x.Products_Id == productId);
            product.StatusProduct = StatusProduct.Transactioned;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }
    }
}