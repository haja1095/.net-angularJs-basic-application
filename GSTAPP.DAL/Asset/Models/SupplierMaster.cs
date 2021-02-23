namespace GSTAPP.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SupplierMaster")]
    public partial class SupplierMaster
    {
        [Key]
        [StringLength(7)]
        public string Code { get; set; }

        [StringLength(50)]
        public string CompanyName { get; set; }

        [StringLength(15)]
        public string PhoneNo { get; set; }

        [StringLength(15)]
        public string MobileNo { get; set; }

        [StringLength(15)]
        public string WhatsAppNo { get; set; }

        [StringLength(50)]
        public string WebSite { get; set; }

        [StringLength(15)]
        public string Fax { get; set; }

        [StringLength(50)]
        public string ContactPerson { get; set; }

        [StringLength(50)]
        public string BankName { get; set; }

        [StringLength(50)]
        public string BranchName { get; set; }

        [StringLength(50)]
        public string IFSCCode { get; set; }

        [StringLength(50)]
        public string AccountNo { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(60)]
        public string Address { get; set; }

        [StringLength(40)]
        public string State { get; set; }

        [StringLength(20)]
        public string PostalCode { get; set; }

        [StringLength(60)]
        public string Description { get; set; }

        [StringLength(50)]
        public string GSTIN { get; set; }

        public bool? Disabled { get; set; }

        public int? CreatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public int? Owner { get; set; }

        [StringLength(50)]
        public string Place { get; set; }
    }
}
