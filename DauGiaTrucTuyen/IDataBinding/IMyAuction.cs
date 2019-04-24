using DauGiaTrucTuyen.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DauGiaTrucTuyen.IDataBinding
{
    public interface IMyAuction
    {
        List<ListAuctioningViewModel> ListAuctioning(string sessionUserId);
        List<ListAuctionWinViewModel> ListAuctionWin(string sessionUserId);
        List<ListAuctionLostViewModel> ListAuctionLost(string sessionUserId);
    }
}
