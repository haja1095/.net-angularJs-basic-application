using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GSTAPP.BAL;
using GSTAPP.REP;


namespace GSTAPP.WEB.Controllers
{
    [RoutePrefix("api/PurchaseInvoice")]
    public class PurchaseInvoiceController : DefaultController
    {

       PurchaseInvoiceService purSer = new PurchaseInvoiceService();
        PIREP pirep = new PIREP();

        [Route("GetSingle"), HttpPost]
        public ReturnModel<PurchaseInvoiceViewModel> GetSingle(PurchaseInvoiceViewModel model)
        {
            return purSer.GetSingle(model);
            
        }
        [Route("GetAll"), HttpPost]
        public ReturnModel<PurchaseInvoiceViewModel> GetAll()
        {
            return purSer.GetAll();
            
        }
        [Route("Delete"), HttpPost]
        public ReturnModel<PurchaseInvoiceViewModel> Delete(PurchaseInvoiceViewModel model)
        {
            return purSer.Delete(model);
            
        }
        [Route("Add"), HttpPost]
        public ReturnModel<PurchaseInvoiceViewModel> Add(PurchaseInvoiceViewModel model)
        {

            return purSer.Insert(model);

        }
        [Route("Update"), HttpPost]
        public ReturnModel<PurchaseInvoiceViewModel> Update(PurchaseInvoiceViewModel model)
        {

            return purSer.Update(model);

        }
        [Route("GetPIPDF"), HttpPost]
        public ReturnModel<PurchaseInvoiceViewModel> GetPIPDF(PurchaseInvoiceViewModel model)
        {
            return pirep.GetPIPDF(model, baseUrlData);
        }
    }
}
