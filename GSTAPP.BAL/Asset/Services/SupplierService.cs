using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTAPP.BAL
{
    public class SupplierService : Service, IService<SupplierMaster, ReturnModel<SupplierMaster>>
    {

        public ReturnModel<SupplierMaster> Delete(SupplierMaster model)
        {
            var data = uow.SupplierMaster.GetSingle(o => o.Code == model.Code);
            data.Disabled = true;
            uow.SupplierMaster.Delete(data);
            return new ReturnModel<SupplierMaster>()
            {
                code = "0",
                datam = model
            };

        }
        public ReturnModel<SupplierMaster> GetAll()
        {
            return new ReturnModel<SupplierMaster>()
            {
                code = "0",
                data = uow.SupplierMaster.GetAll(o => o.Disabled == false).Select(p => new SupplierMaster()
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
                    MobileNo = p.MobileNo,
                    CompanyName = p.CompanyName,
                    Owner = p.Owner,
                    PhoneNo = p.PhoneNo,
                    State = p.State,
                    PostalCode = p.PostalCode,
                    AccountNo = p.AccountNo,
                    BankName = p.BankName,
                    BranchName = p.BranchName,
                    ContactPerson = p.ContactPerson,
                    IFSCCode = p.IFSCCode,
                    Place =p.Place,
                    WebSite = p.WebSite,
                    WhatsAppNo = p.WhatsAppNo,
                    ModifiedBy = p.ModifiedBy,
                    ModifiedOn =p.ModifiedOn,
                    
                     
                      })
            };
        }

        public ReturnModel<SupplierMaster> GetSingle(SupplierMaster model)
        {
            var data = uow.SupplierMaster.GetSingle(o => o.Code == model.Code);
            var rData = new SupplierMaster()
            {
                Code = data.Code,
                Address = data.Address,
                CreatedBy = data.CreatedBy,
                Disabled = data.Disabled,
                CreatedOn = data.CreatedOn,
                Description = data.Description,
                Email = data.Email,
                Fax = data.Fax,
                GSTIN = data.GSTIN,
                MobileNo = data.MobileNo,
                CompanyName = data.CompanyName,
                Owner = data.Owner,
                PhoneNo = data.PhoneNo,
                State = data.State,
                PostalCode = data.PostalCode,
                BankName = data.BankName,
                AccountNo = data.AccountNo,
                BranchName = data.BranchName,
                ContactPerson =data.ContactPerson,
                IFSCCode =data.IFSCCode,
                Place = data.Place,
                WebSite =data.WebSite,
                WhatsAppNo =data.WhatsAppNo,
                ModifiedBy=data.ModifiedBy,
                ModifiedOn=data.ModifiedOn,
                 

            };
            return new ReturnModel<SupplierMaster>()
            {

                code = "0",
                datam = rData
            };
        }

        public ReturnModel<SupplierMaster> Insert(SupplierMaster model)
        {

            string lastCode = "", newCode = "";
            var lastData = uow.SupplierMaster.GetAll().ToList();
            if (lastData.Count > 0)
            {
                lastCode = lastData.OrderBy(p => p.Code).Last().Code;
                lastCode = lastCode.Replace("SU", "");
                newCode = "SU" + (int.Parse(lastCode) + 1).ToString("00000");
            }
            else
            {
                newCode = "SU00001";
            }
            model.Code = newCode;
            uow.SupplierMaster.Add(new DAL.SupplierMaster()
            {
                CompanyName = model.CompanyName,
                PhoneNo = model.PhoneNo,
                MobileNo = model.MobileNo,
                WhatsAppNo = model.WhatsAppNo,
                WebSite = model.WebSite,
                Fax = model.Fax,
                ContactPerson = model.ContactPerson,
                BankName = model.BankName,
                BranchName = model.BranchName,
                IFSCCode = model.IFSCCode,
                AccountNo = model.AccountNo,
                Email = model.Email,
                Address = model.Address,
                State = model.State,
                PostalCode = model.PostalCode,
                Description = model.Description,
                GSTIN = model.GSTIN,
                CreatedBy = 0,
                Disabled = false,
                CreatedOn = DateTime.Now,
                Owner = model.Owner,
                ModifiedBy = 0,
                ModifiedOn = DateTime.Now,
                Place = model.Place,
                Code = model.Code
                

            });
            return new ReturnModel<SupplierMaster>()
            {

                code = "0",
                datam = model
            };
        }

        public ReturnModel<SupplierMaster> Update(SupplierMaster model)
        {
            var data = uow.SupplierMaster.GetSingle(o => o.Code == model.Code);
            if (data != null)
            {
                data.CompanyName = model.CompanyName;
                data.PhoneNo = model.PhoneNo;
                data.MobileNo = model.MobileNo;
                data.WhatsAppNo = model.WhatsAppNo;
                data.WebSite = model.WebSite;
                data.Fax = model.Fax;
                data.ContactPerson = model.ContactPerson;
                data.BankName = model.BankName;
                data.BranchName = model.BranchName;
                data.IFSCCode = model.IFSCCode;
                data.AccountNo = model.AccountNo;
                data.Email = model.Email;
                data.Address = model.Address;
                data.State = model.State;
                data.PostalCode = model.PostalCode;
                data.Description = model.Description;
                data.GSTIN = model.GSTIN;
                data.ModifiedBy = model.ModifiedBy;
                data.ModifiedOn = DateTime.Now;
                data.Place = model.Place;
                uow.SupplierMaster.Update(data);
            }
            return new ReturnModel<SupplierMaster>()
            {

                code = "0",
                datam = model
            };
        }
    }
}
