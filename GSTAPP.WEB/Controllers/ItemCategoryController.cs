using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GSTAPP.BAL;
namespace GSTAPP.WEB.Controllers
{
    [RoutePrefix("api/ItemCategory")]
    public class ItemCategoryController : DefaultController
    {
        ItemCategoryService iteSer = new ItemCategoryService();
        [Route("GetAll"), HttpPost]
        public ReturnModel<ItemCategoryMaster> GetAll()
        {
            return iteSer.GetAll();
            
        }
        [Route("GetSingle"), HttpPost]
        public ReturnModel<ItemCategoryMaster> GetSingle(ItemCategoryMaster model)
        {
            return iteSer.GetSingle(model);
            
        }

        [Route("Delete"), HttpPost]
        public ReturnModel<ItemCategoryMaster> Delete(ItemCategoryMaster model)
        {
            return iteSer.Delete(model);
           
        }

        [Route("Add"), HttpPost]
        public ReturnModel<ItemCategoryMaster> Add(ItemCategoryMaster model)
        {
            return iteSer.Insert(model);
           
        }

        [Route("Update"), HttpPost]
        public ReturnModel<ItemCategoryMaster> Update(ItemCategoryMaster model)
        {
            return iteSer.Update(model);
        }

    }
}
