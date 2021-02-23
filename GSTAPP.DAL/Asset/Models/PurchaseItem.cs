namespace GSTAPP.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PurchaseItem")]
    public partial class PurchaseItem
    {
        public int Id { get; set; }

        public int? PurchaseId { get; set; }

        public int? ProductId { get; set; }

        public decimal? WeightorLength { get; set; }

        [StringLength(50)]
        public string UnitsOrPieces { get; set; }

        public int? ItemTypeId { get; set; }

        public decimal? Rate { get; set; }

        public decimal? Tax { get; set; }

        public decimal? TaxAmount { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? Date { get; set; }

        public bool? Disabled { get; set; }

        public int? CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreatedOn { get; set; }

        public int? ModifiedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ModifiedOn { get; set; }

        public int? Owner { get; set; }

        [StringLength(15)]
        public string Discount { get; set; }

        public bool? IsPercentage { get; set; }
    }
}
