using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GSTAPP.BAL;


namespace GSTAPP.WEB.Controllers
{
    [RoutePrefix("api/ProductGroup")]
    public class ProductGroupController : DefaultController
    {

        ProductGroupService proSer = new ProductGroupService();
        [Route("GetSingle"), HttpPost]
        public ReturnModel<ProductGroupMaster> GetSingle(ProductGroupMaster model)
        {
            return proSer.GetSingle(model);
        }
        [Route("GetAll"), HttpPost]
        public ReturnModel<ProductGroupMaster> GetAll()
        {
            return proSer.GetAll();
        }
        [Route("Delete"), HttpPost]
        public ReturnModel<ProductGroupMaster> Delete(ProductGroupMaster model)
        {
            return proSer.Delete(model);
        }
        [Route("Add"), HttpPost]
        public ReturnModel<ProductGroupMaster> Add(ProductGroupMaster model)
        {

            return proSer.Insert(model);

        }
        [Route("Update"), HttpPost]
        public ReturnModel<ProductGroupMaster> Update(ProductGroupMaster model)
        {

            return proSer.Update(model);

        }
    }
}
