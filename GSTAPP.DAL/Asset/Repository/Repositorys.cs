using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTAPP.DAL
{
    public class CompanyMasterRepository : Repository<CompanyMaster>
    {
        public CompanyMasterRepository(GSTModel model) : base(model)
        {

        }
    }
    public class CustomerMasterRepository : Repository<CustomerMaster>
    {
        public CustomerMasterRepository(GSTModel model) : base(model)
        {

        }
    }
    public class SupplierMasterRepository : Repository<SupplierMaster>
    {
        public SupplierMasterRepository(GSTModel model) : base(model)
        {

        }
    }
    public class ProductGroupMasterRepository : Repository<ProductGroupMaster>
    {
        public ProductGroupMasterRepository(GSTModel model) : base(model)
        {

        }
    }
    public class ItemTypeMasterRepository : Repository<ItemTypeMaster>
    {
        public ItemTypeMasterRepository(GSTModel model) : base(model)
        {

        }
    }

    public class ItemCategoryMasterRepository : Repository<ItemCategoryMaster>
    {
        public ItemCategoryMasterRepository(GSTModel model) : base(model)
        {

        }
    }

    public class TermsMasterRepository : Repository<TermsMaster>
    {
        public TermsMasterRepository(GSTModel model) : base(model)
        {


        }
    }
    public class DepartmentMasterRepository : Repository<DepartmentMaster>
    {
        public DepartmentMasterRepository(GSTModel model) : base(model)
        {

        }
    }
    public class ProductMasterRepository : Repository<ProductMaster>
    {
        public ProductMasterRepository(GSTModel model) : base(model)
        {

        }
    }

    public class RegistrationMasterRepository : Repository<RegistrationMaster>
    {
        public RegistrationMasterRepository(GSTModel model) : base(model)
        {

        }
    }

    public class UserRepository : Repository<UserMaster>
    {
        public UserRepository(GSTModel model) : base(model)
        {

        }
    }

    public class PurchaseOrderMasterRepository : Repository<PurchaseOrderMaster>
    {
        public PurchaseOrderMasterRepository(GSTModel model) : base(model)
        {


        }
    }

    public class PurchaseOrderItemRepository : Repository<PurchaseOrderItem>
    {
        public PurchaseOrderItemRepository(GSTModel model) : base(model)
        {


        }
    }

    public class PurchaseInvoiceMasterRepository : Repository<PurchaseMaster>
    {
        public PurchaseInvoiceMasterRepository(GSTModel model) : base(model)
        {

        }
    }

    public class PurchaseInvoiceItemRepository : Repository<PurchaseItem>
    {
        public PurchaseInvoiceItemRepository(GSTModel model) : base(model)
        {

        }
    }

    public class SalesInvoiceMasterRepository : Repository<SalesMaster>
    {
        public SalesInvoiceMasterRepository(GSTModel model) : base(model)
        {

        }
    }

    public class SalesInvoiceItemRepository : Repository<SalesItem>
    {
        public SalesInvoiceItemRepository(GSTModel model) : base(model)
        {

        }
    }

    public class SalesOrderMasterRepository : Repository<SalesOrderMaster>
    {
        public SalesOrderMasterRepository(GSTModel model) : base(model)
        {

        }
    }

    public class SalesOrderItemRepository : Repository<SalesOrderItem>
    {
        public SalesOrderItemRepository(GSTModel model) : base(model)
        {

        }
    }

    public class MiscellaneousRepository : Repository<MiscellaneousMaster>
    {
        public MiscellaneousRepository(GSTModel model) : base(model)
        {

        }
    }
}
