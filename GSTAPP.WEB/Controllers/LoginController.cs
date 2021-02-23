using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GSTAPP.BAL;
namespace GSTAPP.WEB.Controllers
{
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        UserService usrSre = new UserService();
        [Route("LoginUser"), HttpPost]
        public LoginModel LoginUser(UserMaster model)
        {
            return usrSre.LoginUser(model);
        }
    }
}
