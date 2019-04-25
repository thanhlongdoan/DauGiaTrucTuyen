using System.Collections.Generic;
using static DauGiaTrucTuyen.Areas.Admin.Models.ManagerUserViewModel;

namespace DauGiaTrucTuyen.IDataBinding
{
    public interface IUser
    {
        List<ListUserViewModel> GetListUser();

        DetailUserViewModel DetailUser(string id);

        bool DeleteUser(string id);

        bool HandleUser(HandleUserViewModel model);
    }
}