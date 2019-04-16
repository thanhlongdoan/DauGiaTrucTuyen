using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Common;
using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.IDataBinding;
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
        /// <param name="status">trạng thái sản phẩm cho người dùng ( trạng thái chưa duyệt và duyệt rồi )</param>
        /// <returns></returns>
        public List<ListProductViewModel> GetListProductForClient(string sessionUserId)
        {
            var list = from product in db.Products
                       join category in db.Categorys on product.Category_Id equals category.Categorys_Id
                       join productDetail in db.ProductDetails on product.Products_Id equals productDetail.Product_Id
                       join transaction in db.Transactions on product.Products_Id equals transaction.Product_Id
                       where product.User_Id == sessionUserId && product.StatusProduct.Equals(StatusProduct.Approved) || product.StatusProduct.Equals(StatusProduct.Review)
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
        public bool Create(AddProductViewModel model, HttpPostedFileBase file, string sessionUserId)
        {
            try
            {
                Upload upload = new Upload();

                Data.Product product = new Data.Product();
                product.Products_Id = Guid.NewGuid().ToString();
                product.CreateDate = DateTime.Now;
                product.CreateBy = "admin";
                product.StatusProduct = StatusProduct.Approved;
                product.Category_Id = model.Category_Id;
                product.User_Id = sessionUserId;
                db.Products.Add(product);

                ProductDetail productDetail = new ProductDetail();
                productDetail.ProductDetails_Id = Guid.NewGuid().ToString();
                productDetail.ProductName = model.ProductName;
                productDetail.Image = upload.UploadImg(file);
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
        public bool CreateForClient(AddProductViewModel model, HttpPostedFileBase file, string sessionUserId)
        {
            try
            {
                Upload upload = new Upload();

                Data.Product product = new Data.Product();
                product.Products_Id = Guid.NewGuid().ToString();
                product.CreateDate = DateTime.Now;
                product.CreateBy = "admin";
                product.StatusProduct = StatusProduct.Review;
                product.Category_Id = model.Category_Id;
                product.User_Id = sessionUserId;
                db.Products.Add(product);

                ProductDetail productDetail = new ProductDetail();
                productDetail.ProductDetails_Id = Guid.NewGuid().ToString();
                productDetail.ProductName = model.ProductName;
                productDetail.Image = upload.UploadImg(file);
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
                product.StatusProduct = StatusProduct.Approved;

                TransactionAuction transactionAuction = new TransactionAuction();
                transactionAuction.Transaction_Id = transaction.Transaction_Id;
                transactionAuction.User_Id = product.User_Id;
                transactionAuction.AuctionTime = DateTime.Now;
                transactionAuction.AuctionPrice = transaction.PriceStart; ;
                db.TransactionAuctions.Add(transactionAuction);

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        //Danh sách danh sách sản phẩm đấu giá cho trang người dùng
        public List<ListProductForPageClientViewModel> GetListProductForPageClient()
        {
            var list = from product in db.Products
                       join category in db.Categorys on product.Category_Id equals category.Categorys_Id
                       join productDetail in db.ProductDetails on product.Products_Id equals productDetail.Product_Id
                       join transaction in db.Transactions on product.Products_Id equals transaction.Product_Id
                       orderby transaction.TimeLine ascending
                       where product.StatusProduct.Equals(StatusProduct.Auctioning)
                       select new ListProductForPageClientViewModel
                       {
                           Products_Id = product.Products_Id,
                           TimeLine = transaction.TimeLine,
                           PriceStart = (decimal)db.TransactionAuctions.Where(x => x.Transaction_Id == transaction.Transaction_Id).Max(x => x.AuctionPrice),
                           Image = productDetail.Image
                       };
            return list.ToList();
        }

        //Danh sách danh sách sản phẩm đấu giá theo danh mục
        public List<ListProductForPageClientViewModel> GetListProductFromCategory(string categoryId)
        {
            var list = from product in db.Products
                       join category in db.Categorys on product.Category_Id equals category.Categorys_Id
                       join productDetail in db.ProductDetails on product.Products_Id equals productDetail.Product_Id
                       join transaction in db.Transactions on product.Products_Id equals transaction.Product_Id
                       orderby transaction.TimeLine ascending
                       where product.StatusProduct.Equals(StatusProduct.Auctioning) && product.Category_Id == categoryId
                       select new ListProductForPageClientViewModel
                       {
                           Products_Id = product.Products_Id,
                           TimeLine = transaction.TimeLine,
                           PriceStart = (decimal)db.TransactionAuctions.Where(x => x.Transaction_Id == transaction.Transaction_Id).Max(x => x.AuctionPrice),
                           Image = productDetail.Image
                       };
            return list.ToList();
        }

        //Chi tiết sản phẩm đấu giá
        public DetailProductViewModel DetailProduct(string productId)
        {
            var transactionGetTimer = db.Transactions.Where(x => x.Product_Id == productId).FirstOrDefault();
            var timerRemaining = Math.Abs(DateTime.Now.Subtract(transactionGetTimer.AuctionDateStart.Value.AddSeconds(transactionGetTimer.TimeLine.Value.TotalSeconds)).TotalSeconds);
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
                             Description = productDetail.Description,
                             TimeRemaining = timerRemaining,
                             TimeLine = transaction.TimeLine,
                             PriceStart = transaction.PriceStart,
                             StepPrice = transaction.StepPrice,
                             CategoryName = category.CategoryName,
                             AuctionPrice = db.TransactionAuctions.Where(x => x.Transaction.Transaction_Id == transaction.Transaction_Id).Max(x => x.AuctionPrice),
                             Transaction_Id = transaction.Transaction_Id
                         };
            return result.FirstOrDefault();
        }

        //Kiểm tra tiền nhập vào đấu giá
        public bool CheckPrice(decimal price, string productId)
        {
            var result = db.TransactionAuctions.Where(x => x.AuctionPrice <= price).FirstOrDefault();
            if (result != null)
                return true;
            return false;
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
            return true;
        }
    }
}