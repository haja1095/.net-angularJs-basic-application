namespace GSTAPP.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MiscellaneousMaster")]
    public partial class MiscellaneousMaster
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string MiscellaneousType { get; set; }

        [StringLength(50)]
        public string MiscellaneousKey { get; set; }

        [StringLength(50)]
        public string MiscellaneousValue { get; set; }

        public bool? Disabled { get; set; }
    }
}
