using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GSTAPP.BAL;
namespace GSTAPP.WEB.Controllers
{
    [RoutePrefix("api/Terms")]
    public class TermsController : DefaultController
    {
        TermsService terSer = new TermsService();
        [Route("GetAll"), HttpPost]
        public ReturnModel<TermsMaster> GetAll()
        {
            return terSer.GetAll();
           
        }
        [Route("GetSingle"), HttpPost]
        public ReturnModel<TermsMaster> GetSingle(TermsMaster model)
        {
            return terSer.GetSingle(model);
           
        }

        [Route("Add"), HttpPost]
        public ReturnModel<TermsMaster> Add(TermsMaster model)
        {
            return terSer.Insert(model);
            
        }

        [Route("Delete"), HttpPost]
        public ReturnModel<TermsMaster> Delete(TermsMaster model)
        {
            return terSer.Delete(model);
           
        }

        [Route("Update"), HttpPost]
        public ReturnModel<TermsMaster> Update(TermsMaster model)
        {
            return terSer.Update(model);
           
        }

    }

    }

    