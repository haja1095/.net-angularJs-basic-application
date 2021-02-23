using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GSTAPP.BAL;
namespace GSTAPP.WEB.Controllers
{
    [RoutePrefix("api/Department")]
    public class DepartmentController : DefaultController
    {
        DepartmentService depSer = new DepartmentService();
        [Route("GetSingle"), HttpPost]
        public ReturnModel<DepartmentMaster> GetSingle(DepartmentMaster model)
        {
            return depSer.GetSingle(model);
        }
        [Route("GetAll"), HttpPost]
        public ReturnModel<DepartmentMaster> GetAll()
        {
            return depSer.GetAll();
        }
        [Route("Delete"), HttpPost]
        public ReturnModel<DepartmentMaster> Delete(DepartmentMaster model)
        {

            return depSer.Delete(model);

        }
        [Route("Add"), HttpPost]
        public ReturnModel<DepartmentMaster> Add(DepartmentMaster model)
        {

            return depSer.Insert(model);

        }

        [Route("Update"), HttpPost]
        public ReturnModel<DepartmentMaster> Update(DepartmentMaster model)
        {

            return depSer.Update(model);

        }


    }
}
