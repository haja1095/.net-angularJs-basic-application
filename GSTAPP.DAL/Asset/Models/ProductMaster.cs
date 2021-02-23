namespace GSTAPP.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductMaster")]
    public partial class ProductMaster
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string ProductName { get; set; }

        [StringLength(100)]
        public string Company { get; set; }

        public int? Department { get; set; }

        public int? ProductGroup { get; set; }

        public decimal? Rate { get; set; }

        public bool? Taxable { get; set; }

        public decimal? IGST { get; set; }

        public decimal? CGST { get; set; }

        public decimal? SGST { get; set; }

        public decimal? VAT { get; set; }

        public bool? Barcode { get; set; }

        public bool? Disabled { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int? Owner { get; set; }

        [StringLength(50)]
        public string HSNCode { get; set; }
    }
}
