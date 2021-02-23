using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTAPP.BAL
{
    public class ItemCategoryService : Service, IService<ItemCategoryMaster,ReturnModel<ItemCategoryMaster>>
    {
        public ReturnModel<ItemCategoryMaster> Delete(ItemCategoryMaster model)
        {
            var data = uow.ItemCategoryMaster.GetSingle(o => o.Id == model.Id);
            data.Disabled = true;
            uow.ItemCategoryMaster.Delete(data);
            return new ReturnModel<ItemCategoryMaster>()
            {
                code = "0",
                datam = model
            };

        }
        public ReturnModel<ItemCategoryMaster> GetAll()
        {
            return new ReturnModel<ItemCategoryMaster>()
            {
                code = "0",
                data = uow.ItemCategoryMaster.GetAll(o => o.Disabled == false).Select(p => new ItemCategoryMaster()
                {


                    CreatedBy = p.CreatedBy,
                    Disabled = p.Disabled,
                    CreatedOn = p.CreatedOn,
                    ItemCategoryName = p.ItemCategoryName,
                    Id = p.Id,
                    Company = p.Company,
                    Owner = p.Owner,
                    ModifiedBy = p.ModifiedBy,
                    ModifiedOn = p.ModifiedOn


                })
            };
        }

        public ReturnModel<ItemCategoryMaster> GetSingle(ItemCategoryMaster model)
        {
            var data = uow.ItemCategoryMaster.GetSingle(o => o.Id == model.Id);
            var rData = new ItemCategoryMaster()
            {
                CreatedBy = data.CreatedBy,
                Disabled = data.Disabled,
                CreatedOn = data.CreatedOn,
                ItemCategoryName = data.ItemCategoryName,
                Id = data.Id,
                Company = data.Company,
                Owner = data.Owner,
                ModifiedBy = data.ModifiedBy,
                ModifiedOn = data.ModifiedOn

            };
            return new ReturnModel<ItemCategoryMaster>()
            {

                code = "0",
                datam = rData
            };
        }
        public ReturnModel<ItemCategoryMaster> Insert(ItemCategoryMaster model)
        {
            {
                uow.ItemCategoryMaster.Add(new DAL.ItemCategoryMaster()
                {
                    Id = model.Id,
                    ItemCategoryName = model.ItemCategoryName,
                    Company = model.Company,
                    CreatedBy = model.CreatedBy,
                    Disabled = false,
                    CreatedOn = DateTime.Now,
                    Owner = model.Owner,
                    ModifiedBy = model.ModifiedBy,
                    ModifiedOn = DateTime.Now,


                });
                return new ReturnModel<ItemCategoryMaster>()
                {

                    code = "0",
                    datam = model
                };
            }
        }

        public ReturnModel<ItemCategoryMaster> Update(ItemCategoryMaster model)

        {

            var data = uow.ItemCategoryMaster.GetSingle(o => o.Id == model.Id);
            if (data != null)
            {

                data.ItemCategoryName = model.ItemCategoryName;
                data.Company = model.Company;
                data.ModifiedBy = model.ModifiedBy;
                data.ModifiedOn = DateTime.Now;

                uow.ItemCategoryMaster.Update(data);

            }
            return new ReturnModel<ItemCategoryMaster>()
            {

                code = "0",
                datam = model
            };
        }

    }
}
