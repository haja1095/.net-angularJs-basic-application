using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GSTAPP.BAL;
namespace GSTAPP.WEB.Controllers
{
    [RoutePrefix("api/product")]
    public class productController : DefaultController
    {
        ProductService prodSer = new ProductService();
        [Route("GetSingle"), HttpPost]
        public ReturnModel<ProductMaster> GetSingle(ProductMaster model)
        {
            return prodSer.GetSingle(model);
           
        }
        [Route("GetAll"), HttpPost]
        public ReturnModel<ProductMaster> GetAll()
        {
            return prodSer.GetAll();
           
        }
        [Route("Delete"), HttpPost]
        public ReturnModel<ProductMaster> Delete(ProductMaster model)
        {

            return prodSer.Delete(model);

        }
        [Route("Add"), HttpPost]
        public ReturnModel<ProductMaster> Add(ProductMaster model)
        {

            return prodSer.Insert(model);

        }
        [Route("Update"), HttpPost]
        public ReturnModel<ProductMaster> Update(ProductMaster model)
        {

            return prodSer.Update(model);

        }
        [Route("GetAllUsinCompany"), HttpPost]
        public ReturnModel<ProductMaster> GetAllUsinCompany(CompanyMaster model)
        {
            return prodSer.GetAllUsinCompany(model);

        }
    }
}
