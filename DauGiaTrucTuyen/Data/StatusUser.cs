namespace DauGiaTrucTuyen.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class StatusUser
    {
        [Key]
        public string StatusUsers_Id { get; set; }

        public bool? BlockAuctionStatus { get; set; }

        public DateTime? BlockAuctionTimes { get; set; }

        public DateTime? BlockAuctionDate { get; set; }

        public bool? BlockUserStatus { get; set; }

        public DateTime? BlockUserTime { get; set; }

        [StringLength(128)]
        public string User_Id { get; set; }

        public virtual User User { get; set; }
    }
}
