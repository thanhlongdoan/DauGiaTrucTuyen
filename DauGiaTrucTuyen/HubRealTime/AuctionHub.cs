using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.DataBinding;
using Microsoft.AspNet.SignalR;
using System;
using System.Data.Entity;
using System.Linq;
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
            var transactions = db.Transactions.Where(x => x.Product.StatusProduct.Equals(StatusProduct.Auctioning)
                                                        || x.Product.StatusProduct.Equals(StatusProduct.Transactioning))
                                                        .ToList();
            foreach (var item in transactions)
            {
                if (DateTime.Now > (item.AuctionDateStart + item.TimeLine))
                {

                    //lấy ra người có giá cao nhất để update trạng thái win
                    TransactionAuction transactionAuction = db.TransactionAuctions.Where(x => x.Transaction_Id == item.Transaction_Id
                                                                                            && x.Status != null)
                                                                                            .OrderByDescending(x => x.AuctionPrice)
                                                                                            .FirstOrDefault();
                    //update trạng thái win cho người thắng cuộc trong phiên đấu giá
                    if (transactionAuction != null)
                    {
                        transactionAuction.Status = StatusTransactionAuction.Win;
                        db.Entry(transactionAuction).State = EntityState.Modified;
                        db.SaveChanges();

                        //query để lấy thông tin gửi email 
                        var query = (from productSendMail in db.Products
                                     join productDetail in db.ProductDetails on productSendMail.Products_Id equals productDetail.Product_Id
                                     join transactionSendMail in db.Transactions on productSendMail.Products_Id equals transactionSendMail.Product_Id
                                     where transactionSendMail.Transaction_Id == transactionAuction.Transaction_Id &&
                                         productSendMail.StatusProduct.Equals(StatusProduct.Auctioning)
                                     select new NoticationWin
                                     {
                                         Transaction_Id = transactionSendMail.Transaction_Id,
                                         ProductName = productDetail.ProductName,
                                         PriceAuction = transactionAuction.AuctionPrice,
                                         User_Id_Auction = transactionAuction.User_Id,
                                         User_Id_Add = productSendMail.User_Id
                                     })
                                 .FirstOrDefault();
                        if (query != null)
                        {
                            SendNoticationSuccess(query);
                        }
                    }

                    //cập nhật trạng thái khi kết thúc phiên đấu giá
                    Product product = db.Products.FirstOrDefault(x => x.Products_Id == item.Product_Id
                                                                    && x.StatusProduct.Equals(StatusProduct.Auctioning));
                    if (product != null)
                    {
                        product.StatusProduct = StatusProduct.Transactioning;
                        db.Entry(product).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    Clients.Group(item.Transaction_Id).EndTime("End");
                }
            }
        }

        //kiểm tra phiên đấu giá kết thúc ngay tại trang hiển thị danh sách đấu giá của người dùng
        public void CheckEndTime()
        {
            var transactions = db.Transactions.Where(x => x.Product.StatusProduct.Equals(StatusProduct.Auctioning)
                                                        || x.Product.StatusProduct.Equals(StatusProduct.Transactioning))
                                                        .ToList();
            foreach (var item in transactions)
            {
                if (DateTime.Now > (item.AuctionDateStart + item.TimeLine))
                {
                    var product = db.Products.FirstOrDefault(x => x.Products_Id == item.Product_Id);
                    product.StatusProduct = StatusProduct.Transactioning;
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();

                    Clients.All.EndTimeInListView(item.Product_Id);
                }
            }
        }

        //tham gia phiên đấu giá khi load trang chi tiết
        public void JoinGroupAuction(string productId)
        {
            var transaction = db.Transactions.Where(x => x.Product_Id == productId
                                                        && x.Product.StatusProduct.Equals(StatusProduct.Auctioning)
                                                        || x.Product_Id == productId
                                                        && x.Product.StatusProduct.Equals(StatusProduct.Transactioning))
                                                        .FirstOrDefault();

            if (transaction != null)
            {
                Groups.Add(Context.ConnectionId, transaction.Transaction_Id);
            }
        }

        //Đấu giá
        public void JoinAuction(string productId, string userId, decimal? price)
        {
            var transaction = db.Transactions.Where(x => x.Product_Id == productId
                                                        && x.Product.StatusProduct.Equals(StatusProduct.Auctioning))
                                                        .FirstOrDefault();
            var statusUser = db.StatusUsers.FirstOrDefault(x => x.User_Id == userId);

            if (statusUser == null || !statusUser.BlockAuctionStatus.Equals(StatusBlockAuction.Close))
            {
                if (transaction == null)
                {
                    Clients.Caller.AuctionError("Phiên đấu giá đã kết thúc !");
                }
                else
                {
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
                            transactionAuction.Status = StatusTransactionAuction.Lost;
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
            else
            {
                Clients.Caller.AuctionError("Tài khoản của bạn đã bi khóa chức năng này!");
            }
        }

        //gửi email khi phiên đấu giá kết thúc ( gửi cho người thắng cuộc và người chủ sản phẩm)
        public void SendNoticationSuccess(NoticationWin model)
        {
            SendNotication sendNotication = new SendNotication();

            //gửi thông báo cho người chủ sản phẩm
            if (db.Users.FirstOrDefault(x => x.Id == model.User_Id_Add).EmailConfirmed == false)
            {
                //gui qua sdt
                sendNotication.SendSMSNoticationForUserAdd(model);
            }
            else
            {
                sendNotication.SendMailNoticationUserAdd(model);
            }

            //gửi thông báo cho người thắng cuộc
            if (db.Users.FirstOrDefault(x => x.Id == model.User_Id_Auction).EmailConfirmed == false)
            {
                //gui qua sdt
                sendNotication.SendSMSNoticationForUserAuction(model);
            }
            else
            {
                sendNotication.SendMailNoticationForUserAuction(model);
            }
        }
    }
}