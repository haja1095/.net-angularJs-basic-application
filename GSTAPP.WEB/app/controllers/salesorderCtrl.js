'use strict';
angular.module('GSTApp')
.controller("salesorderCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', 'dialogModelService', 'toasterService', function (scope, state, timeout, window, ApiFactory, dialogModelService, toasterService) {
    scope.salesorders = [];
    function load() {
        var res = ApiFactory.getData("salesorder/GetAll");
        res.success(function (data, status) {
            scope.salesorders = data.data;
            angular.element(document).ready(function () {
                var table = $('#salesorderTbl').DataTable({
                    select: true,
                    "scrollX": true
                });
            });
        });
    }
    load();
    scope.delete = function (data) {
        var res = ApiFactory.setData("salesorder/Delete", data);
        res.success(function (data, status) {
            $('#salesorderTbl').DataTable().destroy();
            toasterService.success("Sales Order was deleted");
            load();
        });
    }
    scope.showDelete = function (data) {
        dialogModelService.show().result.then(function () {
            scope.delete(data);
        });
    }
}])
.controller("salesorderFormCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', '$stateParams', 'toasterService', 'dialogModelService', 'AppConstant', function (scope, state, timeout, window, ApiFactory, stateParams, toasterService, dialogModelService, AppConstant) {
    scope.open3 = function (item) {
        item.opened = true;
    };
    scope.open4 = function (item) {
        item.opened3 = true;
    };
    scope.open1 = function (item) {
        item.opened1 = true;
    };
    scope.open2 = function (item) {
        item.opened2 = true;
    };
    scope.salesorder = {

        Id: "",
        CompanyCode: "",
        CompanyAddress: "",
        SO: "",
        SO1: "",
        SODate: new Date(),
        ShippingDate: new Date(),
        ShippingAddress: "",
        CustomerCode: "",
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
        SalesOrderItems: []
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
    scope.salesorderClone = angular.copy(scope.salesorder);
    if (state.$current.name == "app.salesorderForm") {
        scope.salesorder.Id = stateParams.Code;
    }
    else {
        scope.salesorder.Id = stateParams.Code;
        scope.salesorder.Id = "0";
    }
    scope.salesorder.SalesOrderItems.push(angular.copy(scope.salesItem));
    scope.salesorderClone = angular.copy(scope.salesorder);
    scope.salesorder.Id = stateParams.Code;
    if (stateParams.Code != "0") {
        scope.salesorder.Id = stateParams.Code;
        var res = ApiFactory.setData("salesorder/GetSingle", scope.salesorder);
        res.success(function (data, status) {
            if (data.datam != null) {
                scope.salesorder.Id = data.datam.Id;
                scope.salesorder.CompanyCode = data.datam.CompanyCode;
                scope.salesorder.CompanyAddress = data.datam.Company.Address
                scope.salesorder.CustomerCode = data.datam.CustomerCode;
                scope.salesorder.SO = data.datam.SO;
                scope.salesorder.SO1 = data.datam.SO;
                scope.salesorder.SODate = new Date(data.datam.SODate);
                scope.salesorder.ShippingDate = new Date(data.datam.ShippingDate);
                scope.salesorder.ShippingAddress = data.datam.ShippingAddress;
                scope.salesorder.DueDate = new Date(data.datam.DueDate);
                scope.salesorder.TermsId = data.datam.TermsId;
                scope.salesorder.Message = data.datam.Message;
                scope.salesorder.Discount = data.datam.Discount;
                scope.salesorder.TotalAmount = data.datam.TotalAmount;
                scope.salesorder.Disabled = data.datam.Disabled;
                scope.salesorder.CreatedBy = data.datam.CreatedBy;
                scope.salesorder.CreatedOn = data.datam.CreatedOn;
                scope.salesorder.ModifiedBy = data.datam.ModifiedBy;
                scope.salesorder.ModifiedOn = data.datam.ModifiedOn;
                scope.salesorder.Owner = data.datam.Owner;
                if (data.datam.ShippingAddress.indexOf("<==>") >= 0) {
                    scope.salesorder.CustomerAddress = data.datam.ShippingAddress.split("<==>")[0];
                    scope.cusAddresstoShippingAddress = true;
                }
                else {
                    scope.salesorder.CustomerAddress = data.datam.Customer.Address;
                    scope.ArrAddrsttl = data.datam.Customer.ShippingAddressTitles.split("<==>");
                    scope.ArrAddrsval = data.datam.Customer.ShippingAddressValues.split("<==>");
                    var spAdd = scope.ArrAddrsval.findIndex(p=>p == scope.salesorder.ShippingAddress);
                    if (spAdd != null) {
                        scope.salesorder.ShippingAddressTitle = scope.ArrAddrsttl[spAdd];
                    }
                }

                data.datam.SalesOrderItems.forEach(function (o) {
                    o.Date = new Date(o.Date);
                    o.ItemTypeId = o.ItemTypeId + "";
                    o.ProductId = o.ProductId + "";
                });
                scope.salesorder.SalesOrderItems = data.datam.SalesOrderItems;
                scope.setProduct({ Code: scope.salesorder.CompanyCode });
                scope.salesorderClone = angular.copy(scope.salesorder);
                angular.element(document).ready(function () {
                    $('#salesorderForm_ShippingAddressSelect').selectpicker('refresh');
                    $('.productCS').selectpicker('refresh');
                    $('.itemTypeCS').selectpicker('refresh');
                    $('#salesorderForm_Company').selectpicker('refresh');
                    $('#salesorderForm_Customer').selectpicker('refresh');
                    $('#salesorderForm_TermsId').selectpicker('refresh');
                });
            }
            else {
                scope.salesorder = angular.copy(scope.salesorderClone);
            }
        });
    }
    angular.element(document).ready(function () {
        $('.selectpicker').selectpicker('refresh');
    });
    var resCom = ApiFactory.getData("Company/GetAll");
    resCom.success(function (data, status) {
        if (data.data != null) {
            scope.companys = data.data;
            angular.element(document).ready(function () {
                $('#salesorderForm_Company').selectpicker('refresh');
            });
        }
    });
    scope.setCustomer = function () {
        var resCus = ApiFactory.getData("Customer/GetAll");
        resCus.success(function (data, status) {
            if (data.data != null) {
                scope.customers = data.data;
                angular.element(document).ready(function () {
                    $('#salesorderForm_Customer').selectpicker('refresh');
                });
            }
        });

    }
    scope.setCustomer();
    scope.setTerms = function () {
        var resTer = ApiFactory.getData("Terms/GetAll");
        resTer.success(function (data, status) {
            if (data.data != null) {
                scope.termss = data.data;
                angular.element(document).ready(function () {
                    $('#salesorderForm_TermsId').selectpicker('refresh');
                });
            }
        });
    }
    scope.setTerms();
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
    scope.setComAddress = function () {
        var data = scope.companys.find(p=>p.Code == scope.salesorder.CompanyCode);
        if (data != null) {
            scope.salesorder.CompanyAddress = data.Address;
            scope.setProduct(data);
        }
    }

    scope.ArrAddrsttl = [];
    scope.ArrAddrsval = [];
    scope.setCusAddress = function () {
        scope.ArrAddrsttl = [];
        scope.ArrAddrsval = [];
        scope.salesorder.ShippingAddressTitle = "";
        scope.salesorder.ShippingAddress = "";
        var data = scope.customers.find(p=>p.Code == scope.salesorder.CustomerCode);
        if (data != null) {
            scope.salesorder.CustomerAddress = data.Address;
            scope.salesorder.ShippingAddress = "";
            if (data.ShippingAddressTitles != null && data.ShippingAddressTitles != "<==>") {
                scope.ArrAddrsttl = data.ShippingAddressTitles.split("<==>");
                scope.ArrAddrsval = data.ShippingAddressValues.split("<==>");
            }            
        }
        angular.element(document).ready(function () {
            $('#salesorderForm_ShippingAddressSelect').selectpicker('refresh');
        });
    }
    scope.setShippingAddress = function () {
        var data = scope.ArrAddrsttl.findIndex(p=>p == scope.salesorder.ShippingAddressTitle);
        if (data != null) {
            scope.salesorder.ShippingAddress = scope.ArrAddrsval[data];
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
            scope.salesorder.ShippingAddress = scope.salesorder.CustomerAddress + "<==>";
        }
        else {
            scope.salesorder.ShippingAddressTitle = "";
            scope.salesorder.ShippingAddress = "";
            scope.setCusAddress();
        }
    }
    scope.setRate = function (index) {
        var data = scope.products.find(p=>p.Id == scope.salesorder.SalesOrderItems[index].ProductId);
        var dataCom = scope.companys.find(p=>p.Code == scope.salesorder.CompanyCode);
        var dataCus = scope.customers.find(p=>p.Code == scope.salesorder.CustomerCode);
        if (data != null && dataCom != null && dataCus != null) {
            scope.salesorder.SalesOrderItems[index].Rate = data.Rate;
            if ((dataCom.State).toLowerCase() == (dataCus.State).toLowerCase()) {

                scope.salesorder.SalesOrderItems[index].Tax = data.CGST + data.SGST;
            }
            else {
                scope.salesorder.SalesOrderItems[index].Tax = data.IGST;

            }
            scope.setAmout(scope.salesorder.SalesOrderItems[index]);
        }
    }
    scope.setAmout = function (item) {
        //    var amt = (parseFloat(item.Rate) * parseFloat(item.UnitsOrPieces));
        //    var disAmt = amt * parseFloat(item.Discount) / 100;
        //    if (isNaN(disAmt))
        //        disAmt = 0;
        //    var amt1 = amt - disAmt;
        //    var taxAmt = amt * parseFloat(item.Tax) / 100;
        //    if (isNaN(taxAmt))
        //        taxAmt = 0;
        //    var totalAmt = taxAmt + amt1;
        //    item.TaxAmount = totalAmt;
        //    scope.setTotalAmt();
        //}
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
        //    var subTotal = 0;
        //    scope.salesorder.SalesOrderItems.forEach(function (o) {
        //        if (!isNaN(o.TaxAmount)) {
        //            subTotal += o.TaxAmount;
        //        }
        //    });
        //    var mDisAmt = subTotal * parseFloat(scope.salesorder.Discount) / 100;
        //    if (isNaN(mDisAmt))
        //        mDisAmt = 0;
        //    scope.salesorder.TotalAmount = subTotal - mDisAmt;
        //}
        var subTotal = 0;
        scope.salesorder.SalesOrderItems.forEach(function (o) {
            if (!isNaN(o.TaxAmount) && o.TaxAmount != null) {
                subTotal += parseFloat(o.TaxAmount);
            }
        });

        var ddss = 0;
        if (scope.salesorder.IsPercentage == true) {
            scope.pItems = angular.copy(scope.salesorder.SalesOrderItems);
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
                if (scope.salesorder.IsPercentage == true) {
                    ovdisamt = afdis * parseFloat(scope.salesorder.Discount / 100);
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
            var mDisAmt = parseFloat(scope.salesorder.Discount);
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
        scope.salesorder.TotalAmount = ddss.toFixed(2);
        var ddssaamm = subTotal - ddss;
        if (isNaN(ddssaamm) || ddssaamm < 0)
            ddssaamm = 0;
        scope.salesorder.DiscountAmount = ddssaamm.toFixed(2);
    }

    scope.setAllIntTxtBox = function ($event) {
        if ($event.keyCode >= 48 && $event.keyCode <= 57 || $event.keyCode == 8 || $event.keyCode == 46) {

        }
        else {
            $event.preventDefault();
        }
    }
    scope.reset = function () {
        //for set shippingaddresstitle
        scope.salesorder = angular.copy(scope.salesorderClone);

        scope.setCusAddress();
        scope.salesorder = angular.copy(scope.salesorderClone);
        angular.element(document).ready(function () {
            $('.selectpicker').selectpicker('refresh');
        });
    }
    scope.clear = function () {
        scope.salesorder = {

            Id: "0",
            CompanyCode: "",
            CompanyAddress: "",
            SO: "",
            SODate: new Date(),
            ShippingDate: new Date(),
            ShippingAddress: "",
            CustomerCode: "",
            DueDate: new Date(),
            TermsId: "",
            Message: "",
            Discount: "",
            IsPercentage: "",
            TotalAmount: "",
            Disabled: "",
            CreatedBy: "",
            CreatedOn: "",
            ModifiedBy: "",
            ModifiedOn: "",
            Owner: "",
            SalesOrderItems: []
        };
        scope.salesorder.SalesOrderItems.push(angular.copy(scope.salesItem));
        scope.products = [];
        scope.ArrAddrsttl = [];
        scope.ArrAddrsval = [];
        angular.element(document).ready(function () {
            $('.selectpicker').selectpicker('refresh');
        });
    }
    scope.save = function () {
        if (scope.cusAddresstoShippingAddress == true) {
            scope.salesorder.ShippingAddress = scope.salesorder.CustomerAddress + "<==>";
        }
        scope.salesorder.SODateSTR = scope.salesorder.SODate.toGMTString();
        scope.salesorder.DueDateSTR = scope.salesorder.DueDate.toGMTString();
        scope.salesorder.ShippingDateSTR = scope.salesorder.ShippingDate.toGMTString();
        scope.salesorder.SalesOrderItems.forEach(function (o) {
            o.DateSTR = o.Date.toGMTString();
        });
        if (scope.salesorder.Id == "0" || scope.salesorder.Id == "") {
            //Insert
            var res = ApiFactory.setData("SalesOrder/Add", scope.salesorder);
            res.success(function (data, status) {

                if (data.code == "0") {
                    toasterService.success("Sales order was added");
                    scope.clear();
                }
                else {
                    toasterService.error(data.error);
                }
            });
        }
        else {
            //Update
            var res = ApiFactory.setData("salesorder/Update", scope.salesorder);
            res.success(function (data, status) {
                if (data.code == "0") {
                    toasterService.success("SalesOrder was updated");
                    scope.salesorder.SO1 = scope.salesorder.SO;
                    scope.salesorderClone = angular.copy(scope.salesorder);
                }
                else {
                    toasterService.error(data.error);
                }
            });
        }
    }
    scope.addRow = function () {
        scope.salesorder.SalesOrderItems.push(angular.copy(scope.salesItem));
        angular.element(document).ready(function () {
            $('.productCS').selectpicker('refresh');
            $('.itemTypeCS').selectpicker('refresh');
        });
    }
    scope.removeRow = function (index) {
        scope.salesorder.SalesOrderItems.splice(index, 1);
    };

    scope.setNewCustomer = function () {
        scope.salesorder.CustomerCode = "";
        angular.element(document).ready(function () {
            $('#salesorderForm_Customer').selectpicker('refresh');
        });
        scope.setCustomer();
    }
    scope.showNewCus = function () {
        if (scope.salesorder.CustomerCode == "_") {
            dialogModelService.showTemplate('views/customer/customerFormDialog.html', 'customerFormCtrl').result.then(function () {

            }, function () {
                scope.setNewCustomer();
            });
        }
    }
    scope.setNewTerms = function () {
        scope.salesorder.TermsId = "";
        angular.element(document).ready(function () {
            $('#salesorderForm_TermsId').selectpicker('refresh');
        });
        scope.setTerms();
    }
    scope.showNewTer = function () {
        if (scope.salesorder.TermsId == "_") {
            dialogModelService.showTemplate('views/terms/termsFormDialog.html', 'termsFormCtrl').result.then(function () {

            }, function () {
                scope.setNewTerms();
            });
        }
    }
    scope.setNewItemType = function (index) {
        index.ItemTypeId = "";
        angular.element(document).ready(function () {
            $('#salesorderForm_ItemType').selectpicker('refresh');
        });
        scope.setItemType();
    }
    scope.showNewItemType = function (index) {
        if (index.ItemTypeId == "_") {
            dialogModelService.showTemplate('views/itemtype/itemtypeFormDialog.html', 'itemtypeFormCtrl').result.then(function () {

            }, function () {
                scope.setNewItemType(index);
            });
        }
    }

    scope.SOPDF = function () {
        var res = ApiFactory.getData("SalesOrder/GetSOPDF", scope.salesorder);
        res.success(function (data, status) {
            toasterService.success(AppConstant.ViewUrl + data.datam.URLData);
            console.log(AppConstant.ViewUrl + data.datam.URLData);
        });
    }
     scope.setvalidform = function () {
        var v1 = true;
        if ((!scope.salesorderForm.$valid) || (scope.salesorder.CompanyCode == '') || (scope.salesorder.CustomerCode == '') || (scope.salesorder.TermsId == '') || scope.salesorder.SalesOrderItems.some(o=>o.ProductId == "" || o.ItemTypeId == "")) {
            return true;
        }
        return false;
     }
     scope.showalert = function () {

         if (angular.equals(scope.salesorder, scope.salesorderClone)) {
             state.go('app.salesorder');
         }
         else {
             if (scope.salesorderClone != scope.salesorder) {
                 dialogModelService.showBackTemplate().result.then(function () {
                     state.go('app.salesorder');
                 });
             }
         }

     }
}])
;
