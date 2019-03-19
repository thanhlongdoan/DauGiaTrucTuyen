using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static DauGiaTrucTuyen.Areas.Admin.Models.ManagerUserViewModel;

namespace DauGiaTrucTuyen.IDataBinding
{
    public interface IUser
    {
        List<ListUserViewModel> GetListUser();
        DetailUserViewModel DetailUser(string id);
        bool DeleteUser(string id);
    }
}