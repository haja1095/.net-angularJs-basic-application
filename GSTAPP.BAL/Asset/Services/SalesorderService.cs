using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GSTAPP.BAL
{
    public class SalesOrderService : Service, IService<SalesOrderViewModel, ReturnModel<SalesOrderViewModel>>
    {
        public ReturnModel<SalesOrderViewModel> Delete(SalesOrderViewModel model)
        {
            var data = uow.SalesOrderMaster.GetSingle(o => o.Id == model.Id);
            data.Disabled = true;
            uow.SalesOrderMaster.Delete(data);
            return new ReturnModel<SalesOrderViewModel>()
            {
                code = "0",
                datam = model
            };
        }
        public ReturnModel<SalesOrderViewModel> GetAllfromCOMCUS(SalesOrderViewModel model)
        {
            var data = uow.SalesOrderMaster.GetAll(o => o.Disabled == false && o.CompanyCode == model.CompanyCode && o.CustomerCode == model.CustomerCode).Select(p => new SalesOrderViewModel()
            {
                Id = p.Id,
                CompanyCode = p.CompanyCode,
                CustomerCode = p.CustomerCode,
                SO = p.SO,
                SODate = p.SODate,
                ShippingAddress = p.ShippingAddress,
                DueDate = p.DueDate,
                TermsId = p.TermsId,
                Message = p.Message,
                Discount = p.Discount,
                TotalAmount = p.TotalAmount,
                CreatedBy = p.CreatedBy,
                CreatedOn = p.CreatedOn,
                Disabled = p.Disabled,
                ModifiedBy = p.ModifiedBy,
                ModifiedOn = p.ModifiedOn,
                Owner = p.Owner,
                ShippingDate = p.ShippingDate,
                Company = new CompanyService().GetSingle(new CompanyMaster() { Code = p.CompanyCode }).datam,
                Customer = new CustomerService().GetSingle(new CustomerMaster() { Code = p.CustomerCode }).datam,
                Terms = new TermsService().GetSingle(new TermsMaster() { Code = p.TermsId }).datam,
                SalesOrderItems = uow.SalesOrderItem.GetAll(y => y.SalesOrderId == p.Id).Select(o => new SalesOrderItemViewModel()
                {
                    SalesOrderId = o.SalesOrderId,
                    ItemTypeId = o.ItemTypeId,
                    CreatedBy = o.CreatedBy,
                    Disabled = false,
                    CreatedOn = o.CreatedOn,
                    Owner = o.Owner,
                    ModifiedBy = o.ModifiedBy,
                    ModifiedOn = o.ModifiedOn,
                    Date = o.Date,
                    Discount = o.Discount,
                    Id = o.Id,
                    ProductId = o.ProductId,
                    Rate = o.Rate,
                    Tax = o.Tax,
                    TaxAmount = o.TaxAmount,
                    UnitsOrPieces = o.UnitsOrPieces,
                    WeightorLength = o.WeightorLength,
                    ItemType = new ItemTypeService().GetSingle(new ItemTypeMaster() { Id = o.ItemTypeId.Value }).datam,
                    Product = new ProductService().GetSingle(new ProductMaster() { Id = o.ProductId.Value }).datam
                }).ToList()
            });
            return new ReturnModel<SalesOrderViewModel>()
            {
                code = "0",
                data = data
            };
        }

        public ReturnModel<SalesOrderViewModel> GetAll()
        {
            return new ReturnModel<SalesOrderViewModel>()
            {
                code = "0",
                data = uow.SalesOrderMaster.GetAll(o => o.Disabled == false).Select(p => new SalesOrderViewModel()
                {
                    Id = p.Id,
                    CompanyCode = p.CompanyCode,
                    CustomerCode = p.CustomerCode,
                    SO = p.SO,
                    SODate = p.SODate,
                    ShippingAddress = p.ShippingAddress,
                    ShippingDate = p.ShippingDate,
                    DueDate = p.DueDate,
                    TermsId = p.TermsId,
                    Message = p.Message,
                    Discount = p.Discount,
                    TotalAmount = p.TotalAmount
                })
            };
        }
        public ReturnModel<SalesOrderViewModel> GetSingle(SalesOrderViewModel model)
        {
            var sm = uow.SalesOrderMaster.GetSingle(p => p.Id == model.Id);
            if (sm == null)
            {
                return new ReturnModel<SalesOrderViewModel>()
                {
                    code = "1",
                    error = "No data found!"
                };
            }
            var si = uow.SalesOrderItem.GetAll(p => p.SalesOrderId == sm.Id);
            return new ReturnModel<SalesOrderViewModel>()
            {
                code = "0",
                datam = new SalesOrderViewModel()
                {
                    Id = sm.Id,
                    CompanyCode = sm.CompanyCode,
                    CustomerCode = sm.CustomerCode,
                    SO = sm.SO,
                    SODate = sm.SODate,
                    ShippingAddress = sm.ShippingAddress,
                    DueDate = sm.DueDate,
                    TermsId = sm.TermsId,
                    Message = sm.Message,
                    Discount = sm.Discount,
                    TotalAmount = sm.TotalAmount,
                    CreatedBy = sm.CreatedBy,
                    CreatedOn = sm.CreatedOn,
                    Disabled = sm.Disabled,
                    ModifiedBy = sm.ModifiedBy,
                    ModifiedOn = sm.ModifiedOn,
                    Owner = sm.Owner,
                    ShippingDate = sm.ShippingDate,
                    Company = new CompanyService().GetSingle(new CompanyMaster() { Code = sm.CompanyCode }).datam,
                    Customer = new CustomerService().GetSingle(new CustomerMaster() { Code = sm.CustomerCode }).datam,
                    Terms = new TermsService().GetSingle(new TermsMaster() { Code = sm.TermsId }).datam,
                    SalesOrderItems = si.Select(o => new SalesOrderItemViewModel()
                    {
                        SalesOrderId = o.SalesOrderId,
                        ItemTypeId = o.ItemTypeId,
                        CreatedBy = o.CreatedBy,
                        Disabled = false,
                        CreatedOn = o.CreatedOn,
                        Owner = o.Owner,
                        ModifiedBy = o.ModifiedBy,
                        ModifiedOn = o.ModifiedOn,
                        Date = o.Date,
                        Discount = o.Discount,
                        Id = o.Id,
                        ProductId = o.ProductId,
                        Rate = o.Rate,
                        Tax = o.Tax,
                        TaxAmount = o.TaxAmount,
                        UnitsOrPieces = o.UnitsOrPieces,
                        WeightorLength = o.WeightorLength,
                        ItemType = o.ItemTypeId.HasValue ? new ItemTypeService().GetSingle(new ItemTypeMaster() { Id = o.ItemTypeId.Value }).datam : null,
                        Product = o.ProductId.HasValue ? new ProductService().GetSingle(new ProductMaster() { Id = o.ProductId.Value }).datam : null
                    }).OrderByDescending(s => s.Id).ToList()
                }
            };
        }
        public ReturnModel<SalesOrderViewModel> Insert(SalesOrderViewModel model)
        {
            string lastCode = "", newCode = "";
            var lastData = uow.SalesOrderMaster.GetAll().ToList();
            if (lastData.Count > 0)
            {
                lastCode = lastData.OrderBy(p => p.SO).Last().SO;
                lastCode = lastCode.Replace("SO", "");
                newCode = "SO" + (int.Parse(lastCode) + 1).ToString("00000");
            }
            else
            {
                newCode = "SO00001";
            }
            model.SO = newCode;
            uow.SalesOrderMaster.Add(new DAL.SalesOrderMaster()
            {
                Id = model.Id,
                CompanyCode = model.CompanyCode,
                CustomerCode = model.CustomerCode,
                SO = model.SO,
                SODate = Convert.ToDateTime(model.SODateSTR).Date + DateTime.Now.TimeOfDay,
                ShippingAddress = model.ShippingAddress,
                ShippingDate = Convert.ToDateTime(model.ShippingDateSTR).Date + DateTime.Now.TimeOfDay,
                DueDate = Convert.ToDateTime(model.DueDateSTR).Date + DateTime.Now.TimeOfDay,
                TermsId = model.TermsId,
                Message = model.Message,
                Discount = model.Discount,
                TotalAmount = model.TotalAmount,
                CreatedBy = 0,
                Disabled = false,
                CreatedOn = DateTime.Now,
                Owner = model.Owner,
                ModifiedBy = 0,
                ModifiedOn = DateTime.Now,
                InvoiceState = false
            });
            model.SalesOrderItems.ForEach(o =>
                uow.SalesOrderItem.Add(new DAL.SalesOrderItem()
                {
                    SalesOrderId = uow.SalesOrderMaster.GetSingle(p => p.SO == model.SO).Id,
                    ItemTypeId = o.ItemTypeId,
                    CreatedBy = 0,
                    Disabled = false,
                    CreatedOn = DateTime.Now,
                    Owner = 0,
                    ModifiedBy = 0,
                    ModifiedOn = DateTime.Now,
                    Date = Convert.ToDateTime(o.DateSTR).Date + DateTime.Now.TimeOfDay,
                    Discount = o.Discount,
                    Id = o.Id,
                    ProductId = o.ProductId,
                    Rate = o.Rate,
                    Tax = o.Tax,
                    TaxAmount = o.TaxAmount,
                    UnitsOrPieces = o.UnitsOrPieces,
                    WeightorLength = o.WeightorLength
                    
                }));
            return new ReturnModel<SalesOrderViewModel>()
            {
                code = "0",
                datam = model
            };
        }
            public ReturnModel<SalesOrderViewModel> Update(SalesOrderViewModel model)
        {
            var mydata = uow.SalesOrderMaster.GetSingle(o => o.SO == model.SO);
            if (mydata == null)
            {
                return new ReturnModel<SalesOrderViewModel>()
                {
                    code = "1",
                    error = "No data found!"
                };
            }

            mydata.CompanyCode = model.CompanyCode;
            mydata.CustomerCode = model.CustomerCode;
            mydata.SO = model.SO;
            mydata.SODate = Convert.ToDateTime(model.SODateSTR).Date + mydata.SODate.Value.TimeOfDay;
            mydata.ShippingAddress = model.ShippingAddress;
            mydata.ShippingDate = Convert.ToDateTime(model.ShippingDateSTR).Date + mydata.SODate.Value.TimeOfDay;
            mydata.DueDate = Convert.ToDateTime(model.DueDateSTR).Date + mydata.DueDate.Value.TimeOfDay;
            mydata.TermsId = model.TermsId;
            mydata.Message = model.Message;
            mydata.Discount = model.Discount;
            mydata.TotalAmount = model.TotalAmount;
            mydata.ModifiedBy = 0;
            mydata.ModifiedOn = DateTime.Now;
            uow.SalesOrderMaster.Update(mydata);

            var imydata = uow.SalesOrderItem.GetAll(o => o.Disabled == false && o.SalesOrderId == mydata.Id);
            if (imydata.Count() > 0)
                uow.SalesOrderItem.RemoveBulk(imydata);

            var myId = uow.SalesOrderMaster.GetSingle(p => p.SO == model.SO).Id;
            var idatas = model.SalesOrderItems.Select(o =>
                    new DAL.SalesOrderItem
                    {
                        SalesOrderId = myId,
                        ItemTypeId = o.ItemTypeId,
                        CreatedBy = 0,
                        Disabled = false,
                        CreatedOn = DateTime.Now,
                        Owner = 0,
                        ModifiedBy = 0,
                        ModifiedOn = DateTime.Now,
                        Date = Convert.ToDateTime(o.DateSTR).Date + o.Date.Value.TimeOfDay,
                        Discount = o.Discount,
                        Id = o.Id,
                        ProductId = o.ProductId,
                        Rate = o.Rate,
                        Tax = o.Tax,
                        TaxAmount = o.TaxAmount,
                        UnitsOrPieces = o.UnitsOrPieces,
                        WeightorLength = o.WeightorLength
                    }
                );
            uow.SalesOrderItem.AddBulk(idatas);
            return new ReturnModel<SalesOrderViewModel>()
            {
                code = "0",
                datam = model
            };
        }
    }
}