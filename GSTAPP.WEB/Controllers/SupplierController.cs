using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GSTAPP.BAL;


namespace GSTAPP.WEB.Controllers
{
    [RoutePrefix("api/Supplier")]
    public class SupplierController : DefaultController
    {

        SupplierService supSer = new SupplierService();
        [Route("GetSingle"), HttpPost]
        public ReturnModel<SupplierMaster> GetSingle(SupplierMaster model)
        {
            return supSer.GetSingle(model);
        }
        [Route("GetAll"), HttpPost]
        public ReturnModel<SupplierMaster> GetAll()
        {
            return supSer.GetAll();
            }
            
        
        [Route("Delete"), HttpPost]
        public ReturnModel<SupplierMaster> Delete(SupplierMaster model)
        {

            return supSer.Delete(model);

        }
        [Route("Add"), HttpPost]
        public ReturnModel<SupplierMaster> Add(SupplierMaster model)
        {

            return supSer.Insert(model);
        }
        [Route("Update"), HttpPost]
        public ReturnModel<SupplierMaster> Update(SupplierMaster model)
        {

            return supSer.Update(model);

        }

    }
}
