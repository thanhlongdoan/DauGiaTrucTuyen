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

        public DateTime? AuctionDate { get; set; }

        public decimal? AuctionPrice { get; set; }

        public virtual Transaction Transaction { get; set; }

        public virtual User User { get; set; }
    }
}
