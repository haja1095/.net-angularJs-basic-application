using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTAPP.BAL
{
    public class SalesInvoiceService : Service, IService<SalesInvoiceViewModel, ReturnModel<SalesInvoiceViewModel>>
    {
        public ReturnModel<SalesInvoiceViewModel> Delete(SalesInvoiceViewModel model)
        {
            var data = uow.SalesInvoiceMaster.GetSingle(o => o.Id == model.Id);
            data.Disabled = true;
            uow.SalesInvoiceMaster.Delete(data);
            return new ReturnModel<SalesInvoiceViewModel>()
            {
                code = "0",
                datam = model
            };
        }

        public ReturnModel<SalesInvoiceViewModel> GetAll()
        {
            return new ReturnModel<SalesInvoiceViewModel>()
            {
                code = "0",
                data = uow.SalesInvoiceMaster.GetAll(o => o.Disabled == false).Select(p => new SalesInvoiceViewModel()
                {
                    Id = p.Id,
                    InvoiceNo = p.InvoiceNo,
                    InvoiceDate = p.InvoiceDate,
                    CompanyCode = p.CompanyCode,
                    CustomerCode = p.CustomerCode,
                    SO = p.SO,
                    SODate = p.SODate,
                    ShippingAddress = p.ShippingAddress,
                    DueDate = p.DueDate,
                    TermsId = p.TermsId,
                    Message = p.Message,
                    Discount = p.Discount,
                    TotalAmount = p.TotalAmount
                })
            };
        }

        public ReturnModel<SalesInvoiceViewModel> GetSingle(SalesInvoiceViewModel model)
        {
            var sm = uow.SalesInvoiceMaster.GetSingle(p => p.Id == model.Id);
            if (sm == null)
            {
                return new ReturnModel<SalesInvoiceViewModel>()
                {
                    code = "1",
                    error = "No data found!"
                };
            }
            var si = uow.SalesInvoiceItem.GetAll(p => p.SalesId == sm.Id);
            return new ReturnModel<SalesInvoiceViewModel>()
            {
                code = "0",
                datam = new SalesInvoiceViewModel()
                {
                    Id = sm.Id,
                    InvoiceNo = sm.InvoiceNo,
                    InvoiceDate = sm.InvoiceDate,
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
                    DiscountAmount = sm.DiscountAmount,
                    Company = new CompanyService().GetSingle(new CompanyMaster() { Code = sm.CompanyCode }).datam,
                    Customer = new CustomerService().GetSingle(new CustomerMaster() { Code = sm.CustomerCode }).datam,
                    Terms = new TermsService().GetSingle(new TermsMaster() { Code = sm.TermsId }).datam,
                    SalesInvoiceItems = uow.SalesInvoiceItem.GetAll(y => y.SalesId == sm.Id).Select(o => new SalesInvoiceItemViewModel()
                    {
                        SalesId = o.SalesId,
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
                        IsPercentage = o.IsPercentage,
                        ItemType = o.ItemTypeId.HasValue ? new ItemTypeService().GetSingle(new ItemTypeMaster() { Id = o.ItemTypeId.Value }).datam : null,
                        Product = o.ProductId.HasValue ? new ProductService().GetSingle(new ProductMaster() { Id = o.ProductId.Value }).datam : null
                    }).OrderBy(s=>s.Id).ToList()

                }
            };
        }

        public ReturnModel<SalesInvoiceViewModel> Insert(SalesInvoiceViewModel model)
        {
            if (uow.SalesInvoiceMaster.GetSingle(o => o.InvoiceNo == model.InvoiceNo) != null)
            {
                return new ReturnModel<SalesInvoiceViewModel>()
                {
                    code = "1",
                    error = "Invoice Number already present!! Can't add Invoice"
                };
            }
            uow.SalesInvoiceMaster.Add(new DAL.SalesMaster()
            {
                Id = model.Id,
                InvoiceNo = model.InvoiceNo,
                InvoiceDate = Convert.ToDateTime(model.InvoiceDateSTR).Date + DateTime.Now.TimeOfDay,
                CompanyCode = model.CompanyCode,
                CustomerCode = model.CustomerCode,
                SO = model.SO,
                SODate = Convert.ToDateTime(model.SODateSTR).Date + DateTime.Now.TimeOfDay,
                ShippingAddress = model.ShippingAddress,
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
                DiscountAmount = model.DiscountAmount

            });
            model.SalesInvoiceItems.ForEach(o =>
                uow.SalesInvoiceItem.Add(new DAL.SalesItem()
                {
                    SalesId = uow.SalesInvoiceMaster.GetSingle(p => p.InvoiceNo == model.InvoiceNo).Id,
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
                    WeightorLength = o.WeightorLength,
                    IsPercentage = o.IsPercentage
                }));
            return new ReturnModel<SalesInvoiceViewModel>()
            {
                code = "0",
                datam = model
            };
        }

        public ReturnModel<SalesInvoiceViewModel> Update(SalesInvoiceViewModel model)
        {
            var mydata = uow.SalesInvoiceMaster.GetSingle(o => o.InvoiceNo == model.InvoiceNo);
            if (mydata == null)
            {
                return new ReturnModel<SalesInvoiceViewModel>()
                {
                    code = "1",
                    error = "No data found!"
                };
            }

            mydata.InvoiceDate = Convert.ToDateTime(model.InvoiceDateSTR).Date + mydata.InvoiceDate.Value.TimeOfDay;
            mydata.CompanyCode = model.CompanyCode;
            mydata.CustomerCode = model.CustomerCode;
            mydata.SO = model.SO;
            mydata.SODate = Convert.ToDateTime(model.SODateSTR).Date + mydata.SODate.Value.TimeOfDay;
            mydata.ShippingAddress = model.ShippingAddress;
            mydata.DueDate = Convert.ToDateTime(model.DueDateSTR).Date + mydata.DueDate.Value.TimeOfDay;
            mydata.TermsId = model.TermsId;
            mydata.Message = model.Message;
            mydata.Discount = model.Discount;
            mydata.TotalAmount = model.TotalAmount;
            mydata.ModifiedBy = 0;
            mydata.ModifiedOn = DateTime.Now;
            mydata.DiscountAmount = model.DiscountAmount;
            uow.SalesInvoiceMaster.Update(mydata);

            var imydata = uow.SalesInvoiceItem.GetAll(o => o.Disabled == false && o.SalesId == mydata.Id);
            if (imydata.Count() > 0)
                uow.SalesInvoiceItem.RemoveBulk(imydata);

            var myId = uow.SalesInvoiceMaster.GetSingle(p => p.InvoiceNo == model.InvoiceNo).Id;
            var idatas = model.SalesInvoiceItems.Select(o =>
                    new DAL.SalesItem
                    {
                        SalesId = myId,
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
                        WeightorLength = o.WeightorLength,
                        IsPercentage = o.IsPercentage
                    }
                );
            uow.SalesInvoiceItem.AddBulk(idatas);
            return new ReturnModel<SalesInvoiceViewModel>()
            {
                code = "0",
                datam = model
            };
        }
    }
}
