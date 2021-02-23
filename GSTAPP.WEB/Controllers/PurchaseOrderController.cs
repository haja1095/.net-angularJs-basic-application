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
    [RoutePrefix("api/PurchaseOrder")]
    public class PurchaseOrderController : DefaultController
    {
        PurchaseOrderService purSer = new PurchaseOrderService();
        POREP porep = new POREP();
        [Route("GetAll"), HttpPost]
        public ReturnModel<PurchaseOrderViewModel> GetAll()
        {
            return purSer.GetAll();
        }
        [Route("GetSingle"), HttpPost]
        public ReturnModel<PurchaseOrderViewModel> GetSingle(PurchaseOrderViewModel model)
        {
            return purSer.GetSingle(model);
        }
        [Route("Delete"), HttpPost]
        public ReturnModel<PurchaseOrderViewModel> Delete(PurchaseOrderViewModel model)
        {
            return purSer.Delete(model);
        }

        [Route("Add"), HttpPost]
        public ReturnModel<PurchaseOrderViewModel> Add(PurchaseOrderViewModel model)
        {

            return purSer.Insert(model);

        }

        [Route("Update"), HttpPost]
        public ReturnModel<PurchaseOrderViewModel> Update(PurchaseOrderViewModel model)
        {

            return purSer.Update(model);

        }
        [Route("GetAllfromCOMSUP"), HttpPost]
        public ReturnModel<PurchaseOrderViewModel> GetAllfromCOMSUP(PurchaseOrderViewModel model)
        {
            return purSer.GetAllfromCOMSUP(model);
        }
        [Route("GetPOPDF"), HttpPost]
        public ReturnModel<PurchaseOrderViewModel> GetPOPDF(PurchaseOrderViewModel model)
        {
            return porep.GetPOPDF(model,baseUrlData);
        }
    }
}
