namespace DauGiaTrucTuyen.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserChat")]
    public partial class UserChat
    {
        [Key]
        public string UserChat_Id { get; set; }

        [StringLength(250)]
        public string ConnectionId { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public bool? IsOnline { get; set; }

        public DateTime? DateOnline { get; set; }
    }
}
