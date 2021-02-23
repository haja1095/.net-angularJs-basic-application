using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTAPP.BAL
{
    public class TermsService : Service, IService<TermsMaster, ReturnModel<TermsMaster>>
    {
        public ReturnModel<TermsMaster> Delete(TermsMaster model)
        {
            var data = uow.TermsMaster.GetSingle(o => o.Code == model.Code);
            data.Disabled = true;
            uow.TermsMaster.Delete(data);
            return new ReturnModel<TermsMaster>()
            {
                code = "0",
                datam = model
            };

        }

        public ReturnModel<TermsMaster> GetAll()
        {
            return new ReturnModel<TermsMaster>()
            {
                code = "0",
                data = uow.TermsMaster.GetAll(o => o.Disabled == false).Select(p => new TermsMaster()
                {

                    Code = p.Code,
                    CreatedBy = p.CreatedBy,
                    Disabled = p.Disabled,
                    CreatedOn = p.CreatedOn,
                    TermsName = p.TermsName,
                    NumberOfDays = p.NumberOfDays,
                    Company = p.Company,
                    Remarks = p.Remarks,
                    Owner = p.Owner,
                    ModifiedBy = p.ModifiedBy,
                    ModifiedOn = p.ModifiedOn


                })
            };
        }
        public ReturnModel<TermsMaster> GetSingle(TermsMaster model)
   
            {
                var data = uow.TermsMaster.GetSingle(o => o.Code == model.Code);
                var rData = new TermsMaster()
                {

                    Code = data.Code,
                    CreatedBy = data.CreatedBy,
                    Disabled = data.Disabled,
                    CreatedOn = data.CreatedOn,
                    TermsName = data.TermsName,
                    NumberOfDays = data.NumberOfDays,
                    Remarks = data.Remarks,
                    Company = data.Company,
                    Owner = data.Owner,
                    ModifiedBy = data.ModifiedBy,
                    ModifiedOn = data.ModifiedOn
                   

                };
            return new ReturnModel<TermsMaster>()
            {

                code = "0",
                datam = rData
            };
        }
        public ReturnModel<TermsMaster> Insert(TermsMaster model)
        {
            string lastCode = "", newCode = "";
            var lastData = uow.TermsMaster.GetAll().ToList();
            if (lastData.Count > 0)
            {
                lastCode = lastData.OrderBy(p => p.Code).Last().Code;
                lastCode = lastCode.Replace("TM", "");
                newCode = "TM" + (int.Parse(lastCode) + 1).ToString("00000");
            }
            else
            {
                newCode = "TM00001";

            }
            model.Code = newCode;
            uow.TermsMaster.Add(new DAL.TermsMaster()
            {
                TermsName = model.TermsName,
                NumberOfDays = model.NumberOfDays,
                Remarks = model.Remarks,
                Company = model.Company,
                Disabled = false,
                CreatedOn = DateTime.Now,
                CreatedBy = model.CreatedBy,
                ModifiedOn = DateTime.Now,
                ModifiedBy = model.ModifiedBy,
                Owner = model.Owner,
                Code = model.Code
            });
            return new ReturnModel<TermsMaster>
            {
                code = "0",
                datam = model

        };
            }


        

        public ReturnModel<TermsMaster> Update(TermsMaster model)
        {
            var data = uow.TermsMaster.GetSingle(o => o.Code == model.Code);
            if(data!=null)
            {
                data.TermsName = model.TermsName;
                data.NumberOfDays = model.NumberOfDays;
                data.Remarks = model.Remarks;
                data.Company = model.Company;
                data.ModifiedBy = model.ModifiedBy;
                data.ModifiedOn = DateTime.Now;

                uow.TermsMaster.Update(data);


            }

            return new ReturnModel<TermsMaster>
            {
                code = "0",
                datam = model
            };

        }
    }
}
