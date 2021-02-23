

using GSTAPP.BAL;
using System.Collections.Generic;
using System;
using System.Linq;
using GSTAPP.DAL;

namespace GSTAPP.BAL
{
    public class ProductService : Service, IService<ProductMaster,ReturnModel<ProductMaster>>
    {

        public ReturnModel<ProductMaster> Delete(ProductMaster model)
        {
            var data = uow.ProductMaster.GetSingle(o => o.Id == model.Id);
            data.Disabled = true;
            uow.ProductMaster.Delete(data);
            return new ReturnModel<ProductMaster>()
            {
                code = "0",
                datam = model
            };

        }

        public ReturnModel<ProductMaster> GetAll()
        {
            return new ReturnModel<ProductMaster>()
            {
                code = "0",
                data = uow.ProductMaster.GetAll(o => o.Disabled == false).Select(p => new ProductMaster()
                {
                    CreatedBy = p.CreatedBy,
                    Disabled = false,
                    CreatedOn = DateTime.Now,
                    ProductName = p.ProductName,
                    Company = p.Company,
                    Department = p.Department,
                    ProductGroup = p.ProductGroup,
                    Owner = p.Owner,
                    Id = p.Id,
                    ModifiedBy = p.ModifiedBy,
                    ModifiedOn = DateTime.Now,
                    Rate = p.Rate
                })
            };
        }

        public ReturnModel<ProductMaster> GetSingle(ProductMaster model)
        {
            var data = uow.ProductMaster.GetSingle(o => o.Id == model.Id);
            var rData = new ProductMaster()
            {
                CreatedBy = data.CreatedBy,
                Disabled = data.Disabled,
                CreatedOn = data.CreatedOn,
                Owner = data.Owner,
                Id = data.Id,
                ModifiedBy = data.ModifiedBy,
                ModifiedOn = data.ModifiedOn,
                Barcode = data.Barcode,
                CGST = data.CGST,
                Company = data.Company,
                Department = data.Department,
                HSNCode = data.HSNCode,
                SGST = data.SGST,
                IGST = data.IGST,
                ProductName = data.ProductName,
                ProductGroup = data.ProductGroup,
                Rate = data.Rate,
                Taxable = data.Taxable,
                VAT = data.VAT

            };
            return new ReturnModel<ProductMaster>()
            {

                code = "0",
                datam = rData
            };
        }

        public ReturnModel<ProductMaster> GetAllUsinCompany(CompanyMaster model)
        {
            List<ProductMaster> pp = new List<ProductMaster>();
            var prod = uow.ProductMaster.GetAll(o => o.Disabled == false && o.Company != null).ToList();
            for (int i = 0; i < prod.Count; i++)
            {
                if (prod[i].Company.Length > 0)
                {
                    var comp = prod[i].Company.Split(',').FirstOrDefault(o => o.ToUpper() == model.Code.ToString().ToUpper());
                    if (comp != null)
                    {
                        pp.Add(new ProductMaster()
                        {
                            CreatedBy = prod[i].CreatedBy,
                            Disabled = prod[i].Disabled,
                            CreatedOn = prod[i].CreatedOn,
                            Owner = prod[i].Owner,
                            Id = prod[i].Id,
                            ModifiedBy = prod[i].ModifiedBy,
                            ModifiedOn = prod[i].ModifiedOn,
                            Barcode = prod[i].Barcode,
                            CGST = prod[i].CGST,
                            Company = prod[i].Company,
                            Department = prod[i].Department,
                            HSNCode = prod[i].HSNCode,
                            SGST = prod[i].SGST,
                            IGST = prod[i].IGST,
                            ProductName = prod[i].ProductName,
                            ProductGroup = prod[i].ProductGroup,
                            Rate = prod[i].Rate,
                            Taxable = prod[i].Taxable,
                            VAT = prod[i].VAT
                        });
                    }
                }
            }
            return new ReturnModel<ProductMaster>()
            {
                code = "0",
                data = pp
            };
        }

        public ReturnModel<ProductMaster> Insert(ProductMaster model)
        {
                uow.ProductMaster.Add(new DAL.ProductMaster()
                {
                    CreatedBy = model.CreatedBy,
                    Disabled = false,
                    CreatedOn = DateTime.Now,
                    Owner = model.Owner,
                    Id = model.Id,
                    ModifiedBy = model.ModifiedBy,
                    ModifiedOn = DateTime.Now,
                    Barcode = model.Barcode,
                    CGST = model.CGST,
                    Company = model.Company,
                    Department = model.Department,
                    HSNCode = model.HSNCode,
                    SGST = model.SGST,
                    IGST = model.IGST,
                    ProductName = model.ProductName,
                    ProductGroup = model.ProductGroup,
                    Rate = model.Rate,
                    Taxable = model.Taxable,
                    VAT = model.VAT
                });
                return new ReturnModel<ProductMaster>()
                {

                    code = "0",
                    datam = model
                };
        }
              

        public ReturnModel<ProductMaster> Update(ProductMaster model)
        {
            var data = uow.ProductMaster.GetSingle(o => o.Id == model.Id);
            if (data != null)
            {
                data.ProductName = model.ProductName;
                data.Id = model.Id;
                data.Company = model.Company;
                data.Department = model.Department;
                data.Owner = model.Owner;
                data.ProductGroup = model.ProductGroup;
                data.Rate = model.Rate;
                data.SGST = model.SGST;
                data.Taxable = model.Taxable;
                data.VAT = model.VAT;
                data.IGST = model.IGST;
                data.Barcode = model.Barcode;
                data.CGST = model.CGST;
                data.ModifiedBy = model.ModifiedBy;
                data.ModifiedOn = DateTime.Now;
                data.CreatedBy = model.CreatedBy;
                data.Disabled = false;
                data.CreatedOn = DateTime.Now;
                uow.ProductMaster.Update(data);
            }
            return new ReturnModel<ProductMaster>()
            {

                code = "0",
                datam = model
            };
        }
    }
}

