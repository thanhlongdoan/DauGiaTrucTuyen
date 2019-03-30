namespace DauGiaTrucTuyen.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Transaction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Transaction()
        {
            TransactionAuctions = new HashSet<TransactionAuction>();
        }

        [Key]
        public string Transaction_Id { get; set; }

        public TimeSpan? AuctionTime { get; set; }

        public DateTime? AuctionDate { get; set; }

        public decimal? PriceStart { get; set; }

        public int? StepPrice { get; set; }

        [StringLength(128)]
        public string Product_Id { get; set; }

        public virtual Product Product { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionAuction> TransactionAuctions { get; set; }
    }
}
