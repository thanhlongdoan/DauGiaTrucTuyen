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

        [StringLength(10)]
        public string BlockAuctionStatus { get; set; }

        public DateTime? BlockAuctionDate { get; set; }

        [StringLength(10)]
        public string BlockUserStatus { get; set; }

        public DateTime? BlockUserDate { get; set; }

        [StringLength(128)]
        public string User_Id { get; set; }
    }
}
