using GSTAPP.DAL;

namespace GSTAPP.BAL
{
    public class UOW
    {
        private GSTModel model = default(GSTModel);
        private CompanyMasterRepository companyMasterRepository = default(CompanyMasterRepository);
        private CustomerMasterRepository customerMasterRepository = default(CustomerMasterRepository);
        private SupplierMasterRepository supplierMasterRepository = default(SupplierMasterRepository);
        private ProductGroupMasterRepository productgroupMasterRepository = default(ProductGroupMasterRepository);
        private ItemTypeMasterRepository itemtypeMasterRepository = default(ItemTypeMasterRepository);
        private ItemCategoryMasterRepository itemcategoryMasterRepository = default(ItemCategoryMasterRepository);
        private TermsMasterRepository termsMasterRepository = default(TermsMasterRepository);
        private DepartmentMasterRepository departmentMasterRepository = default(DepartmentMasterRepository);
        private ProductMasterRepository productMasterRepository = default(ProductMasterRepository);
        private RegistrationMasterRepository registrationMasterRepository = default(RegistrationMasterRepository);
        private UserRepository userRepository = default(UserRepository);
        private PurchaseOrderMasterRepository purchaseorderMasterRepository = default(PurchaseOrderMasterRepository);
        private PurchaseOrderItemRepository purchaseorderitemRepository = default(PurchaseOrderItemRepository);
        private PurchaseInvoiceMasterRepository purchaseinvoiceMasterRepository = default(PurchaseInvoiceMasterRepository);
        private PurchaseInvoiceItemRepository purchaseinvoiceitemRepository = default(PurchaseInvoiceItemRepository);
        private SalesOrderMasterRepository salesOrderMasterRepository = default(SalesOrderMasterRepository);
        private SalesOrderItemRepository salesOrderItemRepository = default(SalesOrderItemRepository);
        private SalesInvoiceMasterRepository salesInvoiceMasterRepository = default(SalesInvoiceMasterRepository);
        private SalesInvoiceItemRepository salesInvoiceItemRepository = default(SalesInvoiceItemRepository);
        private MiscellaneousRepository miscellaneousRepository = default(MiscellaneousRepository);






        public UOW()
        {
            model = new GSTModel();
        }

        public CompanyMasterRepository CompanyMaster
        {
            get
            {
                if (companyMasterRepository == null)
                    companyMasterRepository = new CompanyMasterRepository(model);
                return companyMasterRepository;
            }
        }

        public CustomerMasterRepository CustomerMaster
        {
            get
            {
                if (customerMasterRepository == null)
                    customerMasterRepository = new CustomerMasterRepository(model);
                return customerMasterRepository;
            }
        }
        public SupplierMasterRepository SupplierMaster
        {
            get
           {
                if (supplierMasterRepository == null)
                   supplierMasterRepository = new SupplierMasterRepository(model);
                return supplierMasterRepository;
            }
        }
        public ProductGroupMasterRepository ProductGroupMaster
        {
            get
            {
                if (productgroupMasterRepository == null)
                    productgroupMasterRepository = new ProductGroupMasterRepository(model);
                return productgroupMasterRepository;
            }
        }
        public ItemTypeMasterRepository ItemTypeMaster
        {
            get
            {
                if (itemtypeMasterRepository == null)
                    itemtypeMasterRepository = new ItemTypeMasterRepository(model);
                return itemtypeMasterRepository;
            }
        }
        public ItemCategoryMasterRepository ItemCategoryMaster
        {
            get
            {
                if (itemcategoryMasterRepository == null)
                    itemcategoryMasterRepository = new ItemCategoryMasterRepository(model);
                return itemcategoryMasterRepository;
            }
        }

        public TermsMasterRepository TermsMaster
        {
            get
            {
                if (termsMasterRepository == null)
                    termsMasterRepository = new TermsMasterRepository(model);
                return termsMasterRepository;
            }
        }
        public DepartmentMasterRepository DepartmentMaster
        {
            get
            {
                if (departmentMasterRepository == null)
                    departmentMasterRepository = new DepartmentMasterRepository(model);
                return departmentMasterRepository;
            }
        }

        public ProductMasterRepository ProductMaster
        {
            get
            {
                if (productMasterRepository == null)
                    productMasterRepository = new ProductMasterRepository(model);
                return productMasterRepository;
            }
        }

        public RegistrationMasterRepository RegistrationMaster
        {
            get
            {
                if (registrationMasterRepository == null)
                    registrationMasterRepository = new RegistrationMasterRepository(model);
                return registrationMasterRepository;
            }
        }

        public UserRepository UserMaster
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(model);
                return userRepository;
            }
        }

        public PurchaseOrderMasterRepository PurchaseOrderMaster
        {
            get
            {
                if (purchaseorderMasterRepository == null)
                    purchaseorderMasterRepository = new PurchaseOrderMasterRepository(model);
                return purchaseorderMasterRepository;
            }
        }

        public PurchaseOrderItemRepository PurchaseOrderItem
        {
            get
            {
                if (purchaseorderitemRepository == null)
                    purchaseorderitemRepository = new PurchaseOrderItemRepository(model);
                return purchaseorderitemRepository;
            }
        }

        public PurchaseInvoiceMasterRepository PurchaseInvoiceMaster
        {
            get
            {
                if (purchaseinvoiceMasterRepository == null)
                    purchaseinvoiceMasterRepository = new PurchaseInvoiceMasterRepository(model);
                return purchaseinvoiceMasterRepository;
            }
        }
        public PurchaseInvoiceItemRepository PurchaseInvoiceItem
        {
            get
            {
                if (purchaseinvoiceitemRepository == null)
                    purchaseinvoiceitemRepository = new PurchaseInvoiceItemRepository(model);
                return purchaseinvoiceitemRepository;
            }
        }

        public SalesInvoiceMasterRepository SalesInvoiceMaster
        {
            get
            {
                if (salesInvoiceMasterRepository == null)
                    salesInvoiceMasterRepository = new SalesInvoiceMasterRepository(model);
                return salesInvoiceMasterRepository;
            }
        }
        public SalesInvoiceItemRepository SalesInvoiceItem
        {
            get
            {
                if (salesInvoiceItemRepository == null)
                    salesInvoiceItemRepository = new SalesInvoiceItemRepository(model);
                return salesInvoiceItemRepository;
            }
        }

        public SalesOrderMasterRepository SalesOrderMaster
        {
            get
            {
                if (salesOrderMasterRepository == null)
                    salesOrderMasterRepository = new SalesOrderMasterRepository(model);
                return salesOrderMasterRepository;
            }
        }

        public SalesOrderItemRepository SalesOrderItem
        {
            get
            {
                if (salesOrderItemRepository == null)
                    salesOrderItemRepository = new SalesOrderItemRepository(model);
                return salesOrderItemRepository;
            }
        }

        public MiscellaneousRepository MiscellaneousMaster
        {
            get
            {
                if (miscellaneousRepository == null)
                    miscellaneousRepository = new MiscellaneousRepository(model);
                return miscellaneousRepository;
            }
        }
    }
}
