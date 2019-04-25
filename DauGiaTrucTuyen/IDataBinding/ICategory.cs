using DauGiaTrucTuyen.Areas.Admin.Models;
using System.Collections.Generic;

namespace DauGiaTrucTuyen.IDataBinding
{
    public interface ICategory
    {
        List<ListCategoryViewModel> GetListCategory();

        List<ListCategoryViewModel> GetListCategoryForClient();

        bool AddCategory(AddCategoryViewModel model);

        bool EditCategory(EditCategoryViewModel model);

        bool DeleteCategory(string id);

        DetailCategoryViewModel DetailCategory(string id);
    }
}
