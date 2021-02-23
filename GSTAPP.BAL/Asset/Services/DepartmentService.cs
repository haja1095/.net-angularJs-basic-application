using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTAPP.BAL
{
    public class DepartmentService : Service, IService<DepartmentMaster, ReturnModel<DepartmentMaster>>
    {
        public ReturnModel<DepartmentMaster> Delete(DepartmentMaster model)
        {
            var data = uow.DepartmentMaster.GetSingle(o => o.Id == model.Id);
            data.Disabled = true;
            uow.DepartmentMaster.Delete(data);
            return new ReturnModel<DepartmentMaster>()
            {
                code = "0",
                datam = model
            };

        }
        public ReturnModel<DepartmentMaster> GetAll()
        {
            return new ReturnModel<DepartmentMaster>()
            {
                code = "0",
                data = uow.DepartmentMaster.GetAll(o => o.Disabled == false).Select(p => new DepartmentMaster()
                {
                    CreatedBy = p.CreatedBy,
                    Disabled = false,
                    CreatedOn = DateTime.Now,
                    DepartmentName = p.DepartmentName,
                    Company = p.Company,
                    Owner = p.Owner,
                    Id = p.Id,
                    ModifiedBy = p.ModifiedBy,
                    ModifiedOn = DateTime.Now
                })
            };
        }

        public ReturnModel<DepartmentMaster> GetSingle(DepartmentMaster model)
        {

            var data = uow.DepartmentMaster.GetSingle(o => o.Id == model.Id);
            var rData = new DepartmentMaster()
            {
                CreatedBy = data.CreatedBy,
                Disabled = false,
                CreatedOn = data.CreatedOn,
                DepartmentName = data.DepartmentName,
                Company = data.Company,
                Owner = data.Owner,
                Id = data.Id,
                ModifiedBy = data.ModifiedBy,
                ModifiedOn =DateTime.Now

            };
            return new ReturnModel<DepartmentMaster>()
            {

                code = "0",
                datam = rData
            };
        }

        public ReturnModel<DepartmentMaster> Insert(DepartmentMaster model)
        {
            uow.DepartmentMaster.Add(new DAL.DepartmentMaster()
            {
                
                CreatedBy = 0,
                CreatedOn = DateTime.Now,
                Disabled = false,
                DepartmentName = model.DepartmentName,
                Owner = model.Owner,
                Id = model.Id,
                Company = model.Company,
                ModifiedBy = model.ModifiedBy,
                ModifiedOn = DateTime.Now

            });
            return new ReturnModel<DepartmentMaster>()
            {

                code = "0",
                datam = model
            };
        }
        public ReturnModel<DepartmentMaster> Update(DepartmentMaster model)
        {
            var data = uow.DepartmentMaster.GetSingle(o => o.Id == model.Id);
            if (data != null)
            {
                data.DepartmentName = model.DepartmentName;
                data.Id = model.Id;
                data.Company = model.Company;
                data.ModifiedBy = model.ModifiedBy;
                data.ModifiedOn = DateTime.Now;
                uow.DepartmentMaster.Update(data);

            }
            return new ReturnModel<DepartmentMaster>()
            {

                code = "0",
                datam = model
            };
        }
    }
}
