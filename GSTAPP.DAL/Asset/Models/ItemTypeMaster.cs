namespace GSTAPP.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ItemTypeMaster")]
    public partial class ItemTypeMaster
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string ItemTypeName { get; set; }

        public int? ItemCategory { get; set; }

        [StringLength(100)]
        public string Company { get; set; }

        public bool? Disabled { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int? Owner { get; set; }
    }
}
