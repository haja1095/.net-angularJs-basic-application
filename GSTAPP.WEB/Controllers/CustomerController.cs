using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GSTAPP.BAL;
namespace GSTAPP.WEB.Controllers
{
    [RoutePrefix("api/Customer")]
    public class CustomerController : DefaultController
    {
        CustomerService cusSer = new CustomerService();
        [Route("GetAll"), HttpPost]
        public ReturnModel<CustomerMaster> GetAll()
        {
            return cusSer.GetAll();
        }
        [Route("GetSingle"), HttpPost]
        public ReturnModel<CustomerMaster> GetSingle(CustomerMaster model)
        {
            return cusSer.GetSingle(model);
        }
        [Route("Delete"), HttpPost]
        public ReturnModel<CustomerMaster> Delete(CustomerMaster model)
        {
            return cusSer.Delete(model);
        }

        [Route("Add"), HttpPost]
        public ReturnModel<CustomerMaster> Add(CustomerMaster model)
        {
            return cusSer.Insert(model);
        }

        [Route("Update"), HttpPost]
        public ReturnModel<CustomerMaster> Update(CustomerMaster model)
        {
            return cusSer.Update(model);
        }

    }
}
    

