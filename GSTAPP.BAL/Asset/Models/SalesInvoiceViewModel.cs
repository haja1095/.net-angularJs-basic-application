using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTAPP.BAL
{
    public class SalesInvoiceViewModel : DAL.SalesMaster
    {
        public List<SalesInvoiceItemViewModel> SalesInvoiceItems { get; set; }
        public CompanyMaster Company { get; set; }
        public CustomerMaster Customer { get; set; }
        public TermsMaster Terms { get; set; }
        public string InvoiceDateSTR { get; set; }
        public string SODateSTR { get; set; }
        public string DueDateSTR { get; set; }
        public string URLData { get; set; }
        
    }

    public class SalesInvoiceItemViewModel : DAL.SalesItem
    {
        public ProductMaster Product { get; set; }
        public ItemTypeMaster ItemType { get; set; }
        public string DateSTR { get; set; }

    }
}
