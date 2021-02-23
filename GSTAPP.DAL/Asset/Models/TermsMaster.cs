namespace GSTAPP.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TermsMaster")]
    public partial class TermsMaster
    {
        [Key]
        [StringLength(7)]
        public string Code { get; set; }

        [StringLength(50)]
        public string TermsName { get; set; }

        public int? NumberOfDays { get; set; }

        [Column(TypeName = "text")]
        public string Remarks { get; set; }

        public bool? Disabled { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int? Owner { get; set; }

        [StringLength(100)]
        public string Company { get; set; }
    }
}
