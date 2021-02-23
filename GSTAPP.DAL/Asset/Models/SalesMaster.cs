namespace GSTAPP.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SalesMaster")]
    public partial class SalesMaster
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string InvoiceNo { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? InvoiceDate { get; set; }

        [StringLength(50)]
        public string CompanyCode { get; set; }

        [StringLength(50)]
        public string CustomerCode { get; set; }

        [StringLength(50)]
        public string SO { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? SODate { get; set; }

        [StringLength(50)]
        public string ShippingAddress { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DueDate { get; set; }

        [StringLength(50)]
        public string TermsId { get; set; }

        [StringLength(50)]
        public string Message { get; set; }

        [StringLength(15)]
        public string Discount { get; set; }

        [StringLength(50)]
        public string DiscountAmount { get; set; }

        [StringLength(30)]
        public string TotalAmount { get; set; }

        public bool? IsPercentage { get; set; }

        public bool? Disabled { get; set; }

        public int? CreatedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreatedOn { get; set; }

        public int? ModifiedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ModifiedOn { get; set; }

        public int? Owner { get; set; }
    }
}
