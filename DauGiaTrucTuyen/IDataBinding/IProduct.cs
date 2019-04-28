using DauGiaTrucTuyen.Areas.Admin.Models;
using System.Collections.Generic;
using System.Web;

namespace DauGiaTrucTuyen.IDataBinding
{
    public interface IProduct
    {
        List<ListProductViewModel> GetListProduct(string status);

        List<ListProductViewModel> GetListProductForClient(string sessionUserId);

        bool Create(AddProductViewModel model, HttpPostedFileBase file, HttpPostedFileBase file1, HttpPostedFileBase file2, string sessionUserId);

        bool CreateForClient(AddProductViewModel model, HttpPostedFileBase file, HttpPostedFileBase file1, HttpPostedFileBase file2, string sessionUserId);

        bool ApprovedProduct(string product_Id);

        List<ListProductFullViewModel> GetListProductForPageClient();

        List<ListProductFullViewModel> GetListProductForPageClientAuctionning();

        DetailProductViewModel DetailProduct(string productId);

        List<ListProductFullViewModel> GetListProductFromCategory(string categoryId);

        bool Delele(string productId);

        bool UnApproved(string productId);

        EditProductViewModel GetViewEditProduct(string productId);

        bool Edit(EditProductViewModel model, HttpPostedFileBase file, HttpPostedFileBase file1, HttpPostedFileBase file2);

        bool EndAuction(string productId);

        void SendNoticationSuccess(NoticationWin model);

        List<ListProductViewModel> GetFullListProduct();
    }
}
