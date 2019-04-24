namespace DauGiaTrucTuyen.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Message_User_Chat
    {
        [Key]
        public string MessageChat_Id { get; set; }

        [StringLength(250)]
        public string FromConnectionId { get; set; }

        [StringLength(128)]
        public string FromUser_Id { get; set; }

        [StringLength(128)]
        public string ToUser_Id { get; set; }

        public DateTime? DateSend { get; set; }

        public bool? IsRead { get; set; }

        public DateTime? DateRead { get; set; }

        public string Message { get; set; }
    }
}
