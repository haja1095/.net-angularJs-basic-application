using GSTAPP.BAL;
using GSTAPP.WEB;
using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace GSTAPP.WEB.Controllers
{
    public abstract class DefaultController : ApiController
    {
        HttpRequest request = HttpContext.Current.Request;

        UserClass userClass = new UserClass();
        public string baseUrlData = "";
        public DefaultController()
        {
            try
            {
                string authString = request.Headers.Get("Authorization").ToString();
                baseUrlData = request.PhysicalApplicationPath;
                if (authString != null && authString != "userInfo" && authString.StartsWith("userInfo") && authString.IndexOf('`') >= 0)
                {
                    var baseStrings = EnDe.Decrypt((authString.Substring("userInfo ".Length).Trim()).Split(new char[] { '`' })[0], true, authString.Substring(authString.LastIndexOf('`') + 1, 7));//.Split(new char[] { ';' });
                    var sec = EnDe.Decrypt((authString.Substring("userInfo ".Length).Trim()).Split(new char[] { '`' })[0], true, authString.Substring(authString.LastIndexOf('`') + 1, 7)).Split(new char[] { ';' });
                    var UserName = sec[0];
                    var Password = sec[1];
                    //GSTAPP.BAL.UOW _uow = new GSTAPP.BAL.UOW();
                    //var user = _uow.UserMaster.GetSingle(o => o.IsActive == true && o.UserName == UserName && o.Password == Password);
                    UserService usrSer = new UserService();
                    var user = usrSer.GetSingleByUserPass(new UserMaster() { UserName = UserName, Password = Password });
                    if (user == null)
                    {
                        throw new Exception("401");
                    }
                    else
                    {
                        if (user.Tocken.ToString() == authString.Split(' ')[1].ToString())
                        {
                            userClass.UserName = user.UserName;
                            userClass.UserId = user.Id;
                            if (user.UserType == "owner")
                            {
                                userClass.OwnerName = user.UserName;
                                userClass.OwnerId = user.Id;
                                userClass.UserType = user.UserType;
                            }
                            else
                            {
                                var owner = usrSer.GetSingle(new BAL.UserMaster() { Id = user.Owner.Value });
                                if (owner != null)
                                {
                                    userClass.OwnerId = owner.Id;
                                    userClass.OwnerName = owner.UserName;
                                    userClass.UserType = user.UserType;
                                }
                                else
                                {
                                    throw new Exception("401");
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("401");
                        }
                    }
                }
                else
                {
                    throw new Exception("401");
                }
            }
            catch (Exception e)
            {
                throw new Exception("401",e);
            }
            
        }
    }

}

