using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Common;
using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.IDataBinding;
using DauGiaTrucTuyen.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DauGiaTrucTuyen.DataBinding
{
    public class ProductService : IProduct
    {
        Db_DauGiaTrucTuyen db = new Db_DauGiaTrucTuyen();

        ApplicationDbContext context = new ApplicationDbContext();

        /// <summary>
        /// Trả về danh sách sản phẩm đấu giá
        /// </summary>
        /// <param name="status">trạng thái sản phẩm</param>
        /// <returns></returns>
        public List<ListProductViewModel> GetListProduct(string status)
        {
            var list = from product in db.Products
                       join category in db.Categorys on product.Category_Id equals category.Categorys_Id
                       join productDetail in db.ProductDetails on product.Products_Id equals productDetail.Product_Id
                       join transaction in db.Transactions on product.Products_Id equals transaction.Product_Id
                       where product.StatusProduct.Equals(status)
                       select new ListProductViewModel
                       {
                           Products_Id = product.Products_Id,
                           ProductName = productDetail.ProductName,
                           TimeLine = transaction.TimeLine,
                           PriceStart = (decimal)transaction.PriceStart,
                           Status = product.StatusProduct,
                           CategoryName = productDetail.ProductName
                       };
            return list.ToList();
        }

        /// <summary>
        /// Trả về danh sách sản phẩm đấu giá
        /// </summary>
        /// <param name="status">trạng thái sản phẩm cho người dùng ( trạng thái chưa duyệt và duyệt rồi ) trong quản lý đấu giá của người dùng</param>
        /// <returns></returns>
        public List<ListProductViewModel> GetListProductForClient(string sessionUserId)
        {
            var list = from product in db.Products
                       join category in db.Categorys on product.Category_Id equals category.Categorys_Id
                       join productDetail in db.ProductDetails on product.Products_Id equals productDetail.Product_Id
                       join transaction in db.Transactions on product.Products_Id equals transaction.Product_Id
                       where product.User_Id == sessionUserId && product.StatusProduct.Equals(StatusProduct.Auctioning) || product.User_Id == sessionUserId && product.StatusProduct.Equals(StatusProduct.Review)
                       select new ListProductViewModel
                       {
                           Products_Id = product.Products_Id,
                           ProductName = productDetail.ProductName,
                           TimeLine = transaction.TimeLine,
                           PriceStart = (decimal)transaction.PriceStart,
                           Status = product.StatusProduct,
                           CategoryName = productDetail.ProductName
                       };
            return list.ToList();
        }

        //Tạo mới sản phẩm đấu giá ( dành cho admin )
        public bool Create(AddProductViewModel model, HttpPostedFileBase file, HttpPostedFileBase file1, HttpPostedFileBase file2, string sessionUserId)
        {
            try
            {
                Upload upload = new Upload();

                Data.Product product = new Data.Product();
                product.Products_Id = Guid.NewGuid().ToString();
                product.CreateDate = DateTime.Now;
                product.CreateBy = "admin";
                product.StatusProduct = StatusProduct.Transactioning;
                product.Category_Id = model.Category_Id;
                product.User_Id = sessionUserId;
                db.Products.Add(product);

                ProductDetail productDetail = new ProductDetail();
                productDetail.ProductDetails_Id = Guid.NewGuid().ToString();
                productDetail.ProductName = model.ProductName;
                productDetail.Image = upload.UploadImg(file);
                productDetail.ImageMore1 = upload.UploadImg(file1);
                productDetail.ImageMore2 = upload.UploadImg(file2);
                productDetail.Description = model.Description;
                productDetail.Product_Id = product.Products_Id;
                db.ProductDetails.Add(productDetail);

                Transaction transaction = new Transaction();
                transaction.Transaction_Id = Guid.NewGuid().ToString();
                transaction.TimeLine = TimeSpan.FromTicks(model.TimeLine);
                transaction.AuctionDateApproved = DateTime.Now;
                transaction.PriceStart = model.PriceStart;
                transaction.StepPrice = model.StepPrice;
                transaction.Product_Id = product.Products_Id;
                db.Transactions.Add(transaction);

                TransactionAuction transactionAuction = new TransactionAuction();
                transactionAuction.Transaction_Id = transaction.Transaction_Id;
                transactionAuction.User_Id = sessionUserId;
                transactionAuction.AuctionTime = DateTime.Now;
                transactionAuction.AuctionPrice = transaction.PriceStart;
                db.TransactionAuctions.Add(transactionAuction);

                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Tạo mới sản phẩm đấu giá ( dành cho client )
        public bool CreateForClient(AddProductViewModel model, HttpPostedFileBase file, HttpPostedFileBase file1, HttpPostedFileBase file2, string sessionUserId)
        {
            try
            {
                Upload upload = new Upload();

                Data.Product product = new Data.Product();
                product.Products_Id = Guid.NewGuid().ToString();
                product.CreateDate = DateTime.Now;
                product.CreateBy = sessionUserId;
                product.StatusProduct = StatusProduct.Review;
                product.Category_Id = model.Category_Id;
                product.User_Id = sessionUserId;
                db.Products.Add(product);

                ProductDetail productDetail = new ProductDetail();
                productDetail.ProductDetails_Id = Guid.NewGuid().ToString();
                productDetail.ProductName = model.ProductName;
                productDetail.Image = upload.UploadImg(file);
                productDetail.ImageMore1 = upload.UploadImg(file1);
                productDetail.ImageMore2 = upload.UploadImg(file2);
                productDetail.Description = model.Description;
                productDetail.Product_Id = product.Products_Id;
                db.ProductDetails.Add(productDetail);

                Transaction transaction = new Transaction();
                transaction.Transaction_Id = Guid.NewGuid().ToString();
                transaction.TimeLine = TimeSpan.FromTicks(model.TimeLine);
                transaction.AuctionDateApproved = DateTime.Now;
                transaction.PriceStart = model.PriceStart;
                transaction.StepPrice = model.StepPrice;
                transaction.Product_Id = product.Products_Id;
                db.Transactions.Add(transaction);

                db.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Phê duyệt sản phẩm người dùng đăng ký
        public bool ApprovedProduct(string product_Id)
        {
            var product = db.Products.Where(x => x.Products_Id == product_Id).FirstOrDefault();
            var transaction = db.Transactions.Where(x => x.Product_Id == product.Products_Id).FirstOrDefault();
            if (product != null)
            {
                product.StatusProduct = StatusProduct.Auctioning;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();

                transaction.AuctionDateStart = DateTime.Now;
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();

                TransactionAuction transactionAuction = new TransactionAuction();
                transactionAuction.Transaction_Id = transaction.Transaction_Id;
                transactionAuction.User_Id = product.User_Id;
                transactionAuction.AuctionTime = DateTime.Now;
                transactionAuction.AuctionPrice = transaction.PriceStart; ;
                db.TransactionAuctions.Add(transactionAuction);
                db.SaveChanges();

                return true;
            }
            return false;
        }

        //Danh sách danh sách sản phẩm đấu giá cho trang người dùng (tất cả sản phẩm)
        public List<ListProductFullViewModel> GetListProductForPageClient()
        {
            var query = from product in db.Products
                        join category in db.Categorys on product.Category_Id equals category.Categorys_Id
                        join productDetail in db.ProductDetails on product.Products_Id equals productDetail.Product_Id
                        join transaction in db.Transactions on product.Products_Id equals transaction.Product_Id
                        orderby transaction.TimeLine ascending
                        where product.StatusProduct.Equals(StatusProduct.Auctioning) || product.StatusProduct.Equals(StatusProduct.Transactioning)
                        select new ListProductForPageClientViewModel
                        {
                            Products_Id = product.Products_Id,
                            TimeLine = transaction.TimeLine,
                            AuctionDateStart = transaction.AuctionDateStart,
                            PriceStart = (decimal)db.TransactionAuctions.Where(x => x.Transaction_Id == transaction.Transaction_Id).Max(x => x.AuctionPrice),
                            Image = productDetail.Image
                        };
            var list = query.ToList();
            List<ListProductFullViewModel> listView = new List<ListProductFullViewModel>();
            foreach (var item in list)
            {
                listView.Add(new ListProductFullViewModel
                {
                    Products_Id = item.Products_Id,
                    TimeLine = item.TimeLine,
                    AuctionDateStart = item.AuctionDateStart,
                    PriceStart = item.PriceStart,
                    Image = item.Image,
                    TimeRemaining = item.AuctionDateStart == null ? 0 : (long)Math.Abs(DateTime.Now.Subtract(item.AuctionDateStart.Value.AddSeconds(item.TimeLine.Value.TotalSeconds)).TotalSeconds)
                });
            }
            return listView;
        }

        //Danh sách danh sách sản phẩm đấu giá cho trang người dùng (chỉ sản phẩm đang đấu giá)
        public List<ListProductFullViewModel> GetListProductForPageClientAuctionning()
        {
            var query = from product in db.Products
                        join category in db.Categorys on product.Category_Id equals category.Categorys_Id
                        join productDetail in db.ProductDetails on product.Products_Id equals productDetail.Product_Id
                        join transaction in db.Transactions on product.Products_Id equals transaction.Product_Id
                        orderby transaction.TimeLine ascending
                        where product.StatusProduct.Equals(StatusProduct.Auctioning)
                        select new ListProductForPageClientViewModel
                        {
                            Products_Id = product.Products_Id,
                            TimeLine = transaction.TimeLine,
                            AuctionDateStart = transaction.AuctionDateStart,
                            PriceStart = (decimal)db.TransactionAuctions.Where(x => x.Transaction_Id == transaction.Transaction_Id).Max(x => x.AuctionPrice),
                            Image = productDetail.Image
                        };
            var list = query.ToList();
            List<ListProductFullViewModel> listView = new List<ListProductFullViewModel>();
            foreach (var item in list)
            {
                listView.Add(new ListProductFullViewModel
                {
                    Products_Id = item.Products_Id,
                    TimeLine = item.TimeLine,
                    AuctionDateStart = item.AuctionDateStart,
                    PriceStart = item.PriceStart,
                    Image = item.Image,
                    TimeRemaining = item.AuctionDateStart == null ? 0 : (long)Math.Abs(DateTime.Now.Subtract(item.AuctionDateStart.Value.AddSeconds(item.TimeLine.Value.TotalSeconds)).TotalSeconds)
                });
            }
            return listView;
        }

        //Danh sách danh sách sản phẩm đấu giá theo danh mục
        public List<ListProductFullViewModel> GetListProductFromCategory(string categoryId)
        {
            var query = from product in db.Products
                        join category in db.Categorys on product.Category_Id equals category.Categorys_Id
                        join productDetail in db.ProductDetails on product.Products_Id equals productDetail.Product_Id
                        join transaction in db.Transactions on product.Products_Id equals transaction.Product_Id
                        orderby transaction.TimeLine ascending
                        where product.StatusProduct.Equals(StatusProduct.Auctioning) && product.Category_Id == categoryId
                        || product.StatusProduct.Equals(StatusProduct.Transactioning) && product.Category_Id == categoryId
                        select new ListProductForPageClientViewModel
                        {
                            Products_Id = product.Products_Id,
                            TimeLine = transaction.TimeLine,
                            AuctionDateStart = transaction.AuctionDateStart,
                            PriceStart = (decimal)db.TransactionAuctions.Where(x => x.Transaction_Id == transaction.Transaction_Id).Max(x => x.AuctionPrice),
                            Image = productDetail.Image
                        };
            var list = query.ToList();
            List<ListProductFullViewModel> listView = new List<ListProductFullViewModel>();
            foreach (var item in list)
            {
                listView.Add(new ListProductFullViewModel
                {
                    Products_Id = item.Products_Id,
                    TimeLine = item.TimeLine,
                    AuctionDateStart = item.AuctionDateStart,
                    PriceStart = item.PriceStart,
                    Image = item.Image,
                    TimeRemaining = item.AuctionDateStart == null ? 0 : (long)Math.Abs(DateTime.Now.Subtract(item.AuctionDateStart.Value.AddSeconds(item.TimeLine.Value.TotalSeconds)).TotalSeconds)
                });
            }
            return listView;
        }

        //Chi tiết sản phẩm đấu giá
        public DetailProductViewModel DetailProduct(string productId)
        {
            double timerRemaining = 0;
            var transactionGetTimer = db.Transactions.Where(x => x.Product_Id == productId).FirstOrDefault();
            if (transactionGetTimer.AuctionDateStart == null)
                timerRemaining = 0;
            else
                timerRemaining = Math.Abs(DateTime.Now.Subtract(transactionGetTimer.AuctionDateStart.Value.AddSeconds(transactionGetTimer.TimeLine.Value.TotalSeconds)).TotalSeconds);
            var result = from product in db.Products
                         join category in db.Categorys on product.Category_Id equals category.Categorys_Id
                         join productDetail in db.ProductDetails on product.Products_Id equals productDetail.Product_Id
                         join transaction in db.Transactions on product.Products_Id equals transaction.Product_Id
                         where product.Products_Id == productId
                         select new DetailProductViewModel
                         {
                             Products_Id = product.Products_Id,
                             ProductName = productDetail.ProductName,
                             Image = productDetail.Image,
                             ImageMore1 = productDetail.ImageMore1,
                             ImageMore2 = productDetail.ImageMore2,
                             Description = productDetail.Description,
                             TimeRemaining = (long)timerRemaining,
                             TimeLine = transaction.TimeLine,
                             PriceStart = transaction.PriceStart,
                             StepPrice = transaction.StepPrice,
                             CategoryName = category.CategoryName,
                             AuctionPrice = db.TransactionAuctions.Where(x => x.Transaction.Transaction_Id == transaction.Transaction_Id).Max(x => x.AuctionPrice),
                             Transaction_Id = transaction.Transaction_Id,
                             ListTopAuction = (from transactionAuction in db.TransactionAuctions
                                               join user in db.Users on transactionAuction.User_Id equals user.Id
                                               where transactionAuction.Transaction_Id == transaction.Transaction_Id && transactionAuction.Status != null
                                               orderby transactionAuction.AuctionPrice descending
                                               select new ListTopAuction
                                               {
                                                   UserName = user.UserName,
                                                   //UserName = transactionAuction.User_Id,
                                                   PriceAuction = transactionAuction.AuctionPrice
                                               }).Take(5).ToList()
                         };
            var a = result.FirstOrDefault();
            return result.FirstOrDefault();
        }

        //Cập nhật sản phẩm
        public EditProductViewModel GetViewEditProduct(string productId)
        {
            var result = from product in db.Products
                         join category in db.Categorys on product.Category_Id equals category.Categorys_Id
                         join productDetail in db.ProductDetails on product.Products_Id equals productDetail.Product_Id
                         join transaction in db.Transactions on product.Products_Id equals transaction.Product_Id
                         where product.Products_Id == productId && product.StatusProduct.Equals(StatusProduct.Review)
                         select new EditProductViewModel
                         {
                             Products_Id = product.Products_Id,
                             ProductName = productDetail.ProductName,
                             Image = productDetail.Image,
                             ImageMore1 = productDetail.ImageMore1,
                             ImageMore2 = productDetail.ImageMore2,
                             Description = productDetail.Description,
                             //TimeLine = transaction.TimeLine,
                             PriceStart = transaction.PriceStart,
                             StepPrice = transaction.StepPrice,
                             Category_Id = category.Categorys_Id,
                             User_Id = product.User_Id
                         };
            return result.FirstOrDefault();
        }

        //Cập nhật sản phẩm POST
        public bool Edit(EditProductViewModel model, HttpPostedFileBase file, HttpPostedFileBase file1, HttpPostedFileBase file2)
        {
            Upload upload = new Upload();
            var product = db.Products.Find(model.Products_Id);
            product.UpdateBy = model.User_Id == null ? "" : model.User_Id;
            product.UpdateDate = DateTime.Now;
            product.Category_Id = model.Category_Id;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();

            var productDetail = db.ProductDetails.FirstOrDefault(x => x.Product_Id == model.Products_Id);
            productDetail.ProductName = model.ProductName;
            if (file != null)
            {
                productDetail.Image = upload.UploadImg(file);
            }
            if (file1 != null)
            {
                productDetail.ImageMore1 = upload.UploadImg(file1);
            }
            if (file2 != null)
            {
                productDetail.ImageMore2 = upload.UploadImg(file2);
            }
            productDetail.Description = model.Description;
            db.Entry(productDetail).State = EntityState.Modified;
            db.SaveChanges();

            var transaction = db.Transactions.FirstOrDefault(x => x.Product_Id == model.Products_Id);
            transaction.TimeLine = TimeSpan.FromTicks(model.TimeLine);
            transaction.PriceStart = model.PriceStart;
            transaction.StepPrice = model.StepPrice;
            db.Entry(transaction).State = EntityState.Modified;
            db.SaveChanges();

            return true;
        }

        //xóa sản phẩm
        public bool Delele(string productId)
        {
            var product = db.Products.Find(productId);
            var productDetail = db.ProductDetails.FirstOrDefault(x => x.Product_Id == productId);
            var transaction = db.Transactions.FirstOrDefault(x => x.Product_Id == productId);
            var transactionAuction = db.TransactionAuctions.Where(x => x.Transaction_Id == transaction.Transaction_Id).ToList();
            if (transactionAuction.Count() > 0)
            {
                foreach (var item in transactionAuction)
                {
                    db.TransactionAuctions.Remove(item);
                }
                db.SaveChanges();
            }
            db.Transactions.Remove(transaction);
            db.SaveChanges();
            db.ProductDetails.Remove(productDetail);
            db.SaveChanges();
            db.Products.Remove(product);
            db.SaveChanges();
            return true;
        }

        //hoàn tác đã duyệt thành đang chờ duyệt
        public bool UnApproved(string productId)
        {
            var transaction = db.Transactions.FirstOrDefault(x => x.Product_Id == productId);
            var transactionAuction = db.TransactionAuctions.Where(x => x.Transaction_Id == transaction.Transaction_Id).ToList();
            if (transactionAuction.Count() > 0)
            {
                foreach (var item in transactionAuction)
                {
                    db.TransactionAuctions.Remove(item);
                }
                db.SaveChanges();
            }
            var product = db.Products.FirstOrDefault(x => x.Products_Id == productId);
            product.StatusProduct = StatusProduct.Review;
            db.Entry(product).State = EntityState.Modified;
            db.SaveChanges();

            transaction.AuctionDateStart = null;
            db.Entry(transaction).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }


        //kết thúc phiên đấu giá
        public bool EndAuction(string productId)
        {
            var product = db.Products.FirstOrDefault(x => x.Products_Id == productId);
            if (product != null)
            {
                product.StatusProduct = StatusProduct.Transactioning;
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                var transactions = db.Transactions.Where(x => x.Product.StatusProduct.Equals(StatusProduct.Auctioning)
                                                        || x.Product.StatusProduct.Equals(StatusProduct.Transactioning))
                                                        .ToList();
                foreach (var item in transactions)
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
                }
                return true;
            }
            return false;
        }

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