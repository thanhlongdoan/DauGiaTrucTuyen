using DauGiaTrucTuyen.Areas.Admin.Models;
using System.Collections.Generic;

namespace DauGiaTrucTuyen.IDataBinding
{
    public interface IMyAuction
    {
        List<ListAuctioningViewModel> ListAuctioning(string sessionUserId);
        List<ListAuctionWinViewModel> ListAuctionWin(string sessionUserId);
        List<ListAuctionLostViewModel> ListAuctionLost(string sessionUserId);
        bool ConfirmTransaction(string productId);
    }
}
