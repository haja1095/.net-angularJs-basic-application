using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSTAPP.BAL
{
    public class PurchaseOrderService : Service, IService<PurchaseOrderViewModel, ReturnModel<PurchaseOrderViewModel>>
    {
        public ReturnModel<PurchaseOrderViewModel> Delete(PurchaseOrderViewModel model)
        {
            var data = uow.PurchaseOrderMaster.GetSingle(o => o.Id == model.Id);
            data.Disabled = true;
            uow.PurchaseOrderMaster.Delete(data);
            return new ReturnModel<PurchaseOrderViewModel>()
            {
                code = "0",
                datam = model
            };

        }
        public ReturnModel<PurchaseOrderViewModel> GetAll()
        {
            return new ReturnModel<PurchaseOrderViewModel>()
            {
                code = "0",
                data = uow.PurchaseOrderMaster.GetAll(o => o.Disabled == false).Select(p => new PurchaseOrderViewModel()
                {
                    CreatedBy = p.CreatedBy,
                    Disabled = p.Disabled,
                    CreatedOn = p.CreatedOn,
                    Id = p.Id,
                    CompanyCode = p.CompanyCode,
                    SupplierCode = p.SupplierCode,
                    PO = p.PO,
                    PODate = p.PODate,
                    ShippingAddress = p.ShippingAddress,
                    TermsId = p.TermsId,
                    TotalAmount = p.TotalAmount,
                    Discount = p.Discount,
                    DueDate = p.DueDate,
                    Message = p.Message,
                    InvoiceState = p.InvoiceState,
                    ShippingDate = p.ShippingDate,
                    Owner = p.Owner,
                    ModifiedBy = p.ModifiedBy,
                    ModifiedOn = p.ModifiedOn,
                    IsPercentage = p.IsPercentage


                })
            };
        }

        public ReturnModel<PurchaseOrderViewModel> GetSingle(PurchaseOrderViewModel model)
        {
            var pm = uow.PurchaseOrderMaster.GetSingle(p => p.Id == model.Id);
            if (pm == null)
            {
                return new ReturnModel<PurchaseOrderViewModel>()
                {
                    code = "1",
                    error = "No data found!"
                };
            }
            var pi = uow.PurchaseOrderItem.GetAll(p => p.PurchaseOrderId == pm.Id);
            return new ReturnModel<PurchaseOrderViewModel>()
            {
                code = "0",
                datam = new PurchaseOrderViewModel()
                {
                    Id = pm.Id,
                    PODate = pm.PODate,
                    CompanyCode = pm.CompanyCode,
                    SupplierCode = pm.SupplierCode,
                    PO = pm.PO,
                    ShippingAddress = pm.ShippingAddress,
                    DueDate = pm.DueDate,
                    TermsId = pm.TermsId,
                    Message = pm.Message,
                    Discount = pm.Discount,
                    TotalAmount = pm.TotalAmount,
                    ShippingDate = pm.ShippingDate,
                    CreatedBy = pm.CreatedBy,
                    CreatedOn = pm.CreatedOn,
                    Disabled = pm.Disabled,
                    IsPercentage = pm.IsPercentage,
                    ModifiedBy = pm.ModifiedBy,
                    ModifiedOn = pm.ModifiedOn,
                    Owner = pm.Owner,
                    DiscountAmount = pm.DiscountAmount,
                    Company = new CompanyService().GetSingle(new CompanyMaster() { Code = pm.CompanyCode }).datam,
                    Supplier = new SupplierService().GetSingle(new SupplierMaster() { Code = pm.SupplierCode }).datam,
                    Terms = new TermsService().GetSingle(new TermsMaster() { Code = pm.TermsId }).datam,
                    PurchaseOrderItems = pi.Select(o => new PurchaseOrderItemViewModel()
                    {
                        PurchaseOrderId = o.PurchaseOrderId,
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
                        ItemType = new ItemTypeService().GetSingle(new ItemTypeMaster() { Id = o.ItemTypeId.Value }).datam,
                        Product = new ProductService().GetSingle(new ProductMaster() { Id = o.ProductId.Value }).datam
                    }).OrderByDescending(s=>s.Id).ToList()
                }
            };
        }

        public ReturnModel<PurchaseOrderViewModel> GetAllfromCOMSUP(PurchaseOrderViewModel model)
        {
            var data = uow.PurchaseOrderMaster.GetAll(o => o.Disabled == false && o.CompanyCode == model.CompanyCode && o.SupplierCode == model.SupplierCode).Select(p => new PurchaseOrderViewModel()
            {
                Id = p.Id,
                PODate = p.PODate,
                CompanyCode = p.CompanyCode,
                SupplierCode = p.SupplierCode,
                PO = p.PO,
                ShippingAddress = p.ShippingAddress,
                DueDate = p.DueDate,
                TermsId = p.TermsId,
                Message = p.Message,
                Discount = p.Discount,
                TotalAmount = p.TotalAmount,
                ShippingDate = p.ShippingDate,
                CreatedBy = p.CreatedBy,
                CreatedOn = p.CreatedOn,
                Disabled = p.Disabled,
                ModifiedBy = p.ModifiedBy,
                ModifiedOn = p.ModifiedOn,
                Owner = p.Owner,
                IsPercentage = p.IsPercentage,
                DiscountAmount = p.DiscountAmount,
                Company = new CompanyService().GetSingle(new CompanyMaster() { Code = p.CompanyCode }).datam,
                Supplier = new SupplierService().GetSingle(new SupplierMaster() { Code = p.SupplierCode }).datam,
                Terms = new TermsService().GetSingle(new TermsMaster() { Code = p.TermsId }).datam,
                PurchaseOrderItems = uow.PurchaseOrderItem.GetAll(y => y.PurchaseOrderId == p.Id).Select(o => new PurchaseOrderItemViewModel()
                {
                    PurchaseOrderId = o.PurchaseOrderId,
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
                    ItemType = new ItemTypeService().GetSingle(new ItemTypeMaster() { Id = o.ItemTypeId.Value }).datam,
                    Product = new ProductService().GetSingle(new ProductMaster() { Id = o.ProductId.Value }).datam
                }).ToList()
            });
            return new ReturnModel<PurchaseOrderViewModel>()
            {
                code = "0",
                data = data
            };
        }
        public ReturnModel<PurchaseOrderViewModel> Insert(PurchaseOrderViewModel model)
        {


            string lastCode = "", newCode = "";
            var lastData = uow.PurchaseOrderMaster.GetAll().ToList();
            if (lastData.Count > 0)
            {
                lastCode = lastData.OrderBy(p => p.PO).Last().PO;
                lastCode = lastCode.Replace("PO", "");
                newCode = "PO" + (int.Parse(lastCode) + 1).ToString("00000");
            }
            else
            {
                newCode = "PO00001";
            }
            model.PO = newCode;
            uow.PurchaseOrderMaster.Add(new DAL.PurchaseOrderMaster()
            {
                Id = model.Id,
                CompanyCode = model.CompanyCode,
                CreatedOn = DateTime.Now,
                Disabled = false,
                InvoiceState = false,
                SupplierCode = model.SupplierCode,
                TermsId = model.TermsId,
                CreatedBy = 1,
                ModifiedBy = 1,
                ModifiedOn = DateTime.Now,
                Owner = 1,
                Discount = model.Discount,
                DueDate = Convert.ToDateTime(model.DueDateSTR).Date + DateTime.Now.TimeOfDay,
                PODate = Convert.ToDateTime(model.PODateSTR).Date + DateTime.Now.TimeOfDay,
                ShippingDate = Convert.ToDateTime(model.ShippingDateSTR).Date + DateTime.Now.TimeOfDay,
                Message = model.Message,
                PO = model.PO,
                ShippingAddress = model.ShippingAddress,
                TotalAmount = model.TotalAmount,
                IsPercentage = model.IsPercentage,
                DiscountAmount = model.DiscountAmount
                


            });

            model.PurchaseOrderItems.ForEach(o =>
                uow.PurchaseOrderItem.Add(new DAL.PurchaseOrderItem()
                {
                    PurchaseOrderId = uow.PurchaseOrderMaster.GetSingle(p => p.PO == model.PO).Id,
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
            return new ReturnModel<PurchaseOrderViewModel>()
            {

                code = "0",
                datam = model
            };
        }
        public ReturnModel<PurchaseOrderViewModel> Update(PurchaseOrderViewModel model)
        {
            var mydata = uow.PurchaseOrderMaster.GetSingle(o => o.PO == model.PO);
            if (mydata == null)
            {
                return new ReturnModel<PurchaseOrderViewModel>()
                {
                    code = "1",
                    error = "No data found!"
                };
            }

            mydata.PODate = Convert.ToDateTime(model.PODateSTR).Date + DateTime.Now.TimeOfDay;
            mydata.CompanyCode = model.CompanyCode;
            mydata.SupplierCode = model.SupplierCode;
            mydata.ShippingAddress = model.ShippingAddress;
            mydata.ShippingDate = Convert.ToDateTime(model.ShippingDateSTR).Date + DateTime.Now.TimeOfDay;
            mydata.DueDate = Convert.ToDateTime(model.DueDateSTR).Date + DateTime.Now.TimeOfDay;
            mydata.TermsId = model.TermsId;
            mydata.Message = model.Message;
            mydata.Discount = model.Discount;
            mydata.TotalAmount = model.TotalAmount;
            mydata.ModifiedBy = 0;
            mydata.ModifiedOn = DateTime.Now;
            mydata.IsPercentage = model.IsPercentage;
            mydata.DiscountAmount = model.DiscountAmount;
            uow.PurchaseOrderMaster.Update(mydata);

            var imydata = uow.PurchaseOrderItem.GetAll(o => o.Disabled == false && o.PurchaseOrderId == mydata.Id);
            if (imydata.Count() > 0)
                uow.PurchaseOrderItem.RemoveBulk(imydata);

            var myId = uow.PurchaseOrderMaster.GetSingle(p => p.PO == model.PO).Id;
            var idatas = model.PurchaseOrderItems.Select(o =>
                    new DAL.PurchaseOrderItem
                    {
                        PurchaseOrderId = myId,
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
                    }
                );
            uow.PurchaseOrderItem.AddBulk(idatas);
            return new ReturnModel<PurchaseOrderViewModel>()
            {
                code = "0",
                datam = model 
            };
        }
    }
}

