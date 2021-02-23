using GSTAPP.BAL;
using GSTAPP.REP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GSTAPP.WEB.Controllers
{
    [RoutePrefix("api/SalesOrder")]
    public class SalesOrderController : DefaultController
    {
        SalesOrderService salSer = new SalesOrderService();
        SOREP sorep = new SOREP();


        [Route("GetSingle"), HttpPost]
        public ReturnModel<SalesOrderViewModel> GetSingle(SalesOrderViewModel model)
        {
            return salSer.GetSingle(model);

        }
        [Route("GetAll"), HttpPost]
        public ReturnModel<SalesOrderViewModel> GetAll()
        {
            return salSer.GetAll();

        }
        [Route("Delete"), HttpPost]
        public ReturnModel<SalesOrderViewModel> Delete(SalesOrderViewModel model)
        {
            return salSer.Delete(model);

        }
        [Route("Add"), HttpPost]
        public ReturnModel<SalesOrderViewModel> Add(SalesOrderViewModel model)
        {
            return salSer.Insert(model);
        }
        [Route("Update"), HttpPost]
        public ReturnModel<SalesOrderViewModel> Update(SalesOrderViewModel model)
        {
            return salSer.Update(model);
        }
        [Route("GetAllfromCOMCUS"), HttpPost]
        public ReturnModel<SalesOrderViewModel> GetAllfromCOMCUS(SalesOrderViewModel model)
        {
            return salSer.GetAllfromCOMCUS(model);
        }
        [Route("GetSOPDF"), HttpPost]
        public ReturnModel<SalesOrderViewModel> GetPOPDF(SalesOrderViewModel model)
        {
            return sorep.GetSOPDF(model, baseUrlData);
        }
    }
}
