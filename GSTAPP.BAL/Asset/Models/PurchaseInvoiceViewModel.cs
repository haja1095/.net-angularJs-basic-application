using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTAPP.BAL
{
    public class PurchaseInvoiceViewModel : DAL.PurchaseMaster
    {
        public List<PurchaseInvoiceItemViewModel> PurchaseInvoiceItems { get; set; }
        public CompanyMaster Company { get; set; }
        public SupplierMaster Supplier { get; set; }
        public TermsMaster Terms { get; set; }
        public string InvoiceDateSTR { get; set; }
        public string PODateSTR { get; set; }
        public string DueDateSTR { get; set; }
        public string URLData { get; set; }
    }

    public class PurchaseInvoiceItemViewModel : DAL.PurchaseItem
    {
        public ProductMaster Product { get; set; }
        public ItemTypeMaster ItemType { get; set; }
        public string DateSTR { get; set; }
    }
}
