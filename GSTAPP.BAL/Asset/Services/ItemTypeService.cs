using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTAPP.BAL
{
    public class ItemTypeService : Service, IService<ItemTypeMaster, ReturnModel<ItemTypeMaster>>
    {
        public ReturnModel<ItemTypeMaster> Delete(ItemTypeMaster model)
        
        {
            var data = uow.ItemTypeMaster.GetSingle(o => o.Id == model.Id);
            data.Disabled = true;
            uow.ItemTypeMaster.Delete(data);
            return new ReturnModel<ItemTypeMaster>()
            {
                code = "0",
                datam = model
            };

        }


        public ReturnModel<ItemTypeMaster> GetAll()
        {
            return new ReturnModel<ItemTypeMaster>()
            {
                code = "0",
                data = uow.ItemTypeMaster.GetAll(o => o.Disabled == false).Select(p => new ItemTypeMaster()
                {
                    CreatedBy = p.CreatedBy,
                    Disabled = p.Disabled,
                    CreatedOn = p.CreatedOn,
                    ItemTypeName = p.ItemTypeName,
                    Id = p.Id,
                    Company = p.Company,
                    ItemCategory = p.ItemCategory,
                    Owner = p.Owner,
                    ModifiedBy = p.ModifiedBy,
                    ModifiedOn = p.ModifiedOn
                })
            };
        }

        public ReturnModel<ItemTypeMaster> GetSingle(ItemTypeMaster model)
        {
            var data = uow.ItemTypeMaster.GetSingle(o => o.Id == model.Id);
            var rData = new ItemTypeMaster()
            {
                CreatedBy = data.CreatedBy,
                Disabled = data.Disabled,
                CreatedOn = data.CreatedOn,
                ItemTypeName = data.ItemTypeName,
                Id = data.Id,
                Company = data.Company,
                ItemCategory=data.ItemCategory,
                Owner = data.Owner,
                ModifiedBy = data.ModifiedBy,
                ModifiedOn = data.ModifiedOn

            };
            return new ReturnModel<ItemTypeMaster>()
            {

                code = "0",
                datam = rData
            };
        }

        public ReturnModel<ItemTypeMaster> Insert(ItemTypeMaster model)
        {
            uow.ItemTypeMaster.Add(new DAL.ItemTypeMaster()
            {
               Id=model.Id,
               ItemTypeName=model.ItemTypeName,
               ItemCategory=model.ItemCategory,
               Company = model.Company,
               CreatedBy=0,
               Disabled=false,
               CreatedOn=DateTime.Now,
               Owner=model.Owner,
               ModifiedBy=0,
               ModifiedOn =DateTime.Now


            });
            return new ReturnModel<ItemTypeMaster>()
            {

                code = "0",
                datam = model
            };
        }



        public ReturnModel<ItemTypeMaster> Update(ItemTypeMaster model)
        {
            var data = uow.ItemTypeMaster.GetSingle(o => o.Id == model.Id);
            if(data!= null)
                {
                data.ItemTypeName = model.ItemTypeName;
                data.ItemCategory = model.ItemCategory;
                data.Company = model.Company;
                data.ModifiedBy = model.ModifiedBy;
                data.ModifiedOn = DateTime.Now;
                uow.ItemTypeMaster.Update(data);
            }
            return new ReturnModel<ItemTypeMaster>()
            {

                code = "0",
                datam = model
            };
        }
    }
}
