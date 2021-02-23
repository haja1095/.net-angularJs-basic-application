using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GSTAPP.BAL;
namespace GSTAPP.WEB.Controllers
{
    [RoutePrefix("api/Company")]
    public class CompanyController : DefaultController
    {
        CompanyService comSer = new CompanyService();
        [Route("GetSingle"), HttpPost]
        public ReturnModel<CompanyMaster> GetSingle(CompanyMaster model)
        {
            return comSer.GetSingle(model);
        }
        [Route("GetAll"), HttpPost]
        public ReturnModel<CompanyMaster> GetAll()
        {
            return comSer.GetAll();
        }
        [Route("Delete"), HttpPost]
        public ReturnModel<CompanyMaster> Delete(CompanyMaster model)
        {

            return comSer.Delete(model);

        }
        [Route("ProfileImagePost"), HttpPost]
        public string ProfileImagePost()
        {
            var request = System.Web.HttpContext.Current.Request;
            string[] extensions = { ".jpg", ".jpeg", ".gif", ".bmp", ".png" };
            System.IO.Stream st = request.Files[0].InputStream;
            System.IO.BinaryReader br = new System.IO.BinaryReader(st);
            Byte[] bytes = br.ReadBytes((Int32)st.Length);
            string base64Str = @"data:image/png; base64," + Convert.ToBase64String(bytes);
            return base64Str;
        }
        [Route("Add"), HttpPost]
        public ReturnModel<CompanyMaster> Add(CompanyMaster model)
        {

            return comSer.Insert(model);
        }
        [Route("Update"), HttpPost]
        public ReturnModel<CompanyMaster> Update(CompanyMaster model)
        {

            return  comSer.Update(model);

        }

    }
}
