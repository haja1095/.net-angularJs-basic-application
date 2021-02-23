using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTAPP.BAL
{
    public class UserService : Service
    {
        public LoginModel LoginUser(UserMaster model)
        {
            var data = uow.UserMaster.GetSingle(o => o.UserName == model.UserName && o.Password == model.Password && o.IsActive == true);
            if (data == null)
            {
                return new LoginModel() { };
            }
            data.Tocken = EnDe.Encrypt(data.UserName + ";" + data.Password, true);
            data.LastLogin = DateTime.Now;
            uow.UserMaster.Update(data);
            return new LoginModel() {
                Tocken = data.Tocken,
                UserName = data.UserName,
                Password = data.Password,
                IsLogin = true
            };
        }

        public UserMaster GetSingle(UserMaster model)
        {
            var data = uow.UserMaster.GetSingle(o => o.IsActive == true && o.Id == model.Id);
            var rData = new UserMaster() {
                CreatedBy = data.CreatedBy,
                CreatedOn = data.CreatedOn,
                Id = data.Id,
                IsActive = data.IsActive,
                LastLogin = data.LastLogin,
                Owner = data.Owner,
                Password = data.Password,
                Tocken = data.Tocken,
                UserName = data.UserName,
                UserType = data.UserType
            };
            return rData;
        }
        public UserMaster GetSingleByUserPass(UserMaster model)
        {
            var data = uow.UserMaster.GetSingle(o => o.IsActive == true && o.UserName == model.UserName && o.Password == model.Password);
            var rData = new UserMaster()
            {
                CreatedBy = data.CreatedBy,
                CreatedOn = data.CreatedOn,
                Id = data.Id,
                IsActive = data.IsActive,
                LastLogin = data.LastLogin,
                Owner = data.Owner,
                Password = data.Password,
                Tocken = data.Tocken,
                UserName = data.UserName,
                UserType = data.UserType
            };
            return rData;
        }
    }
}
