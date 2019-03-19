using DauGiaTrucTuyen.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DauGiaTrucTuyen.IDataBinding
{
    public interface ICategory
    {
        List<ListCategoryViewModel> GetListCategory();

        bool AddCategory(AddCategoryViewModel model);

        bool EditCategory(EditCategoryViewModel model);

        bool DeleteCategory(string id);

        DetailCategoryViewModel DetailCategory(string id);
    }
}
