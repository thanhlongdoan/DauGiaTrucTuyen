using DauGiaTrucTuyen.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DauGiaTrucTuyen.IDataBinding
{
    public interface IProduct
    {
        List<ListProductViewModel> GetListProduct(string status);

        List<ListProductViewModel> GetListProductForClient(string sessionUserId);

        bool Create(AddProductViewModel model, HttpPostedFileBase file, string sessionUserId);

        bool CreateForClient(AddProductViewModel model, HttpPostedFileBase file, string sessionUserId);

        bool ApprovedProduct(string product_Id);

        List<ListProductForPageClientViewModel> GetListProductForPageClient();

        DetailProductViewModel DetailProduct(string productId);

        bool CheckPrice(decimal price, string productId);
        

        List<ListProductForPageClientViewModel> GetListProductFromCategory(string categoryId);
    }
}
