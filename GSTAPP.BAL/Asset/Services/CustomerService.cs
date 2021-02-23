using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTAPP.BAL
{
    public class CustomerService : Service, IService<CustomerMaster, ReturnModel<CustomerMaster>>
    {
        public ReturnModel<CustomerMaster> Delete(CustomerMaster model)
        {
            var data = uow.CustomerMaster.GetSingle(o => o.Code == model.Code);
            data.Disabled = true;
            uow.CustomerMaster.Delete(data);
            return new ReturnModel<CustomerMaster>()
            {
                code = "0",
                datam = model
            };

        }

        public ReturnModel<CustomerMaster> GetAll()
        {
            return new ReturnModel<CustomerMaster>()
            {
                code = "0",
                data = uow.CustomerMaster.GetAll(o => o.Disabled == false).Select(p => new CustomerMaster()
                {
                    Code = p.Code,
                    CompanyName = p.CompanyName,
                    PhoneNo = p.PhoneNo,
                    MobileNo = p.MobileNo,
                    WhatsAppNo = p.WhatsAppNo,
                    WebSite = p.WebSite,
                    Fax = p.Fax,
                    ContactPerson = p.ContactPerson,
                    BankName = p.BankName,
                    BranchName = p.BranchName,
                    IFSCCode = p.IFSCCode,
                    AccountNo = p.AccountNo,
                    Email = p.Email,
                    Disabled = p.Disabled,
                    Address = p.Address,
                    State = p.State,
                    PostalCode = p.PostalCode,
                    Description = p.Description,
                    GSTIN = p.GSTIN,
                    Place = p.Place,
                    ShippingAddressTitles=p.ShippingAddressTitles,
                    ShippingAddressValues=p.ShippingAddressValues,
                    CreatedBy = p.CreatedBy,
                    CreatedOn = p.CreatedOn
                })
            };
        }

        public ReturnModel<CustomerMaster> GetSingle(CustomerMaster model)
        {
            var data = uow.CustomerMaster.GetSingle(o => o.Code == model.Code);
            var rData = new CustomerMaster()
            {
                Code = data.Code,
                CompanyName = data.CompanyName,
                PhoneNo = data.PhoneNo,
                MobileNo = data.MobileNo,
                WhatsAppNo = data.WhatsAppNo,
                WebSite = data.WebSite,
                Fax = data.Fax,
                ContactPerson = data.ContactPerson,
                BankName = data.BankName,
                BranchName = data.BranchName,
                IFSCCode = data.IFSCCode,
                AccountNo = data.AccountNo,
                Email = data.Email,
                Disabled = data.Disabled,
                Address = data.Address,
                State = data.State,
                PostalCode = data.PostalCode,
                Description = data.Description,
                GSTIN = data.GSTIN,
                Place = data.Place,
                ShippingAddressTitles=data.ShippingAddressTitles,
                ShippingAddressValues=data.ShippingAddressValues,
                CreatedBy = data.CreatedBy,
                CreatedOn = data.CreatedOn
            };
            return new ReturnModel<CustomerMaster>()
            {

                code = "0",
                datam = rData
            };
        }

        public ReturnModel<CustomerMaster> Insert(CustomerMaster model)
        {
            string lastCode = "", newCode = "";
            var lastData = uow.CustomerMaster.GetAll().ToList();
            if (lastData.Count > 0)
            {
                lastCode = lastData.OrderBy(p => p.Code).Last().Code;
                lastCode = lastCode.Replace("CU", "");
                newCode = "CU" + (int.Parse(lastCode) + 1).ToString("00000");
            }
            else
            {
                newCode = "CU00001";
            }
            model.Code = newCode;
            uow.CustomerMaster.Add(new DAL.CustomerMaster()
            {
                AccountNo = model.AccountNo,
                Address = model.Address,
                BankName = model.BankName,
                BranchName = model.BranchName,
                Code = model.Code,
                ContactPerson = model.ContactPerson,
                CreatedBy = model.CreatedBy,
                CreatedOn = DateTime.Now,
                Description = model.Description,
                Disabled = false,
                Email = model.Email,
                Fax = model.Fax,
                GSTIN = model.GSTIN,
                IFSCCode = model.IFSCCode,
                MobileNo = model.MobileNo,
                ModifiedBy = model.ModifiedBy,
                ModifiedOn = DateTime.Now,
                CompanyName = model.CompanyName,
                Owner = model.Owner,
                PhoneNo = model.PhoneNo,
                Place = model.Place,
                ShippingAddressTitles=model.ShippingAddressTitles,
                ShippingAddressValues=model.ShippingAddressValues,
                State = model.State,
                PostalCode = model.PostalCode,
                WebSite = model.WebSite,
                WhatsAppNo = model.WhatsAppNo
            });
            return new ReturnModel<CustomerMaster>()
            {

                code = "0",
                datam = model
            };
        }



        public ReturnModel<CustomerMaster> Update(CustomerMaster model)
        {

            var data = uow.CustomerMaster.GetSingle(o => o.Code == model.Code);
            if (data != null)
            {
                data.CompanyName = model.CompanyName;
                data.PhoneNo = model.PhoneNo;
                data.Place = model.Place;
                data.ShippingAddressTitles = model.ShippingAddressTitles;
                data.ShippingAddressValues = model.ShippingAddressValues;
                data.State = model.State;
                data.PostalCode = model.PostalCode;
                data.WebSite = model.WebSite;
                data.WhatsAppNo = model.WhatsAppNo;
                data.AccountNo = model.AccountNo;
                data.Address = model.Address;
                data.BankName = model.BankName;
                data.BranchName = model.BranchName;
                data.ContactPerson = model.ContactPerson;
                data.Description = model.Description;
                data.Email = model.Email;
                data.Fax = model.Fax;
                data.GSTIN = model.GSTIN;
                data.IFSCCode = model.IFSCCode;
                data.MobileNo = model.MobileNo;
                data.ModifiedBy = model.ModifiedBy;
                data.ModifiedOn = DateTime.Now;

                uow.CustomerMaster.Update(data);
            }

            return new ReturnModel<CustomerMaster>()
            {

                code = "0",
                datam = model
            };
        }
    }
}

    

