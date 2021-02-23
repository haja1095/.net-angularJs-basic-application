
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
    [RoutePrefix("api/SalesInvoice")]
    public class SalesInvoiceController : DefaultController
    {
        SalesInvoiceService salSer = new SalesInvoiceService();
        SIREP sirep = new SIREP();

        [Route("GetSingle"), HttpPost]
        public ReturnModel<SalesInvoiceViewModel> GetSingle(SalesInvoiceViewModel model)
        {
            return salSer.GetSingle(model);

        }
        [Route("GetAll"), HttpPost]
        public ReturnModel<SalesInvoiceViewModel> GetAll()
        {
            return salSer.GetAll();

        }
        [Route("Delete"), HttpPost]
        public ReturnModel<SalesInvoiceViewModel> Delete(SalesInvoiceViewModel model)
        {
            return salSer.Delete(model);

        }
        [Route("Add"), HttpPost]
        public ReturnModel<SalesInvoiceViewModel> Add(SalesInvoiceViewModel model)
        {
            return salSer.Insert(model);
        }
        [Route("Update"), HttpPost]
        public ReturnModel<SalesInvoiceViewModel> Update(SalesInvoiceViewModel model)
        {
            return salSer.Update(model);
        }

        [Route("GetSIPDF"), HttpPost]
        public ReturnModel<SalesInvoiceViewModel> GetSIPDF(SalesInvoiceViewModel model)
        {
            return sirep.GetSIPDF(model, baseUrlData);
        }
    }
}
