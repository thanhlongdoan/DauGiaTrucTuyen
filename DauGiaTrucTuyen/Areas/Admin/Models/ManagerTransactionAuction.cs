using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DauGiaTrucTuyen.Areas.Admin.Models
{
    public class NoticationWin
    {
        public string ProductName { get; set; }

        public string Transaction_Id { get; set; }

        public decimal? PriceAuction { get; set; }

        public string User_Id_Add { get; set; }

        public string User_Id_Auction { get; set; }
    }
}