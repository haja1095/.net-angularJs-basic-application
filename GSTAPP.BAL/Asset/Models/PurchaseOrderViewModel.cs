using GSTAPP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTAPP.BAL
{
    public class PurchaseOrderViewModel : DAL.PurchaseOrderMaster
    {
        public List<PurchaseOrderItemViewModel> PurchaseOrderItems { get; set; }
        public CompanyMaster Company { get; set; }
        public SupplierMaster Supplier { get; set; }
        public TermsMaster Terms { get; set; }
        public string PODateSTR { get; set; }
        public string ShippingDateSTR { get; set; }
        public string DueDateSTR { get; set; }
        public string URLData { get; set; }

    }
    public class PurchaseOrderItemViewModel : DAL.PurchaseOrderItem
    {

        public ProductMaster Product { get; set; }
        public ItemTypeMaster ItemType { get; set; }
        public string DateSTR { get; set; }

    }


}
