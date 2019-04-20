namespace DauGiaTrucTuyen.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users_Chat
    {
        [Key]
        public string UserChat_Id { get; set; }

        [StringLength(250)]
        public string ConnectionId { get; set; }

        [StringLength(128)]
        public string User_Id { get; set; }

        public bool? IsOnline { get; set; }

        public DateTime? DateOnline { get; set; }
    }
}
