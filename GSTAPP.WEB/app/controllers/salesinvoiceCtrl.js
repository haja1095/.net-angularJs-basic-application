'use strict';

angular.module('GSTApp')
.controller("salesinvoiceCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', 'dialogModelService', 'toasterService', function (scope, state, timeout, window, ApiFactory, dialogModelService, toasterService) {
    scope.salesinvoices = [];

    function load() {
        var res = ApiFactory.getData("SalesInvoice/GetAll");
        res.success(function (data, status) {
            scope.salesinvoices = data.data;
            angular.element(document).ready(function () {
                var table = $('#salesinvoiceTbl').DataTable({
                    select: true,
                    "scrollX": true
                });
            });
        });
    }
    load();
    scope.delete = function (data) {
        var res = ApiFactory.setData("SalesInvoice/Delete", data);
        res.success(function (data, status) {
            $('#salesinvoiceTbl').DataTable().destroy();
            toasterService.success("Sales Invoice was deleted");
            load();
        });
    }
    scope.showDelete = function (data) {
        dialogModelService.show().result.then(function () {
            scope.delete(data);
        });
    }
}])
.controller("salesinvoiceFormCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', '$stateParams', 'dialogModelService','toasterService','AppConstant', function (scope, state, timeout, window, ApiFactory, stateParams, dialogModelService,toasterService,AppConstant) {
    scope.open3 = function (item) {
        item.opened = true;
    };
    scope.open1 = function (item) {
        item.opened1 = true;
    };
    scope.open2 = function (item) {
        item.opened2 = true;
    };
    scope.SO = {
        CompanyCode: "",
        CustomerCode: ""
    }
    scope.salesinvoice = {

        Id: "",
        InvoiceNo: "",
        InvoiceDate: new Date(),
        CompanyCode: "",
        CustomerCode: "",
        SO: "",
        SODate: new Date(),
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
        Owner: "",
        DiscountAmount: "",
        SalesInvoiceItems: []
    };
    scope.salesItem = {
        Id: "",
        SalesId: "",
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
    scope.salesinvoice.SalesInvoiceItems.push(angular.copy(scope.salesItem));
    if (state.$current.name == "app.salesinvoiceForm") {
        scope.salesinvoice.Id = stateParams.Code;
    }
    else {
        scope.salesinvoice.Id = "0";
    }
    scope.salesinvoiceClone = angular.copy(scope.salesinvoice);
    if (scope.salesinvoice.Id != "0") {
        var res = ApiFactory.setData("SalesInvoice/GetSingle", scope.salesinvoice);
        res.success(function (data, status) {
            if (data.datam != null) {
                scope.salesinvoice.Id = data.datam.Id;
                scope.salesinvoice.InvoiceNo = data.datam.InvoiceNo;
                scope.salesinvoice.InvoiceNo1 = data.datam.InvoiceNo;
                scope.salesinvoice.InvoiceDate = new Date(data.datam.InvoiceDate);
                scope.salesinvoice.CompanyCode = data.datam.CompanyCode;
                scope.salesinvoice.CustomerCode = data.datam.CustomerCode;
                scope.salesinvoice.SO = data.datam.SO;
                scope.salesinvoice.SODate = new Date(data.datam.SODate);
                scope.salesinvoice.ShippingAddress = data.datam.ShippingAddress;
                scope.salesinvoice.DueDate = new Date(data.datam.DueDate);
                scope.salesinvoice.TermsId = data.datam.TermsId;
                scope.salesinvoice.Message = data.datam.Message;
                scope.salesinvoice.Discount = data.datam.Discount;
                scope.salesinvoice.DiscountAmount = data.datam.DiscountAmount;
                scope.salesinvoice.IsPercentage = data.datam.IsPercentage;
                scope.salesinvoice.TotalAmount = data.datam.TotalAmount;
                scope.salesinvoice.Disabled = data.datam.Disabled;
                scope.salesinvoice.CreatedBy = data.datam.CreatedBy;
                scope.salesinvoice.CreatedOn = data.datam.CreatedOn;
                scope.salesinvoice.ModifiedBy = data.datam.ModifiedBy;
                scope.salesinvoice.ModifiedOn = data.datam.ModifiedOn;
                scope.salesinvoice.Owner = data.datam.Owner;
                if (data.datam.ShippingAddress.indexOf("<==>")>=0)
                {
                    scope.salesinvoice.CustomerAddress = data.datam.ShippingAddress.split("<==>")[0];
                    scope.cusAddresstoShippingAddress = true;
                }
                else {
                    scope.salesinvoice.CustomerAddress = data.datam.Customer.Address;
                    scope.ArrAddrsttl = data.datam.Customer.ShippingAddressTitles.split("<==>");
                    scope.ArrAddrsval = data.datam.Customer.ShippingAddressValues.split("<==>");
                    var spAdd = scope.ArrAddrsval.findIndex(p=>p == scope.salesinvoice.ShippingAddress);
                    if (spAdd != null) {
                        scope.salesinvoice.ShippingAddressTitle = scope.ArrAddrsttl[spAdd];
                    }
                }
                data.datam.SalesInvoiceItems.forEach(function (o) {
                    o.Date = new Date(o.Date);
                    o.ItemTypeId = o.ItemTypeId + "";
                    o.ProductId = o.ProductId + "";
                })
                scope.salesinvoice.SalesInvoiceItems = data.datam.SalesInvoiceItems;
                scope.setProduct({ Code: scope.salesinvoice.CompanyCode });
                scope.salesinvoice.CompanyAddress = data.datam.Company.Address;
                scope.salesinvoiceClone = angular.copy(scope.salesinvoice);
                angular.element(document).ready(function () {
                    $('.productCS').selectpicker('refresh');
                    $('.itemTypeCS').selectpicker('refresh');
                    $('#salesinvoiceForm_ShippingAddressSelect').selectpicker('refresh');
                    $('#salesinvoiceForm_Company').selectpicker('refresh');
                    $('#salesinvoiceForm_Customer').selectpicker('refresh');
                    $('#salesinvoiceForm_TermsId').selectpicker('refresh');
                });
            }
            else {
                scope.salesinvoice = angular.copy(scope.salesinvoiceClone);
            }
        });
    }
    scope.setSOs = function () {
        if (scope.SO.CompanyCode.length > 0 && scope.SO.CustomerCode.length > 0) {
            var resSO = ApiFactory.getData("SalesOrder/GetAllfromCOMCUS", scope.SO);
            resSO.success(function (data, status) {
                if (data.data != null) {
                    scope.SOs = data.data;
                    angular.element(document).ready(function () {
                        $('#salesinvoiceForm_SODtl').selectpicker('refresh');
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
                $('#salesinvoiceForm_SOCompany,#salesinvoiceForm_Company').selectpicker('refresh');
            });
        }
    });
    var resTer = ApiFactory.getData("Terms/GetAll");
    resTer.success(function (data, status) {
        if (data.data != null) {
            scope.termss = data.data;
            angular.element(document).ready(function () {
                $('#salesinvoiceForm_TermsId').selectpicker('refresh');
            });
        }
    });
    
    scope.setComAddress = function () {
        var data = scope.companys.find(p=>p.Code == scope.salesinvoice.CompanyCode);
        if (data != null) {
            scope.salesinvoice.CompanyAddress = data.Address;
            scope.setProduct(data);
        }
    }
    scope.ArrAddrsttl = [];
    scope.ArrAddrsval = [];
    scope.setCusAddress = function () {
        var data = scope.customers.find(p=>p.Code == scope.salesinvoice.CustomerCode);
        if (data != null) {
            scope.salesinvoice.CustomerAddress = data.Address;
            scope.salesinvoice.ShippingAddress = "";
            scope.ArrAddrsttl = data.ShippingAddressTitles.split("<==>");
            scope.ArrAddrsval = data.ShippingAddressValues.split("<==>");
        }
        angular.element(document).ready(function () {
            $('#salesinvoiceForm_ShippingAddressSelect').selectpicker('refresh');
        });
    }
    scope.setShippingAddress = function () {
        var data = scope.ArrAddrsttl.findIndex(p=>p == scope.salesinvoice.ShippingAddressTitle);
        if (data != null) {
            scope.salesinvoice.ShippingAddress = scope.ArrAddrsval[data];
        }
    }
    scope.cusAddresstoShippingAddress = false;
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
    scope.setShiAppDet = function () {
        if (scope.cusAddresstoShippingAddress == true) {
            scope.salesinvoice.ShippingAddress = scope.salesinvoice.CustomerAddress + "<==>";
        }
        else {
            scope.salesinvoice.ShippingAddressTitle = "";
            scope.salesinvoice.ShippingAddress = "";
        }
    }
    scope.setRate = function (index) {
        var data = scope.products.find(p=>p.Id == scope.salesinvoice.SalesInvoiceItems[index].ProductId);
        var dataCom = scope.companys.find(p=>p.Code == scope.salesinvoice.CompanyCode);
        var dataCus = scope.customers.find(p=>p.Code == scope.salesinvoice.CustomerCode);
        if (data != null && dataCom != null && dataCus != null) {
            scope.salesinvoice.SalesInvoiceItems[index].Rate = data.Rate;
            if ((dataCom.State).toLowerCase() == (dataCus.State).toLowerCase()) {

                scope.salesinvoice.SalesInvoiceItems[index].Tax = data.CGST + data.SGST;
            }
            else {
                scope.salesinvoice.SalesInvoiceItems[index].Tax = data.IGST;

            }
            scope.setAmout(scope.salesinvoice.SalesInvoiceItems[index]);
        }
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
     
    scope.setTotalAmt = function () {

        var subTotal = 0;
        scope.salesinvoice.SalesInvoiceItems.forEach(function (o) {
            if (!isNaN(o.TaxAmount) && o.TaxAmount != null) {
                subTotal += parseFloat(o.TaxAmount);
            }
        });

        var ddss = 0;
        if (scope.salesinvoice.IsPercentage == true) {
            scope.pItems = angular.copy(scope.salesinvoice.SalesInvoiceItems);
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
                if (scope.salesinvoice.IsPercentage == true) {
                    ovdisamt = afdis * parseFloat(scope.salesinvoice.Discount / 100);
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
            var mDisAmt = parseFloat(scope.salesinvoice.Discount);
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
        scope.salesinvoice.TotalAmount = ddss.toFixed(2);
        var ddssaamm = subTotal - ddss;
        if (isNaN(ddssaamm) || ddssaamm < 0)
            ddssaamm = 0;
        scope.salesinvoice.DiscountAmount = ddssaamm.toFixed(2);
    }
    scope.switchStatus = false;
    scope.setFromSO = function () {
        if (scope.switchStatus == false) {
            scope.clear();
            $(".sodiv").toggle(750);
        }
        else {
            $(".sodiv").toggle(750);
        }
    }
    scope.setAllData = function () {
        var data = scope.SOs.find(o=>o.SO == scope.salesinvoice.SO);
        if (data != null) {
            scope.salesinvoice.Id = "0";
            //scope.salesinvoice.InvoiceNo = data.InvoiceNo;
            //scope.salesinvoice.InvoiceDate = new Date(data.InvoiceDate);
            scope.salesinvoice.CompanyCode = data.CompanyCode;
            scope.salesinvoice.CustomerCode = data.CustomerCode;
            scope.salesinvoice.SO = data.SO;
            scope.salesinvoice.SODate = new Date(data.SODate);
            scope.salesinvoice.ShippingAddress = data.ShippingAddress;
            scope.salesinvoice.DueDate = new Date(data.DueDate);
            scope.salesinvoice.TermsId = data.TermsId;
            scope.salesinvoice.Message = data.Message;
            scope.salesinvoice.Discount = data.Discount;
            scope.salesinvoice.TotalAmount = data.TotalAmount;
            scope.salesinvoice.Disabled = data.Disabled;
            scope.salesinvoice.CreatedBy = data.CreatedBy;
            scope.salesinvoice.CreatedOn = data.CreatedOn;
            scope.salesinvoice.ModifiedBy = data.ModifiedBy;
            scope.salesinvoice.ModifiedOn = data.ModifiedOn;
            scope.salesinvoice.Owner = data.Owner;
            if (data.ShippingAddress.indexOf("<==>") >= 0) {
                scope.salesinvoice.CustomerAddress = data.ShippingAddress.split("<==>")[0];
                scope.cusAddresstoShippingAddress = true;
            }
            else {
                scope.salesinvoice.CustomerAddress = data.Customer.Address;
                scope.ArrAddrsttl = data.Customer.ShippingAddressTitles.split("<==>");
                scope.ArrAddrsval = data.Customer.ShippingAddressValues.split("<==>");
                var spAdd = scope.ArrAddrsval.findIndex(p=>p == scope.salesinvoice.ShippingAddress);
                if (spAdd != null) {
                    scope.salesinvoice.ShippingAddressTitle = scope.ArrAddrsttl[spAdd];
                }
            }

            data.SalesOrderItems.forEach(function (o) {
                o.Date = new Date(o.Date);
                o.ItemTypeId = o.ItemTypeId + "";
                o.ProductId = o.ProductId + "";
            })
            scope.salesinvoice.SalesInvoiceItems = data.SalesOrderItems;
            scope.setProduct({ Code: scope.salesinvoice.CompanyCode });
            scope.salesinvoice.CompanyAddress = data.Company.Address;
            scope.salesinvoiceClone = angular.copy(scope.salesinvoice);
            angular.element(document).ready(function () {
                $('.productCS').selectpicker('refresh');
                $('.itemTypeCS').selectpicker('refresh');
                $('#salesinvoiceForm_ShippingAddressSelect').selectpicker('refresh');
                $('#salesinvoiceForm_Company').selectpicker('refresh');
                $('#salesinvoiceForm_Customer').selectpicker('refresh');
                $('#salesinvoiceForm_TermsId').selectpicker('refresh');
            });
        }
    }
    scope.setAllIntTxtBox = function ($event) {
        if ($event.keyCode >= 48 && $event.keyCode <= 57 || $event.keyCode == 8 || $event.keyCode == 46) {

        }
        else {
            $event.preventDefault();
        }
    }

    scope.reset = function () {
        scope.salesinvoice = angular.copy(scope.salesinvoiceClone);
        angular.element(document).ready(function () {
            $('.selectpicker').selectpicker('refresh');
        });
    }

    scope.clear = function () {
        scope.salesinvoice = {

            Id: "0",
            InvoiceNo: "",
            InvoiceDate: new Date(),
            CompanyCode: "",
            CustomerCode: "",
            SO: "",
            SODate: new Date(),
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
            Owner: "",
            SalesInvoiceItems: []
        };
        scope.salesinvoice.SalesInvoiceItems.push(angular.copy(scope.salesItem));
        scope.SO = {
            CompanyCode: "",
            CustomerCode: ""
        }
        scope.switchStatus = false;
        scope.products = [];
        scope.ArrAddrsttl = [];
        scope.ArrAddrsval = [];
        angular.element(document).ready(function () {
            $('.selectpicker').selectpicker('refresh');
        });

        scope.salesinvoiceClone = angular.copy(scope.salesinvoice);
    }
    scope.setFromSO();
    // scope.switchStatus = false;
    scope.save = function () {
        scope.salesinvoice.InvoiceDateSTR = scope.salesinvoice.InvoiceDate.toGMTString();
        scope.salesinvoice.DueDateSTR = scope.salesinvoice.DueDate.toGMTString();
        scope.salesinvoice.SalesInvoiceItems.forEach(function (o) {
            o.DateSTR = o.Date.toGMTString();
        });
        if (scope.salesinvoice.Id == "0" || scope.salesinvoice.Id == "") {
            //Insert
            var res = ApiFactory.setData("SalesInvoice/Add", scope.salesinvoice);
            res.success(function (data, status) {
                if (data.code == "0") {
                    toasterService.success("Invoice was added");
                    if (scope.switchStatus == true) {
                        $(".sodiv").toggle(750);
                        scope.switchStatus = false;
                    }
                    scope.clear();
                    scope.salesinvoiceClone = angular.copy(scope.salesinvoice);
                    
                }
                else {
                    toasterService.error(data.error);
                }
            });
        }
        else {
            //Update
           
            var res = ApiFactory.setData("SalesInvoice/Update", scope.salesinvoice);
            res.success(function (data, status) {
                if (data.code=="0") {
                    toasterService.success("Invoice was updated");
                    scope.salesinvoice.InvoiceNo1 = data.datam.InvoiceNo;
                    scope.salesinvoiceClone = angular.copy(scope.salesinvoice);
                }
                else {
                    toasterService.error(data.error);

                }
            });
        }
    }
    scope.addRow = function () {
        scope.salesinvoice.SalesInvoiceItems.push(angular.copy(scope.salesItem));
        angular.element(document).ready(function () {
            $('.productCS').selectpicker('refresh');
            $('.itemTypeCS').selectpicker('refresh');
        });
    }
    scope.removeRow = function (index) {
        scope.salesinvoice.SalesInvoiceItems.splice(index, 1);
    }

    scope.setCustomer = function () {
    var resCus = ApiFactory.getData("Customer/GetAll");
    resCus.success(function (data, status) {
        if (data.data != null) {
            scope.customers = data.data;
            angular.element(document).ready(function () {
                $('#salesinvoiceForm_SOCustomer,#salesinvoiceForm_Customer').selectpicker('refresh');
            });
        }
    });
}

    scope.setCustomer();
    scope.setNewCustomer = function () {
        scope.salesinvoiceinvoice.CustomerCode = "";
        angular.element(document).ready(function () {
            $('#salesinvoiceForm_CustomerCode').selectpicker('refresh');
        });
        scope.setSuppliers();
    }
    scope.showNewCustomer = function () {
        if (scope.salesinvoice.CustomerCode == "_") {
            dialogModelService.showTemplate('views/customer/customerFormDialog.html', 'customerFormCtrl').result.then(function () {
                //$('#purchaseinvoiceForm_TermsId').selectpicker('val','');
            }, function () {
                scope.setNewCustomer();
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
            $('#salesinvoiceForm_ItemTypeId').selectpicker('refresh');
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
    scope.setTerms = function () {
        var resTer = ApiFactory.getData("Terms/GetAll");
        resTer.success(function (data, status) {
            if (data.data != null) {
                scope.termss = data.data;
                angular.element(document).ready(function () {
                    $('#salesinvoiceForm_TermsId').selectpicker('refresh');
                });
            }
        });
    }
    scope.setTerms();
    scope.setNewTerms = function () {
        scope.salesinvoice.TermsId = "";
        angular.element(document).ready(function () {
            $('#salesinvoiceForm_TermsId').selectpicker('refresh');
        });
        scope.setTerms();
    }
    scope.showNewTerms = function () {
        if (scope.salesinvoice.TermsId == "_") {
            dialogModelService.showTemplate('views/terms/termsFormDialog.html', 'termsFormCtrl').result.then(function () {
                //$('#purchaseinvoiceForm_TermsId').selectpicker('val','');
            }, function () {
                scope.setNewTerms();
            });
        }
    }

    scope.SIPDF = function () {
        var res = ApiFactory.getData("SalesInvoice/GetSIPDF", scope.salesinvoice);
        res.success(function (data, status) {
            toasterService.success(AppConstant.ViewUrl + data.datam.URLData);
            console.log(AppConstant.ViewUrl + data.datam.URLData);
        });
    }

    scope.showalert = function () {

        if (angular.equals(scope.salesinvoice, scope.salesinvoiceClone)) {
            state.go('app.salesinvoice');
        }
        else {
            if (scope.salesinvoiceClone != scope.salesinvoice) {
                dialogModelService.showBackTemplate().result.then(function () {
                    state.go('app.salesinvoice');
                });
            }
        }

    }

}])
;



