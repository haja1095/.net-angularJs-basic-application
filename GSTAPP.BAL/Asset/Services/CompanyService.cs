using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTAPP.BAL
{
    public class CompanyService : Service, IService<CompanyMaster,ReturnModel<CompanyMaster>>
    {
        public ReturnModel<CompanyMaster> Delete(CompanyMaster model)
        {
            var data = uow.CompanyMaster.GetSingle(o => o.Code == model.Code);
            data.Disabled = true;
            uow.CompanyMaster.Delete(data);
            return new ReturnModel<CompanyMaster>()
            {
                code = "0",
                datam = model
            };

        }

        public ReturnModel<CompanyMaster> GetAll()
        {
            return new ReturnModel<CompanyMaster>()
            {
                code = "0",
                data = uow.CompanyMaster.GetAll(o => o.Disabled == false).Select(p => new CompanyMaster()
                {
                    Code = p.Code,
                    Address = p.Address,
                    CreatedBy = p.CreatedBy,
                    Disabled = p.Disabled,
                    CreatedOn = p.CreatedOn,
                    Description = p.Description,
                    Email = p.Email,
                    Fax = p.Fax,
                    GSTIN = p.GSTIN,
                    Image = p.Image,
                    MobileNo = p.MobileNo,
                    CompanyName = p.CompanyName,
                    Owner = p.Owner,
                    PhoneNo = p.PhoneNo,
                    State = p.State,
                    PostalCode = p.PostalCode,
                    WebSite = p.WebSite
                })
            };
        }

        public ReturnModel<CompanyMaster> GetSingle(CompanyMaster model)
        {
            var a = uow.CompanyMaster.GetSingle(o => o.Disabled == false && o.Code == model.Code);
            CompanyMaster c = new CompanyMaster()
            {
                Code = a.Code,
                Address = a.Address,
                CreatedBy = a.CreatedBy,
                Disabled = a.Disabled,
                CreatedOn = a.CreatedOn,
                Description = a.Description,
                Email = a.Email,
                Fax = a.Fax,
                GSTIN = a.GSTIN,
                Image = a.Image,
                MobileNo = a.MobileNo,
                CompanyName = a.CompanyName,
                Owner = a.Owner,
                PhoneNo = a.PhoneNo,
                State = a.State,
                PostalCode = a.PostalCode,
                WebSite = a.WebSite
            };
            return new ReturnModel<CompanyMaster>()
            {

                code = "0",
                datam = c
            };
        }

        public ReturnModel<CompanyMaster> Insert(CompanyMaster model)
        {
            string lastCode = "", newCode = "";
            var lastData = uow.CompanyMaster.GetAll().ToList();
            if (lastData.Count > 0)
            {
                lastCode = lastData.OrderBy(p => p.Code).Last().Code;
                lastCode = lastCode.Replace("CO", "");
                newCode = "CO" + (int.Parse(lastCode) + 1).ToString("00000");
            }
            else
            {
                newCode = "CO00001";
            }
            model.Code = newCode;
            uow.CompanyMaster.Add(new DAL.CompanyMaster()
            {
                Address = model.Address,
                Code = model.Code,
                CreatedBy = 0,
                CreatedOn = DateTime.Now,
                Description = model.Description,
                Disabled = false,
                Email = model.Email,
                Fax = model.Fax,
                GSTIN = model.GSTIN,
                Image = model.Image,
                MobileNo = model.MobileNo,
                CompanyName = model.CompanyName,
                Owner = model.Owner,
                PhoneNo = model.PhoneNo,
                State = model.State,
                PostalCode = model.PostalCode,
                WebSite = model.WebSite,
                
            });
            return new ReturnModel<CompanyMaster>()
            {

                code = "0",
                datam = model
            };
        }
        public ReturnModel<CompanyMaster> Update(CompanyMaster model)
        {
            var data = uow.CompanyMaster.GetSingle(o => o.Code == model.Code);
            if (data != null)
            {
                data.Address = model.Address;
                data.Code = model.Code;
                data.Description = model.Description;
                data.Email = model.Email;
                data.Fax = model.Fax;
                data.GSTIN = model.GSTIN;
                data.Image = model.Image;
                data.MobileNo = model.MobileNo;
                data.CompanyName = model.CompanyName;
                data.Owner = model.Owner;
                data.PhoneNo = model.PhoneNo;
                data.State = model.State;
                data.PostalCode = model.PostalCode;
                data.WebSite = model.WebSite;

                uow.CompanyMaster.Update(data);


            }
            return new ReturnModel<CompanyMaster>()
            {

                code = "0",
                datam = model
            };
        }
    }
}
