'use strict';

angular.module('GSTApp')
.controller("purchaseinvoiceCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', 'dialogModelService', 'toasterService', function (scope, state, timeout, window, ApiFactory, dialogModelService, toasterService) {
    scope.purchaseinvoices = [];
    function load() {
        var res = ApiFactory.getData("PurchaseInvoice/GetAll");
        res.success(function (data, status) {
            scope.purchaseinvoices = data.data;
            angular.element(document).ready(function () {
                var table = $('#purchaseinvoiceTbl').DataTable({
                    select: true,
                    "scrollX": true
                });
            });
        });
    }
    load();
    scope.delete = function (data) {
        var res = ApiFactory.setData("PurchaseInvoice/Delete", data);
        res.success(function (data, status) {
            $('#purchaseinvoiceTbl').DataTable().destroy();
            toasterService.success("Purchase Invoice was deleted");
            load();
        });
    }
    scope.showDelete = function (data) {
        dialogModelService.show().result.then(function () {
            scope.delete(data);
        });
    }
}])
.controller("purchaseinvoiceFormCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', '$stateParams', 'dialogModelService', 'toasterService', 'AppConstant', function (scope, state, timeout, window, ApiFactory, stateParams, dialogModelService, toasterService,AppConstant) {
    scope.open3 = function (item) {
        item.opened = true;
    };
    scope.open1 = function (item) {
        item.opened1 = true;
    };
    scope.open2 = function (item) {
        item.opened2 = true;
    };
    scope.PO = {
        CompanyCode: "",
        SupplierCode: ""
    }
    scope.purchaseinvoice = {

        Id: "",
        InvoiceNo: "",
        InvoiceDate: new Date(),
        CompanyCode: "",
        SupplierCode: "",
        PO: "",
        PODate: new Date(),
        ShippingAddress: "",
        DueDate: new Date(),
        TermsId: "",
        Message: "",
        Discount: "",
        IsPercentage: false,
        TotalAmount: "",
        Disabled: "",
        CreatedBy: "",
        CreatedOn: "",
        ModifiedBy: "",
        ModifiedOn: "",
        IGST: "",
        CGST: "",
        SGST: "",
        Owner: "",
        PurchaseInvoiceItems: []
    };
    scope.purchaseItem = {
        Id: "",
        PurchaseId: "",
        ProductId: "",
        WeightorLength: "",
        UnitsOrPieces: "",
        ItemTypeId: "",
        Rate: "",
        Tax: "",
        TaxAmount: "",
        Date: new Date(),
        Discount: "",
        IsPercentage: false
    }
    scope.product = [];
    scope.purchaseinvoice.PurchaseInvoiceItems.push(angular.copy(scope.purchaseItem));
    if (state.$current.name == "app.purchaseinvoiceForm") {
        scope.purchaseinvoice.Id = stateParams.Code;
    }
    else {
        scope.purchaseinvoice.Id = "0";
    }
    scope.purchaseinvoiceClone = angular.copy(scope.purchaseinvoice);

    if (scope.purchaseinvoice.Id != "0") {
        var res = ApiFactory.setData("PurchaseInvoice/GetSingle", scope.purchaseinvoice);
        res.success(function (data, status) {
            if (data.datam != null) {
                scope.purchaseinvoice.Id = data.datam.Id;
                scope.purchaseinvoice.InvoiceNo = data.datam.InvoiceNo;
                scope.purchaseinvoice.InvoiceNo1 = data.datam.InvoiceNo;
                scope.purchaseinvoice.InvoiceDate = new Date(data.datam.InvoiceDate);
                scope.purchaseinvoice.CompanyCode = data.datam.CompanyCode;
                scope.purchaseinvoice.SupplierCode = data.datam.SupplierCode;
                scope.purchaseinvoice.PO = data.datam.PO;
                scope.purchaseinvoice.PODate = new Date(data.datam.PODate);
                scope.purchaseinvoice.ShippingAddress = data.datam.ShippingAddress;
                scope.purchaseinvoice.SupplierAddress = data.datam.SupplierAddress;
                scope.purchaseinvoice.DueDate = new Date(data.datam.DueDate);
                scope.purchaseinvoice.TermsId = data.datam.TermsId;
                scope.purchaseinvoice.Message = data.datam.Message;
                scope.purchaseinvoice.Discount = data.datam.Discount;
                scope.purchaseinvoice.TotalAmount = data.datam.TotalAmount;
                scope.purchaseinvoice.Disabled = data.datam.Disabled;
                scope.purchaseinvoice.CreatedBy = data.datam.CreatedBy;
                scope.purchaseinvoice.CreatedOn = data.datam.CreatedOn;
                scope.purchaseinvoice.ModifiedBy = data.datam.ModifiedBy;
                scope.purchaseinvoice.ModifiedOn = data.datam.ModifiedOn;
                scope.purchaseinvoice.IGST = data.datam.IGST;
                scope.purchaseinvoice.CGST = data.datam.CGST;
                scope.purchaseinvoice.SGST = data.datam.SGST;
                scope.purchaseinvoice.Owner = data.datam.Owner;
                scope.purchaseinvoice.IsPercentage = data.datam.IsPercentage;
                data.datam.PurchaseInvoiceItems.forEach(function (o) {
                    o.Date = new Date(o.Date);
                    o.ItemTypeId = o.ItemTypeId + "";
                    o.ProductId = o.ProductId + "";
                })
                scope.purchaseinvoice.PurchaseInvoiceItems = data.datam.PurchaseInvoiceItems;
                scope.setProduct({ Code: scope.purchaseinvoice.CompanyCode });
                scope.purchaseinvoice.CompanyAddress = data.datam.Company.Address;
                scope.purchaseinvoice.SupplierAddress = data.datam.Supplier.Address;
                scope.purchaseinvoiceClone = angular.copy(scope.purchaseinvoice);
                angular.element(document).ready(function () {
                    $('.productCS').selectpicker('refresh');
                    $('.itemTypeCS').selectpicker('refresh');
                    $('#purchaseinvoiceForm_Company').selectpicker('refresh');
                    $('#purchaseinvoiceForm_Supplier').selectpicker('refresh');
                    $('#purchaseinvoiceForm_TermsId').selectpicker('refresh');

                });
                
            }
            else {
                scope.purchaseinvoice = angular.copy(scope.purchaseinvoiceClone);
            }
        });
    }
    scope.setPOs = function () {
        if (scope.PO.CompanyCode.length > 0 && scope.PO.SupplierCode.length > 0) {
            var resPO = ApiFactory.getData("PurchaseOrder/GetAllfromCOMSUP", scope.PO);
            resPO.success(function (data, status) {
                if (data.data != null) {
                    scope.POs = data.data;
                    angular.element(document).ready(function () {
                        $('#purchaseinvoiceForm_PODtl').selectpicker('refresh');
                    });
                }
            });
        }
    }
    angular.element(document).ready(function () {
        $('.selectpicker').selectpicker('refresh');
    });
    var resCom = ApiFactory.getData("Company/GetAll");
    resCom.success(function (data, status) {
        if (data.data != null) {
            scope.companys = data.data;
            angular.element(document).ready(function () {
                $('#purchaseinvoiceForm_POCompany,#purchaseinvoiceForm_Company').selectpicker('refresh');
            });
        }
    });

    var resItmTp = ApiFactory.getData("ItemType/GetAll");
    resItmTp.success(function (data, status) {
        if (data.data != null) {
            scope.itemtypes = data.data;
            angular.element(document).ready(function () {
                $('.itemTypeCS').selectpicker('refresh');
            });
        }
    });
    scope.setComAddress = function () {
        var data = scope.companys.find(p=>p.Code == scope.purchaseinvoice.CompanyCode);
        if (data != null) {
            scope.purchaseinvoice.ShippingAddress = data.Address;
            scope.setProduct(data);
        }
    }
    scope.setSupAddress = function () {
        var data = scope.suppliers.find(p=>p.Code == scope.purchaseinvoice.SupplierCode);
        if (data != null)
            scope.purchaseinvoice.SupplierAddress = data.Address;
    }
    scope.setProduct = function (data) {
        var resItmTp = ApiFactory.getData("Product/GetAllUsinCompany", data);
        resItmTp.success(function (data, status) {
            if (data.data != null) {
                scope.products = data.data;
                angular.element(document).ready(function () {
                    $('.productCS').selectpicker('refresh');
                });
            }
        });
    }
    scope.setRate = function (index) {
        var data = scope.products.find(p=>p.Id == scope.purchaseinvoice.PurchaseInvoiceItems[index].ProductId);
        var dataCom = scope.companys.find(p=>p.Code == scope.purchaseinvoice.CompanyCode);
        var dataSup = scope.suppliers.find(p=>p.Code == scope.purchaseinvoice.SupplierCode);
        if (data != null && dataCom != null && dataSup != null) {
            scope.purchaseinvoice.PurchaseInvoiceItems[index].Rate = data.Rate;
            if ((dataCom.State).toLowerCase() == (dataSup.State).toLowerCase())
                scope.purchaseinvoice.PurchaseInvoiceItems[index].Tax = data.CGST + data.SGST;
            else
                scope.purchaseinvoice.PurchaseInvoiceItems[index].Tax = data.IGST;
        }
        scope.setAmout(scope.purchaseinvoice.PurchaseInvoiceItems[index]);
    }

    scope.setAmout = function (item) {
        //var amt = (parseFloat(item.Rate) * parseFloat(item.UnitsOrPieces));
        //var disAmt = amt * parseFloat(item.Discount) / 100;
        //if (isNaN(disAmt))
        //    disAmt = 0;
        //var amt1 = amt - disAmt;
        //var taxAmt = amt * parseFloat(item.Tax) / 100;
        //if (isNaN(taxAmt))
        //    taxAmt = 0;
        //var totalAmt = taxAmt + amt1;
        //item.TaxAmount = totalAmt;
        var amt = parseFloat(item.Rate) * parseFloat(item.UnitsOrPieces);
        if (isNaN(amt))
            amt = 0;
        var disAmt = 0;
        if (item.IsPercentage == true) {
            disAmt = amt * (parseFloat(item.Discount) / 100);
            if (isNaN(disAmt))
                disAmt = 0;
        }
        else {
            disAmt = parseFloat(item.Discount);
            if (isNaN(disAmt))
                disAmt = 0;
        }
        var afdis = amt - disAmt;
        var txamt = afdis * (parseFloat(item.Tax) / 100);
        if (isNaN(txamt))
            txamt = 0;
        var ttlamt = afdis + txamt;
        if (isNaN(ttlamt))
            ttlamt = 0;
        if (ttlamt < 0) {
            ttlamt = 0;
        }
        item.TaxAmount = ttlamt.toFixed(2);
        scope.setTotalAmt();
    }
    //var ovdisamt = 0;
    //if (scope.purchaseorder.IsPercentage == true) {
    //    ovdisamt = afdis * parseFloat(scope.purchaseorder.Discount / 100);
    //    if (isNaN(ovdisamt)) {
    //        ovdisamt = 0;
    //    }
    //    afdis = afdis - ovdisamt;
    //}
    //if (scope.purchaseorder.IsPercentage == false) {
    //    ovdisamt = parseFloat(scope.purchaseorder.Discount);
    //    if (isNaN(ovdisamt)) {
    //        ovdisamt = 0;
    //    }
    //    ttlamt = ttlamt - ovdisamt
    //}
    scope.setTotalAmt = function () {
        
        var subTotal = 0;
        scope.purchaseinvoice.PurchaseInvoiceItems.forEach(function (o) {
            if (!isNaN(o.TaxAmount) && o.TaxAmount != null) {
                subTotal += parseFloat(o.TaxAmount);
            }
        });

        var ddss = 0;
        if (scope.purchaseinvoice.IsPercentage == true) {
            scope.pItems = angular.copy(scope.purchaseinvoice.PurchaseInvoiceItems);
            scope.pItems.forEach(function (o) {
                var amt = parseFloat(o.Rate) * parseFloat(o.UnitsOrPieces);
                if (isNaN(amt))
                    amt = 0;
                var disAmt = 0;
                if (o.IsPercentage == true) {
                    disAmt = amt * (parseFloat(o.Discount) / 100);
                    if (isNaN(disAmt))
                        disAmt = 0;
                }
                else {
                    disAmt = parseFloat(o.Discount);
                    if (isNaN(disAmt))
                        disAmt = 0;
                }
                var afdis = amt - disAmt;
                var ovdisamt = 0;
                if (scope.purchaseinvoice.IsPercentage == true) {
                    ovdisamt = afdis * parseFloat(scope.purchaseinvoice.Discount / 100);
                    if (isNaN(ovdisamt)) {
                        ovdisamt = 0;
                    }
                    afdis = afdis - ovdisamt;
                }
                var txamt = afdis * (parseFloat(o.Tax) / 100);
                if (isNaN(txamt))
                    txamt = 0;
                var ttlamt = afdis + txamt;
                if (isNaN(ttlamt))
                    ttlamt = 0;
                if (ttlamt < 0) {
                    ttlamt = 0;
                }
                o.TaxAmount = ttlamt.toFixed(2);
                ddss = parseFloat(ddss) + parseFloat(ttlamt.toFixed(2));
            });
        }
        else {
            var mDisAmt = parseFloat(scope.purchaseinvoice.Discount);
            if (isNaN(mDisAmt)) {
                mDisAmt = 0;
            }
            if (isNaN(subTotal)) {
                subTotal = 0;
            }
            var ttrr = subTotal - mDisAmt;
            if (isNaN(ttrr) || ttrr < 0) {
                ttrr = 0;
            }
            ddss = ttrr.toFixed(2);
        }
        ddss = parseFloat(ddss);
        if (isNaN(ddss))
            ddss = 0;
        scope.purchaseinvoice.TotalAmount = ddss.toFixed(2);
        var ddssaamm = subTotal - ddss;
        if (isNaN(ddssaamm) || ddssaamm < 0)
            ddssaamm = 0;
        scope.purchaseinvoice.DiscountAmount = ddssaamm.toFixed(2);
    }

    scope.setAllIntTxtBox = function ($event) {
        if ($event.keyCode >= 48 && $event.keyCode <= 57 || $event.keyCode == 8 || $event.keyCode == 46) {

        }
        else {
            $event.preventDefault();
        }
    }
    scope.switchStatus = false;
    scope.setFromPO = function () {
        if (scope.switchStatus == false) {
            scope.clear();
            $(".sodiv").toggle(750);
        }
        else {
            $(".sodiv").toggle(750);
        }
    }
    scope.reset = function () {
        scope.purchaseinvoice = angular.copy(scope.purchaseinvoiceClone);
        angular.element(document).ready(function () {
            $('.selectpicker').selectpicker('refresh');
        });
    }
    scope.setAllData = function () {
        var data = scope.POs.find(o=>o.PO == scope.purchaseinvoice.PO);
        if (data != null) {
            scope.purchaseinvoice.Id = "0";
            scope.purchaseinvoice.CompanyCode = data.CompanyCode;
            scope.purchaseinvoice.SupplierCode = data.SupplierCode;
            scope.purchaseinvoice.PO = data.PO;
            scope.purchaseinvoice.PODate = new Date(data.PODate);
            scope.purchaseinvoice.ShippingAddress = data.ShippingAddress;
            scope.purchaseinvoice.SupplierAddress = data.SupplierAddress;
            scope.purchaseinvoice.DueDate = new Date(data.DueDate);
            scope.purchaseinvoice.TermsId = data.TermsId;
            scope.purchaseinvoice.Message = data.Message;
            scope.purchaseinvoice.Discount = data.Discount;
            scope.purchaseinvoice.TotalAmount = data.TotalAmount;
            scope.purchaseinvoice.Disabled = data.Disabled;
            scope.purchaseinvoice.CreatedBy = data.CreatedBy;
            scope.purchaseinvoice.CreatedOn = data.CreatedOn;
            scope.purchaseinvoice.ModifiedBy = data.ModifiedBy;
            scope.purchaseinvoice.ModifiedOn = data.ModifiedOn;
            scope.purchaseinvoice.IGST = data.IGST;
            scope.purchaseinvoice.CGST = data.CGST;
            scope.purchaseinvoice.SGST = data.SGST;
            scope.purchaseinvoice.Owner = data.Owner;
            //scope.purchaseinvoice.IsPercentage = data.IsPercentage;
            data.PurchaseOrderItems.forEach(function (o) {
                o.Date = new Date(o.Date);
                o.ItemTypeId = o.ItemTypeId + "";
                o.ProductId = o.ProductId + "";
            })
            scope.purchaseinvoice.PurchaseInvoiceItems = data.PurchaseOrderItems;
            scope.setProduct({ Code: scope.purchaseinvoice.CompanyCode });
            scope.purchaseinvoice.CompanyAddress = data.Company.Address;
            scope.purchaseinvoice.SupplierAddress = data.Supplier.Address;
            scope.purchaseinvoiceClone = angular.copy(scope.purchaseinvoice);
            angular.element(document).ready(function () {
                $('.productCS').selectpicker('refresh');
                $('.itemTypeCS').selectpicker('refresh');
                $('#purchaseinvoiceForm_Company').selectpicker('refresh');
                $('#purchaseinvoiceForm_Supplier').selectpicker('refresh');
                $('#purchaseinvoiceForm_TermsId').selectpicker('refresh');
            });
        }
        else {
            scope.purchaseinvoice = angular.copy(scope.purchaseinvoiceClone);
        }
    }
    scope.clear = function () {
        scope.purchaseinvoice = {
            Id: "0",
            InvoiceNo: "",
            InvoiceDate: new Date(),
            CompanyCode: "",
            SupplierCode: "",
            PO: "",
            PODate: new Date(),
            ShippingAddress: "",
            DueDate: new Date(),
            TermsId: "",
            Message: "",
            Discount: "",
            TotalAmount: "",
            IGST: "",
            CGST: "",
            SGST: "",
            Owner: "",
            IsPercentage: false,
            PurchaseInvoiceItems: []
        };
        angular.element(document).ready(function () {
            $('.selectpicker').selectpicker('refresh');
        });
        scope.purchaseinvoice.PurchaseInvoiceItems.push(angular.copy(scope.purchaseItem));
        scope.PO = {
            CompanyCode: "",
            SupplierCode: ""
        }
        scope.switchStatus = false;
        scope.products = [];
        scope.purchaseinvoice = angular.copy(scope.purchaseinvoiceClone);

    }
    scope.setFromPO();
    scope.save = function () {
        scope.purchaseinvoice.InvoiceDateSTR = scope.purchaseinvoice.InvoiceDate.toGMTString();
        scope.purchaseinvoice.DueDateSTR = scope.purchaseinvoice.DueDate.toGMTString();
        scope.purchaseinvoice.PODateSTR = scope.purchaseinvoice.PODate.toGMTString();
        scope.purchaseinvoice.PurchaseInvoiceItems.forEach(function (o) {
            o.DateSTR = o.Date.toGMTString();
        });
        if (scope.purchaseinvoice.Id == "0" || scope.purchaseinvoice.Id == "") {
            //Insert
            var res = ApiFactory.setData("PurchaseInvoice/Add", scope.purchaseinvoice);
            res.success(function (data, status) {
                if (data.code == "0") {
                    toasterService.success("Invoice was added");
                    if (scope.switchStatus == true) {
                        $(".sodiv").toggle(750);
                        //scope.switchStatus = false;
                    }
                    scope.clear();
                }
                else {
                    toasterService.error(data.error);
                }
            });
        }
        else {
            //Update
            var res = ApiFactory.setData("PurchaseInvoice/Update", scope.purchaseinvoice);
            res.success(function (data, status) {
                if (data.code == "0") {
                    toasterService.success("Invoice was updated");
                    scope.purchaseinvoice.InvoiceNo1 = data.datam.InvoiceNo;
                    scope.purchaseinvoiceClone = angular.copy(scope.purchaseinvoice);
                }
                else {
                    toasterService.error(data.error);
                }
            });
        }
    }

    scope.addRow = function () {
        scope.purchaseinvoice.PurchaseInvoiceItems.push(angular.copy(scope.purchaseItem));
        angular.element(document).ready(function () {
            $('.productCS').selectpicker('refresh');
            $('.itemTypeCS').selectpicker('refresh');
        });
    }
    scope.removeRow = function (index) {
        scope.purchaseinvoice.PurchaseInvoiceItems.splice(index, 1);
    }

    scope.setTerms = function () {
        var resTer = ApiFactory.getData("Terms/GetAll");
        resTer.success(function (data, status) {
            if (data.data != null) {
                scope.termss = data.data;
                angular.element(document).ready(function () {
                    $('#purchaseinvoiceForm_TermsId').selectpicker('refresh');
                });
            }
        });
    }
    scope.setTerms();
    scope.setNewTerms = function () {
        scope.purchaseinvoice.TermsId = "";
        angular.element(document).ready(function () {
            $('#purchaseinvoiceForm_TermsId').selectpicker('refresh');
        });
        scope.setTerms();
    }
    scope.showNewTerms = function () {
        if (scope.purchaseinvoice.TermsId == "_") {
            dialogModelService.showTemplate('views/terms/termsFormDialog.html', 'termsFormCtrl').result.then(function () {
                //$('#purchaseinvoiceForm_TermsId').selectpicker('val','');
            }, function () {
                scope.setNewTerms();
            });
        }
    }
    scope.setSuppliers = function () {
        var resSup = ApiFactory.getData("Supplier/GetAll");
        resSup.success(function (data, status) {
            if (data.data != null) {
                scope.suppliers = data.data;
                angular.element(document).ready(function () {
                    $('#purchaseinvoiceForm_POSupplier,#purchaseinvoiceForm_Supplier').selectpicker('refresh');
                });

            }
        });
    }
    scope.setSuppliers();
    scope.setNewSuppliers = function () {
        scope.purchaseinvoice.SupplierCode = "";
        angular.element(document).ready(function () {
            $('#purchaseinvoiceForm_SupplierCode').selectpicker('refresh');
        });
        scope.setSuppliers();
    }
    scope.showNewSupplier = function () {
        if (scope.purchaseinvoice.SupplierCode == "_") {
            dialogModelService.showTemplate('views/supplier/supplierFormDialog.html', 'supplierFormCtrl').result.then(function () {
                //$('#purchaseinvoiceForm_TermsId').selectpicker('val','');
            }, function () {
                scope.setNewSuppliers();
            });
        }
    }
    scope.setItemType = function () {
    var resItmTp = ApiFactory.getData("ItemType/GetAll");
    resItmTp.success(function (data, status) {
        if (data.data != null) {
            scope.itemTypes = data.data;
            angular.element(document).ready(function () {
                $('.itemTypeCS').selectpicker('refresh');
            });
        }
    });
    }
    scope.setItemType();
    scope.setNewItemType = function (index) {
        index.ItemTypeId = "";
        angular.element(document).ready(function () {
            $('#purchaseinvoiceForm_ItemTypeId').selectpicker('refresh');
        });
        scope.setItemType();
    }
    scope.showNewItemType = function (index) {
        if (index.ItemTypeId == "_") {
            dialogModelService.showTemplate('views/itemtype/itemtypeFormDialog.html', 'itemtypeFormCtrl').result.then(function () {
                //$('#purchaseinvoiceForm_TermsId').selectpicker('val','');
            }, function () {
                scope.setNewItemType(index);
            });
        }
    }

    scope.PIPDF = function () {
        var res = ApiFactory.getData("PurchaseInvoice/GetPIPDF", scope.purchaseinvoice);
        res.success(function (data, status) {
            toasterService.success(AppConstant.ViewUrl + data.datam.URLData);
            console.log(AppConstant.ViewUrl + data.datam.URLData);
        });
    }
    
    scope.setvalidform = function () {
        var v1 = true;
        if ((!scope.purchaseinvoiceForm.$valid) || (scope.purchaseinvoice.CompanyCode == '') || (scope.purchaseinvoice.SupplierCode == '') || (scope.purchaseinvoice.TermsId == '') || scope.purchaseinvoice.PurchaseInvoiceItems.some(o=>o.ProductId == "" || o.ItemTypeId == "")) {
            return true;
        }
        return false;
    }

    scope.showalert = function () {

        if (angular.equals(scope.purchaseinvoice, scope.purchaseinvoiceClone)) {
            state.go('app.purchaseinvoice');
        }
        else {
            if (scope.purchaseinvoiceClone != scope.purchaseinvoice) {
                dialogModelService.showBackTemplate().result.then(function () {
                    state.go('app.purchaseinvoice');
                });
            }
        }

    }
}])
;



