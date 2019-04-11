namespace DauGiaTrucTuyen.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MessageChat")]
    public partial class MessageChat
    {
        [Key]
        public string MessageChat_Id { get; set; }

        [StringLength(250)]
        public string FromConnectionId { get; set; }

        [StringLength(50)]
        public string FromUser_Id { get; set; }

        [StringLength(50)]
        public string ToUser_Id { get; set; }

        public string Msg { get; set; }

        public DateTime? DateSend { get; set; }

        public bool? IsRead { get; set; }

        public DateTime? DateRead { get; set; }
    }
}
