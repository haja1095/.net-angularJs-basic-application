using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTAPP.BAL
{
    public class PurchaseInvoiceService : Service, IService<PurchaseInvoiceViewModel, ReturnModel<PurchaseInvoiceViewModel>>
    {
        public ReturnModel<PurchaseInvoiceViewModel> Delete(PurchaseInvoiceViewModel model)
        {
            var data = uow.PurchaseInvoiceMaster.GetSingle(o => o.Id == model.Id);
            data.Disabled = true;
            uow.PurchaseInvoiceMaster.Delete(data);
            return new ReturnModel<PurchaseInvoiceViewModel>()
            {
                code = "0",
                datam = model
            };
        }
        public ReturnModel<PurchaseInvoiceViewModel> GetAll()
        {
            return new ReturnModel<PurchaseInvoiceViewModel>
            {
                code = "0",
                data = uow.PurchaseInvoiceMaster.GetAll(o => o.Disabled == false).Select(p => new PurchaseInvoiceViewModel()
                {
                    Id = p.Id,
                    InvoiceNo = p.InvoiceNo,
                    InvoiceDate = p.InvoiceDate,
                    CompanyCode = p.CompanyCode,
                    SupplierCode = p.SupplierCode,
                    PO = p.PO,
                    PODate = p.PODate,
                    ShippingAddress = p.ShippingAddress,
                    DueDate = p.DueDate,
                    TermsId = p.TermsId,
                    Message = p.Message,
                    Discount = p.Discount,
                    TotalAmount = p.TotalAmount,
                    Disabled = p.Disabled,
                    CreatedBy = p.CreatedBy,
                    CreatedOn = p.CreatedOn,
                    ModifiedBy = p.ModifiedBy,
                    ModifiedOn = p.ModifiedOn,
                    Owner = p.Owner
                })
            };
        }
        public ReturnModel<PurchaseInvoiceViewModel> GetSingle(PurchaseInvoiceViewModel model)
        {
            var sm = uow.PurchaseInvoiceMaster.GetSingle(p => p.Id == model.Id);
            if (sm == null)
            {
                return new ReturnModel<PurchaseInvoiceViewModel>()
                {
                    code = "1",
                    error = "No data found!"
                };
            }
            var si = uow.PurchaseInvoiceItem.GetAll(p => p.PurchaseId == sm.Id);
            return new ReturnModel<PurchaseInvoiceViewModel>()
            {
                code = "0",
                datam = new PurchaseInvoiceViewModel()
                {
                    Id = sm.Id,
                    InvoiceNo = sm.InvoiceNo,
                    InvoiceDate = sm.InvoiceDate,
                    CompanyCode = sm.CompanyCode,
                    SupplierCode = sm.SupplierCode,
                    PO = sm.PO,
                    PODate = sm.PODate,
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
                    Company = new CompanyService().GetSingle(new CompanyMaster() { Code = sm.CompanyCode }).datam,
                    Supplier=new SupplierService().GetSingle(new SupplierMaster() { Code = sm.SupplierCode }).datam,
                    Terms = new TermsService().GetSingle(new TermsMaster() { Code = sm.TermsId }).datam,
                    PurchaseInvoiceItems = si.Select(o => new PurchaseInvoiceItemViewModel()
                    {
                        PurchaseId = o.PurchaseId,
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
        public ReturnModel<PurchaseInvoiceViewModel> Insert(PurchaseInvoiceViewModel model)
        {
            if (uow.PurchaseInvoiceMaster.GetSingle(o => o.InvoiceNo == model.InvoiceNo) != null)
            {
                return new ReturnModel<PurchaseInvoiceViewModel>()
                {
                    code = "1",
                    error = "Invoice Number already present!! Can't add Invoice"
                };
            }
            uow.PurchaseInvoiceMaster.Add(new DAL.PurchaseMaster()
            {
                Id = model.Id,
                InvoiceNo = model.InvoiceNo,
                InvoiceDate = Convert.ToDateTime(model.InvoiceDateSTR).Date + DateTime.Now.TimeOfDay,
                CompanyCode = model.CompanyCode,
                SupplierCode = model.SupplierCode,
                PO = model.PO,
                PODate = Convert.ToDateTime(model.PODateSTR).Date + DateTime.Now.TimeOfDay,
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
                ModifiedOn = DateTime.Now
            });
            model.PurchaseInvoiceItems.ForEach(o =>
                uow.PurchaseInvoiceItem.Add(new DAL.PurchaseItem()
                {
                    PurchaseId = uow.PurchaseInvoiceMaster.GetSingle(p => p.InvoiceNo == model.InvoiceNo).Id,
                    ItemTypeId = o.ItemTypeId,
                    ProductId = o.ProductId,
                    UnitsOrPieces = o.UnitsOrPieces,
                    WeightorLength = o.WeightorLength,
                    Rate = o.Rate,
                    Tax = o.Tax,
                    TaxAmount = o.TaxAmount,
                    Date = Convert.ToDateTime(o.DateSTR).Date + DateTime.Now.TimeOfDay,
                    Disabled = false,
                    CreatedBy = 0,
                    CreatedOn = DateTime.Now,
                    ModifiedBy = 0,
                    ModifiedOn = DateTime.Now,
                    Owner = o.Owner,
                    Discount = o.Discount,
                    IsPercentage = o.IsPercentage
                }));
            return new ReturnModel<PurchaseInvoiceViewModel>()
            {
                code = "0",
                datam = model
            };

        }

        public ReturnModel<PurchaseInvoiceViewModel> Update(PurchaseInvoiceViewModel model)
        {
            
                var data = uow.PurchaseInvoiceMaster.GetSingle(o => o.InvoiceNo == model.InvoiceNo);
                if (data == null)
                { 
                    return new ReturnModel<PurchaseInvoiceViewModel>()
                    {
                        code = "1",
                        error = "No data found!"
                    };
            }
            
                    data.Id = model.Id;
                    data.InvoiceDate = Convert.ToDateTime(model.InvoiceDateSTR).Date + data.InvoiceDate.Value.TimeOfDay;
                    data.CompanyCode = model.CompanyCode;
                    data.SupplierCode = model.SupplierCode;
                    data.PODate = Convert.ToDateTime(model.PODateSTR).Date + data.PODate.Value.TimeOfDay;
                    data.ShippingAddress = model.ShippingAddress;
                    data.DueDate = Convert.ToDateTime(model.DueDateSTR).Date + data.DueDate.Value.TimeOfDay;
                    data.TermsId = model.TermsId;
                    data.TotalAmount = model.TotalAmount;
                    data.ModifiedBy = model.ModifiedBy;
                    data.ModifiedOn = DateTime.Now;
                    data.Message = model.Message;
                    data.Discount = model.Discount;
                    uow.PurchaseInvoiceMaster.Update(data);
                
                var imydata = uow.PurchaseInvoiceItem.GetAll(o => o.Disabled == false && o.PurchaseId == data.Id);
                if (imydata.Count() > 0)
                    uow.PurchaseInvoiceItem.RemoveBulk(imydata);

                var myId = uow.PurchaseInvoiceMaster.GetSingle(p => p.InvoiceNo == model.InvoiceNo).Id;
            var idatas = model.PurchaseInvoiceItems.Select(o =>
                    new DAL.PurchaseItem
                    {
                        PurchaseId = myId,
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
                    });
                uow.PurchaseInvoiceItem.AddBulk(idatas);
                    return new ReturnModel<PurchaseInvoiceViewModel>()
                {
                    code = "0",
                    datam = model
                };
            }
        }
    }


