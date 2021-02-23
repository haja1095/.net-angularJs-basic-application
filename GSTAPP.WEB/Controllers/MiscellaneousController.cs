using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GSTAPP.BAL;
namespace GSTAPP.WEB.Controllers
{
    [RoutePrefix("api/Miscellaneous")]
    public class MiscellaneousController : DefaultController
    {
        MiscellaneousService ms = new MiscellaneousService();
        [Route("GetAllUsingMiscellaneousType"), HttpPost]
        public ReturnModel<MiscellaneousMaster> GetAllUsingMiscellaneousType(MiscellaneousMaster model)
        {
            return ms.GetAllUsingMiscellaneousType(model);
        }
        [Route("GetAllState"), HttpPost]
        public ReturnModel<MiscellaneousMaster> GetAllState()
        {
            return ms.GetAllState();
        }
    }
}
