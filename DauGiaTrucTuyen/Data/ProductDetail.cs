namespace DauGiaTrucTuyen.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProductDetail
    {
        [Key]
        public string ProductDetails_Id { get; set; }

        [StringLength(50)]
        public string ProductName { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        [StringLength(128)]
        public string Product_Id { get; set; }

        public virtual Product Product { get; set; }
    }
}
