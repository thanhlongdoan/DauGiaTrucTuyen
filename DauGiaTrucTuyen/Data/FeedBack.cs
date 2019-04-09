namespace DauGiaTrucTuyen.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FeedBack
    {
        [Key]
        public string FeedBacks_Id { get; set; }

        public int? Point { get; set; }

        public string Comment { get; set; }

        [StringLength(128)]
        public string User_Id { get; set; }
    }
}
