namespace DauGiaTrucTuyen.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TransactionAuction")]
    public partial class TransactionAuction
    {
        [Key]
        [Column(Order = 0)]
        public string Transaction_Id { get; set; }

        [Key]
        [Column(Order = 1)]
        public string User_Id { get; set; }

        [Key]
        [Column(Order = 2)]
        public DateTime AuctionTime { get; set; }

        public decimal? AuctionPrice { get; set; }

        [StringLength(30)]
        public string Status { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}
