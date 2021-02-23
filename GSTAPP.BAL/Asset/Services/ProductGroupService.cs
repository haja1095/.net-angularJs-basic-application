using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTAPP.BAL
{
    public class ProductGroupService : Service, IService<ProductGroupMaster, ReturnModel<ProductGroupMaster>>
    {
        public ReturnModel<ProductGroupMaster> Delete(ProductGroupMaster model)
        {
          
            var data = uow.ProductGroupMaster.GetSingle(o => o.Id == model.Id);
            data.Disabled = true;
            uow.ProductGroupMaster.Delete(data);
            return new ReturnModel<ProductGroupMaster>()
            {
                code = "0",
                datam = model
            };

        }
        public ReturnModel<ProductGroupMaster> GetAll()
        {
            return new ReturnModel<ProductGroupMaster>()
            {
                code = "0",
                data = uow.ProductGroupMaster.GetAll(o => o.Disabled == false).Select(p => new ProductGroupMaster()
                {
                    CreatedBy = p.CreatedBy,
                    Disabled = p.Disabled,
                    CreatedOn = p.CreatedOn,
                    ProductGroupName = p.ProductGroupName,
                    Id = p.Id,
                    Company = p.Company,
                    Owner = p.Owner,
                    ModifiedBy = p.ModifiedBy,
                    ModifiedOn = p.ModifiedOn
                })
            };
        }

        public ReturnModel<ProductGroupMaster> GetSingle(ProductGroupMaster model)
        {
            var data = uow.ProductGroupMaster.GetSingle(o => o.Id == model.Id);
            var rData = new ProductGroupMaster()
            {
                CreatedBy = data.CreatedBy,
                Disabled = false,
                CreatedOn = data.CreatedOn,
                ProductGroupName = data.ProductGroupName,
                Id = data.Id,
                Company = data.Company,
                Owner = data.Owner,
                ModifiedBy = data.ModifiedBy,
                ModifiedOn = data.ModifiedOn

            };
            return new ReturnModel<ProductGroupMaster>()
            {

                code = "0",
                datam = rData
            };
        }

        public ReturnModel<ProductGroupMaster> Insert(ProductGroupMaster model)
        {
            uow.ProductGroupMaster.Add(new DAL.ProductGroupMaster()
            {
                Id = model.Id,
                ProductGroupName = model.ProductGroupName,
                Company = model.Company,
                CreatedBy = 0,
                Disabled = false,
                CreatedOn = DateTime.Now,
                Owner = model.Owner,
                ModifiedBy = 0,
                ModifiedOn = DateTime.Now
            });
            return new ReturnModel<ProductGroupMaster>()
            {

                code = "0",
                datam = model
            };
        }


        public ReturnModel<ProductGroupMaster> Update(ProductGroupMaster model)
        {
            var data = uow.ProductGroupMaster.GetSingle(o => o.Id == model.Id);
            if (data != null)
            {
                data.ProductGroupName = model.ProductGroupName;
                data.Company = model.Company;
                data.ModifiedBy = model.ModifiedBy;
                data.ModifiedOn = DateTime.Now;
                uow.ProductGroupMaster.Update(data);
            }
            return new ReturnModel<ProductGroupMaster>()
            {

                code = "0",
                datam = model
            };
        }

    }

}

