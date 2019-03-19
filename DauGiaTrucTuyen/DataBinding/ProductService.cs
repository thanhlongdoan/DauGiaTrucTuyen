using DauGiaTrucTuyen.Areas.Admin.Models;
using DauGiaTrucTuyen.Common;
using DauGiaTrucTuyen.Data;
using DauGiaTrucTuyen.IDataBinding;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
        public List<ListProductViewModel> GetListProduct(int status)
        {
            var list = from product in db.Products
                       join category in db.Categorys on product.Category_Id equals category.Categorys_Id
                       join productDetail in db.ProductDetails on product.Products_Id equals productDetail.Product_Id
                       join transaction in db.Transactions on product.Products_Id equals transaction.Product_Id
                       where transaction.Status == status
                       select new ListProductViewModel
                       {
                           Products_Id = product.Products_Id,
                           ProductName = productDetail.ProductName,
                           AuctionTime = transaction.AuctionTime,
                           PriceStart = (decimal)transaction.PriceStart,
                           Status = (int)transaction.Status,
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
                product.UpdateDate = null;
                product.UpdateBy = null;
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
                transaction.AuctionTime = "";
                transaction.AuctionDate = DateTime.Now;
                transaction.PriceStart = model.PriceStart;
                transaction.StepPrice = model.StepPrice;
                transaction.Status = 2;
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
            var product = db.Transactions.Where(x => x.Product_Id == product_Id).FirstOrDefault();
            if (product != null)
            {
                product.Status = 2;
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
                       where transaction.Status == 2
                       select new ListProductForPageClientViewModel
                       {
                           Products_Id = product.Products_Id,
                           AuctionTime = transaction.AuctionTime,
                           PriceStart = (decimal)transaction.PriceStart,
                           Image = productDetail.Image
                       };
            return list.ToList();
        }

        //Chi tiết sản phẩm đấu giá
        public DetailProductViewModel DetailProduct(string productId)
        {
            var result = from product in db.Products
                         join category in db.Categorys on product.Category_Id equals category.Categorys_Id
                         join productDetail in db.ProductDetails on product.Products_Id equals productDetail.Product_Id
                         join transaction in db.Transactions on product.Products_Id equals transaction.Product_Id
                         where transaction.Status == 2 && product.Products_Id == productId
                         select new DetailProductViewModel
                         {
                             Products_Id = product.Products_Id,
                             ProductName = productDetail.ProductName,
                             Image = productDetail.Image,
                             Description = productDetail.Description,
                             AuctionTime = transaction.AuctionTime,
                             PriceStart = transaction.PriceStart,
                             StepPrice = transaction.StepPrice,
                             Category_Id = category.CategoryName
                         };
            return result.FirstOrDefault();
        }
    }
}