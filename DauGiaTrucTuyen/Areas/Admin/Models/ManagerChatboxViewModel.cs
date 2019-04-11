using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DauGiaTrucTuyen.Areas.Admin.Models
{
    public class ChatboxViewModel
    {
        public string ConnectionId { get; set; }

        public string User_Id { get; set; }

        public string UserName { get; set; }

        public bool IsOnline { get; set; }

        public string LastMsg { get; set; }

        public string DateSend { get; set; }

        public bool IsRead { get; set; }
    }
}