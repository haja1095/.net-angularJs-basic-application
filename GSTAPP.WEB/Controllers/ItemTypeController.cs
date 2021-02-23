using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GSTAPP.BAL;


namespace GSTAPP.WEB.Controllers
{
    [RoutePrefix("api/ItemType")]
    public class ItemTypeController : DefaultController
    {

        ItemTypeService iteSer = new ItemTypeService();
        [Route("GetSingle"), HttpPost]
        public ReturnModel<ItemTypeMaster> GetSingle(ItemTypeMaster model)
        {
            return iteSer.GetSingle(model);
        }
        [Route("GetAll"), HttpPost]
        public ReturnModel<ItemTypeMaster> GetAll()
        {
            return iteSer.GetAll();
        }
        [Route("Delete"), HttpPost]
        public ReturnModel<ItemTypeMaster> Delete(ItemTypeMaster model)
        {
            return iteSer.Delete(model);
        }
        [Route("Add"), HttpPost]
        public ReturnModel<ItemTypeMaster> Add(ItemTypeMaster model)
        {

            return iteSer.Insert(model);
        }
        [Route("Update"), HttpPost]
        public ReturnModel<ItemTypeMaster> Update(ItemTypeMaster model)
        {

            return iteSer.Update(model);

        }
    }

}
