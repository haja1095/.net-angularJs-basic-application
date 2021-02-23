using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GSTAPP.BAL;
namespace GSTAPP.WEB.Controllers
{
    [RoutePrefix("api/Registration")]
    public class RegistrationController : ApiController
    {
        RegistrationService regSer = new RegistrationService();
        [Route("Register"), HttpPost]
        public ReturnModel <RegistrationMaster> Register(RegistrationMaster model)
        {
            return regSer.Register(model);
        }
        
        [Route("GetSingleByEmailorPhoneNo"), HttpPost]
        public ReturnModel<RegistrationMaster> GetSingleByEmailorPhoneNo(RegistrationMaster model)
        {
            return regSer.GetSingleByEmailorPhoneNo(model);
        }
        [Route("EmailVerification"), HttpPost]
        public ReturnModel<RegistrationMaster> EmailVerification(RegistrationMaster model)
        {
            return regSer.EmailVerification(model);
        }

        [Route("PhoneVerification"), HttpPost]
        public ReturnModel<RegistrationMaster> PhoneVerification(RegistrationMaster model)
        {
            return regSer.PhoneVerification(model);
        }

        [Route("EmailandPhoneIsVerifid"), HttpPost]
        public ReturnModel<RegistrationMaster> EmailandPhoneIsVerifid(RegistrationMaster model)
        {
            return regSer.EmailandPhoneIsVerifid(model);
        }

        [Route("ResendData"), HttpPost]
        public ReturnModel<RegistrationMaster> ResendData(RegistrationMaster model)
        {
            return regSer.ResendData(model);
        }
    }
}
