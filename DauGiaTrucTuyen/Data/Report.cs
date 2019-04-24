namespace DauGiaTrucTuyen.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Report
    {
        [Key]
        public string Reports_Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        [StringLength(50)]
        public string ReportUser { get; set; }

        [StringLength(128)]
        public string Transaction_Id { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        [StringLength(30)]
        public string Status { get; set; }

        [StringLength(128)]
        public string User_Id { get; set; }

        public virtual User User { get; set; }
    }
}
