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
        public string TracsactionAuction_Id { get; set; }

        [Required]
        [StringLength(128)]
        public string Transaction_Id { get; set; }

        [Required]
        [StringLength(128)]
        public string User_Id { get; set; }

        public DateTime? AuctionDate { get; set; }

        public decimal? AuctionPrice { get; set; }

        [StringLength(250)]
        public string Connection { get; set; }

        [StringLength(30)]
        public string Status { get; set; }

        public virtual Transaction Transaction { get; set; }
    }
}
