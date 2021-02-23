namespace GSTAPP.DAL
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class GSTModel : DbContext
    {
        public GSTModel()
            : base(System.Configuration.ConfigurationManager.ConnectionStrings["myConn"].ConnectionString)
        {
        }

        public virtual DbSet<CompanyMaster> CompanyMasters { get; set; }
        public virtual DbSet<CustomerMaster> CustomerMasters { get; set; }
        public virtual DbSet<DepartmentMaster> DepartmentMasters { get; set; }
        public virtual DbSet<ItemCategoryMaster> ItemCategoryMasters { get; set; }
        public virtual DbSet<ItemTypeMaster> ItemTypeMasters { get; set; }
        public virtual DbSet<MiscellaneousMaster> MiscellaneousMasters { get; set; }
        public virtual DbSet<ProductGroupMaster> ProductGroupMasters { get; set; }
        public virtual DbSet<ProductMaster> ProductMasters { get; set; }
        public virtual DbSet<PurchaseItem> PurchaseItems { get; set; }
        public virtual DbSet<PurchaseMaster> PurchaseMasters { get; set; }
        public virtual DbSet<PurchaseOrderItem> PurchaseOrderItems { get; set; }
        public virtual DbSet<PurchaseOrderMaster> PurchaseOrderMasters { get; set; }
        public virtual DbSet<RegistrationMaster> RegistrationMasters { get; set; }
        public virtual DbSet<SalesItem> SalesItems { get; set; }
        public virtual DbSet<SalesMaster> SalesMasters { get; set; }
        public virtual DbSet<SalesOrderItem> SalesOrderItems { get; set; }
        public virtual DbSet<SalesOrderMaster> SalesOrderMasters { get; set; }
        public virtual DbSet<SupplierMaster> SupplierMasters { get; set; }
        public virtual DbSet<TermsMaster> TermsMasters { get; set; }
        public virtual DbSet<UserMaster> UserMasters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyMaster>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyMaster>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyMaster>()
                .Property(e => e.PhoneNo)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyMaster>()
                .Property(e => e.MobileNo)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyMaster>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyMaster>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyMaster>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyMaster>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyMaster>()
                .Property(e => e.PostalCode)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyMaster>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyMaster>()
                .Property(e => e.GSTIN)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyMaster>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<CompanyMaster>()
                .Property(e => e.WebSite)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerMaster>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerMaster>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerMaster>()
                .Property(e => e.PhoneNo)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerMaster>()
                .Property(e => e.MobileNo)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerMaster>()
                .Property(e => e.WhatsAppNo)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerMaster>()
                .Property(e => e.WebSite)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerMaster>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerMaster>()
                .Property(e => e.ContactPerson)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerMaster>()
                .Property(e => e.BankName)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerMaster>()
                .Property(e => e.BranchName)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerMaster>()
                .Property(e => e.IFSCCode)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerMaster>()
                .Property(e => e.AccountNo)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerMaster>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerMaster>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerMaster>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerMaster>()
                .Property(e => e.PostalCode)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerMaster>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerMaster>()
                .Property(e => e.GSTIN)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerMaster>()
                .Property(e => e.Place)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerMaster>()
                .Property(e => e.ShippingAddressTitles)
                .IsUnicode(false);

            modelBuilder.Entity<CustomerMaster>()
                .Property(e => e.ShippingAddressValues)
                .IsUnicode(false);

            modelBuilder.Entity<DepartmentMaster>()
                .Property(e => e.DepartmentName)
                .IsUnicode(false);

            modelBuilder.Entity<DepartmentMaster>()
                .Property(e => e.Company)
                .IsUnicode(false);

            modelBuilder.Entity<ItemCategoryMaster>()
                .Property(e => e.ItemCategoryName)
                .IsUnicode(false);

            modelBuilder.Entity<ItemCategoryMaster>()
                .Property(e => e.Company)
                .IsUnicode(false);

            modelBuilder.Entity<ItemTypeMaster>()
                .Property(e => e.ItemTypeName)
                .IsUnicode(false);

            modelBuilder.Entity<ItemTypeMaster>()
                .Property(e => e.Company)
                .IsUnicode(false);

            modelBuilder.Entity<MiscellaneousMaster>()
                .Property(e => e.MiscellaneousType)
                .IsUnicode(false);

            modelBuilder.Entity<MiscellaneousMaster>()
                .Property(e => e.MiscellaneousKey)
                .IsUnicode(false);

            modelBuilder.Entity<MiscellaneousMaster>()
                .Property(e => e.MiscellaneousValue)
                .IsUnicode(false);

            modelBuilder.Entity<ProductGroupMaster>()
                .Property(e => e.ProductGroupName)
                .IsUnicode(false);

            modelBuilder.Entity<ProductGroupMaster>()
                .Property(e => e.Company)
                .IsUnicode(false);

            modelBuilder.Entity<ProductMaster>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<ProductMaster>()
                .Property(e => e.Company)
                .IsUnicode(false);

            modelBuilder.Entity<ProductMaster>()
                .Property(e => e.Rate)
                .HasPrecision(15, 2);

            modelBuilder.Entity<ProductMaster>()
                .Property(e => e.IGST)
                .HasPrecision(5, 2);

            modelBuilder.Entity<ProductMaster>()
                .Property(e => e.CGST)
                .HasPrecision(5, 2);

            modelBuilder.Entity<ProductMaster>()
                .Property(e => e.SGST)
                .HasPrecision(5, 2);

            modelBuilder.Entity<ProductMaster>()
                .Property(e => e.VAT)
                .HasPrecision(5, 2);

            modelBuilder.Entity<ProductMaster>()
                .Property(e => e.HSNCode)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseItem>()
                .Property(e => e.WeightorLength)
                .HasPrecision(15, 2);

            modelBuilder.Entity<PurchaseItem>()
                .Property(e => e.UnitsOrPieces)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseItem>()
                .Property(e => e.Rate)
                .HasPrecision(15, 2);

            modelBuilder.Entity<PurchaseItem>()
                .Property(e => e.Tax)
                .HasPrecision(5, 2);

            modelBuilder.Entity<PurchaseItem>()
                .Property(e => e.TaxAmount)
                .HasPrecision(15, 2);

            modelBuilder.Entity<PurchaseItem>()
                .Property(e => e.Discount)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseMaster>()
                .Property(e => e.InvoiceNo)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseMaster>()
                .Property(e => e.CompanyCode)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseMaster>()
                .Property(e => e.SupplierCode)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseMaster>()
                .Property(e => e.PO)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseMaster>()
                .Property(e => e.ShippingAddress)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseMaster>()
                .Property(e => e.TermsId)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseMaster>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseMaster>()
                .Property(e => e.Discount)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseMaster>()
                .Property(e => e.TotalAmount)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseMaster>()
                .Property(e => e.DiscountAmount)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseMaster>()
                .Property(e => e.ReferenceNumber)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrderItem>()
                .Property(e => e.WeightorLength)
                .HasPrecision(15, 2);

            modelBuilder.Entity<PurchaseOrderItem>()
                .Property(e => e.UnitsOrPieces)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrderItem>()
                .Property(e => e.Rate)
                .HasPrecision(15, 2);

            modelBuilder.Entity<PurchaseOrderItem>()
                .Property(e => e.Tax)
                .HasPrecision(5, 2);

            modelBuilder.Entity<PurchaseOrderItem>()
                .Property(e => e.TaxAmount)
                .HasPrecision(15, 2);

            modelBuilder.Entity<PurchaseOrderItem>()
                .Property(e => e.Discount)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrderMaster>()
                .Property(e => e.CompanyCode)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrderMaster>()
                .Property(e => e.SupplierCode)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrderMaster>()
                .Property(e => e.PO)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrderMaster>()
                .Property(e => e.ShippingAddress)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrderMaster>()
                .Property(e => e.TermsId)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrderMaster>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrderMaster>()
                .Property(e => e.Discount)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrderMaster>()
                .Property(e => e.TotalAmount)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrderMaster>()
                .Property(e => e.DiscountAmount)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrderMaster>()
                .Property(e => e.ReferenceNumber)
                .IsUnicode(false);

            modelBuilder.Entity<RegistrationMaster>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<RegistrationMaster>()
                .Property(e => e.PhoneNo)
                .IsUnicode(false);

            modelBuilder.Entity<RegistrationMaster>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<RegistrationMaster>()
                .Property(e => e.EmailVerificationCode)
                .IsUnicode(false);

            modelBuilder.Entity<RegistrationMaster>()
                .Property(e => e.PhoneNoVerificationCode)
                .IsUnicode(false);

            modelBuilder.Entity<SalesItem>()
                .Property(e => e.WeightorLength)
                .HasPrecision(15, 2);

            modelBuilder.Entity<SalesItem>()
                .Property(e => e.UnitsOrPieces)
                .IsUnicode(false);

            modelBuilder.Entity<SalesItem>()
                .Property(e => e.Rate)
                .HasPrecision(15, 2);

            modelBuilder.Entity<SalesItem>()
                .Property(e => e.Tax)
                .HasPrecision(5, 2);

            modelBuilder.Entity<SalesItem>()
                .Property(e => e.TaxAmount)
                .HasPrecision(15, 2);

            modelBuilder.Entity<SalesItem>()
                .Property(e => e.Discount)
                .IsUnicode(false);

            modelBuilder.Entity<SalesMaster>()
                .Property(e => e.InvoiceNo)
                .IsUnicode(false);

            modelBuilder.Entity<SalesMaster>()
                .Property(e => e.CompanyCode)
                .IsUnicode(false);

            modelBuilder.Entity<SalesMaster>()
                .Property(e => e.CustomerCode)
                .IsUnicode(false);

            modelBuilder.Entity<SalesMaster>()
                .Property(e => e.SO)
                .IsUnicode(false);

            modelBuilder.Entity<SalesMaster>()
                .Property(e => e.ShippingAddress)
                .IsUnicode(false);

            modelBuilder.Entity<SalesMaster>()
                .Property(e => e.TermsId)
                .IsUnicode(false);

            modelBuilder.Entity<SalesMaster>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<SalesMaster>()
                .Property(e => e.Discount)
                .IsUnicode(false);

            modelBuilder.Entity<SalesMaster>()
                .Property(e => e.DiscountAmount)
                .IsUnicode(false);

            modelBuilder.Entity<SalesMaster>()
                .Property(e => e.TotalAmount)
                .IsUnicode(false);

            modelBuilder.Entity<SalesOrderItem>()
                .Property(e => e.WeightorLength)
                .HasPrecision(15, 2);

            modelBuilder.Entity<SalesOrderItem>()
                .Property(e => e.UnitsOrPieces)
                .IsUnicode(false);

            modelBuilder.Entity<SalesOrderItem>()
                .Property(e => e.Rate)
                .HasPrecision(15, 2);

            modelBuilder.Entity<SalesOrderItem>()
                .Property(e => e.Tax)
                .HasPrecision(5, 2);

            modelBuilder.Entity<SalesOrderItem>()
                .Property(e => e.TaxAmount)
                .HasPrecision(15, 2);

            modelBuilder.Entity<SalesOrderItem>()
                .Property(e => e.Discount)
                .IsUnicode(false);

            modelBuilder.Entity<SalesOrderMaster>()
                .Property(e => e.CompanyCode)
                .IsUnicode(false);

            modelBuilder.Entity<SalesOrderMaster>()
                .Property(e => e.CustomerCode)
                .IsUnicode(false);

            modelBuilder.Entity<SalesOrderMaster>()
                .Property(e => e.SO)
                .IsUnicode(false);

            modelBuilder.Entity<SalesOrderMaster>()
                .Property(e => e.ShippingAddress)
                .IsUnicode(false);

            modelBuilder.Entity<SalesOrderMaster>()
                .Property(e => e.TermsId)
                .IsUnicode(false);

            modelBuilder.Entity<SalesOrderMaster>()
                .Property(e => e.Message)
                .IsUnicode(false);

            modelBuilder.Entity<SalesOrderMaster>()
                .Property(e => e.Discount)
                .IsUnicode(false);

            modelBuilder.Entity<SalesOrderMaster>()
                .Property(e => e.TotalAmount)
                .IsUnicode(false);

            modelBuilder.Entity<SalesOrderMaster>()
                .Property(e => e.DiscountAmount)
                .IsUnicode(false);

            modelBuilder.Entity<SupplierMaster>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<SupplierMaster>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<SupplierMaster>()
                .Property(e => e.PhoneNo)
                .IsUnicode(false);

            modelBuilder.Entity<SupplierMaster>()
                .Property(e => e.MobileNo)
                .IsUnicode(false);

            modelBuilder.Entity<SupplierMaster>()
                .Property(e => e.WhatsAppNo)
                .IsUnicode(false);

            modelBuilder.Entity<SupplierMaster>()
                .Property(e => e.WebSite)
                .IsUnicode(false);

            modelBuilder.Entity<SupplierMaster>()
                .Property(e => e.Fax)
                .IsUnicode(false);

            modelBuilder.Entity<SupplierMaster>()
                .Property(e => e.ContactPerson)
                .IsUnicode(false);

            modelBuilder.Entity<SupplierMaster>()
                .Property(e => e.BankName)
                .IsUnicode(false);

            modelBuilder.Entity<SupplierMaster>()
                .Property(e => e.BranchName)
                .IsUnicode(false);

            modelBuilder.Entity<SupplierMaster>()
                .Property(e => e.IFSCCode)
                .IsUnicode(false);

            modelBuilder.Entity<SupplierMaster>()
                .Property(e => e.AccountNo)
                .IsUnicode(false);

            modelBuilder.Entity<SupplierMaster>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<SupplierMaster>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<SupplierMaster>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<SupplierMaster>()
                .Property(e => e.PostalCode)
                .IsUnicode(false);

            modelBuilder.Entity<SupplierMaster>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<SupplierMaster>()
                .Property(e => e.GSTIN)
                .IsUnicode(false);

            modelBuilder.Entity<SupplierMaster>()
                .Property(e => e.Place)
                .IsUnicode(false);

            modelBuilder.Entity<TermsMaster>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<TermsMaster>()
                .Property(e => e.TermsName)
                .IsUnicode(false);

            modelBuilder.Entity<TermsMaster>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<TermsMaster>()
                .Property(e => e.Company)
                .IsUnicode(false);

            modelBuilder.Entity<UserMaster>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<UserMaster>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<UserMaster>()
                .Property(e => e.UserType)
                .IsUnicode(false);

            modelBuilder.Entity<UserMaster>()
                .Property(e => e.Tocken)
                .IsUnicode(false);
        }
    }
}
