'use strict';

angular.module('GSTApp')
.controller("purchaseorderCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', 'dialogModelService', 'toasterService', function (scope, state, timeout, window, ApiFactory, dialogModelService, toasterService) {
    scope.purchaseorders = [];

    function load() {
        var res = ApiFactory.getData("PurchaseOrder/GetAll");
        res.success(function (data, status) {
            scope.purchaseorders = data.data;
            angular.element(document).ready(function () {
                var table = $('#purchaseorderTbl').DataTable({
                    select: true,
                    "scrollX": true
                });
            });
        });

    }
    load();

    scope.delete = function (data) {
        var res = ApiFactory.setData("PurchaseOrder/Delete", data);
        res.success(function (data, status) {
            $('#purchaseorderTbl').DataTable().destroy();
            toasterService.success("Purchase Order was deleted");
            load();
        });
    }
    scope.showDelete = function (data) {
        dialogModelService.show().result.then(function () {
            scope.delete(data);
        });
    }

}])

.controller("purchaseorderFormCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', '$stateParams', 'dialogModelService', 'toasterService', 'AppConstant', function (scope, state, timeout, window, ApiFactory, stateParams, dialogModelService, toasterService, AppConstant) {

    scope.open4 = function (item) {
        item.opened = true;
    };
    scope.open1 = function (item) {
        item.opened1 = true;
    };
    scope.open2 = function (item) {
        item.opened2 = true;
    };
    scope.open3 = function (item) {
        item.opened3 = true;
    };
    scope.purchaseorder = {

        Id: "",
        CompanyCode: "",
        SupplierCode: "",
        PO: "",
        PO1: "",
        PODate: new Date(),
        ShippingAddress: "",
        TermsId: "",
        TotalAmount: "",
        Discount: "",
        IsPercentage: false,
        DueDate: new Date(),
        Message: "",
        InvoiceState: "",
        ShippingDate: new Date(),
        Disabled: "",
        DiscountAmount: "",
        PurchaseOrderItems: []

    };
    scope.purchaseOrderItem = {
        Id: "",
        PurchaseOrderId: "",
        ProductId: "",
        WeightorLength: "",
        UnitsOrPieces: "",
        ItemTypeId: "",
        Rate: "",
        Tax: "",
        TaxAmount: "",
        Date: new Date(),
        IsPercentage: false,
        Discount: ""
    }
    scope.purchaseorder.PurchaseOrderItems.push(angular.copy(scope.purchaseOrderItem));
    if (state.$current.name == "app.purchaseorderForm") {
        scope.purchaseorder.Id = stateParams.Code;
    }
    else {
        scope.purchaseorder.Id = "0";
    }
    scope.purchaseorderClone = angular.copy(scope.purchaseorder);

    if (scope.purchaseorder.Id != "0") {
        var res = ApiFactory.setData("PurchaseOrder/GetSingle", scope.purchaseorder);
        res.success(function (data, status) {
            if (data.datam != null) {
                scope.purchaseorder.Id = data.datam.Id;
                scope.purchaseorder.CompanyCode = data.datam.CompanyCode;
                scope.purchaseorder.SupplierCode = data.datam.SupplierCode;
                scope.purchaseorder.PO = data.datam.PO;
                scope.purchaseorder.PO1 = data.datam.PO;
                scope.purchaseorder.PODate = new Date(data.datam.PODate);
                scope.purchaseorder.ShippingAddress = data.datam.ShippingAddress;
                scope.purchaseorder.SupplierAddress = data.datam.SupplierAddress;
                scope.purchaseorder.DueDate = new Date(data.datam.DueDate);
                scope.purchaseorder.ShippingDate = new Date(data.datam.ShippingDate);
                scope.purchaseorder.TermsId = data.datam.TermsId;
                scope.purchaseorder.Message = data.datam.Message;
                scope.purchaseorder.Discount = data.datam.Discount;
                scope.purchaseorder.DiscountAmount = data.datam.DiscountAmount;
                scope.purchaseorder.IsPercentage = data.datam.IsPercentage;
                scope.purchaseorder.TotalAmount = data.datam.TotalAmount;
                scope.purchaseorder.Disabled = data.datam.Disabled;
                scope.purchaseorder.CreatedBy = data.datam.CreatedBy;
                scope.purchaseorder.CreatedOn = data.datam.CreatedOn;
                scope.purchaseorder.ModifiedBy = data.datam.ModifiedBy;
                scope.purchaseorder.ModifiedOn = data.datam.ModifiedOn;
                scope.purchaseorder.Owner = data.datam.Owner;

                data.datam.PurchaseOrderItems.forEach(function (o) {
                    o.Date = new Date(o.Date);
                    o.ItemTypeId = o.ItemTypeId + "";
                    o.ProductId = o.ProductId + "";
                })
                scope.purchaseorder.PurchaseOrderItems = data.datam.PurchaseOrderItems;
                scope.setProduct({ Code: scope.purchaseorder.CompanyCode });
                scope.purchaseorder.CompanyAddress = data.datam.Company.Address;
                scope.purchaseorder.SupplierAddress = data.datam.Supplier.Address;
                scope.purchaseorderClone = angular.copy(scope.purchaseorder);
                angular.element(document).ready(function () {
                    $('.productCS').selectpicker('refresh');
                    $('.itemTypeCS').selectpicker('refresh');
                    $('#purchaseorderForm_Supplier').selectpicker('refresh');
                    $('#purchaseorderForm_Company').selectpicker('refresh');
                    $('#purchaseorderForm_Terms').selectpicker('refresh');
                });
            }
            else {
                scope.purchaseorder = angular.copy(scope.purchaseorderClone);
            }
        });
    }

    angular.element(document).ready(function () {
        $('.selectpicker').selectpicker('refresh');
    });
    //select
    var resCom = ApiFactory.getData("Company/GetAll");
    resCom.success(function (data, status) {
        if (data.data != null) {
            scope.companys = data.data;
            angular.element(document).ready(function () {
                $('#purchaseorderForm_Company').selectpicker('refresh');
            });
        }
    });



    var resIte = ApiFactory.getData("ItemType/GetAll");
    resIte.success(function (data, status) {
        if (data.data != null) {
            scope.itemtypes = data.data;
            angular.element(document).ready(function () {
                $('.itemTypeCS').selectpicker('refresh');
            });
        }
    });

    //mapping
    scope.setComAddress = function () {
        var data = scope.companys.find(p=>p.Code == scope.purchaseorder.CompanyCode);
        if (data != null) {
            scope.purchaseorder.ShippingAddress = data.Address;
            scope.setProduct(data);
        }
    }

    scope.setSupAddress = function () {
        var data = scope.suppliers.find(p=>p.Code == scope.purchaseorder.SupplierCode);
        if (data != null) {
            scope.purchaseorder.SupplierAddress = data.Address;
        }
    }

    scope.setProduct = function (data) {
        var resIte = ApiFactory.getData("Product/GetAllUsinCompany", data)
        resIte.success(function (data, status) {
            if (data.data != null) {
                scope.products = data.data;
                angular.element(document).ready(function () {
                    $('.productCS').selectpicker('refresh');
                });
            }
        });
    }

    scope.reset = function () {
        scope.purchaseorder = angular.copy(scope.purchaseorderClone);
        angular.element(document).ready(function () {
            $('.selectpicker').selectpicker('refresh');
        });
    }
    scope.clear = function () {
        scope.purchaseorder = {

            Id: "0",
            PO: "",
            PODate: new Date(),
            ShippingAddress: "",
            ShippingDate: new Date(),
            SupplierCode: "",
            SupplierAddress: "",
            CompanyCode: "",
            Disabled: "",
            IsPercentage: false,
            Message: "",
            TermsId: "",
            DueDate: new Date(),
            TotalAmount: "",
            InvoiceState: "",
            DiscountAmount: "",
            PurchaseOrderItems: []
        };
        scope.purchaseorder.PurchaseOrderItems.push(angular.copy(scope.purchaseOrderItem));
        angular.element(document).ready(function () {
            $('.selectpicker').selectpicker('refresh');
        });
    }
    scope.save = function () {
        scope.purchaseorder.DueDateSTR = scope.purchaseorder.DueDate.toGMTString();
        scope.purchaseorder.PODateSTR = scope.purchaseorder.PODate.toGMTString();
        scope.purchaseorder.ShippingDateSTR = scope.purchaseorder.ShippingDate.toGMTString();


        scope.purchaseorder.PurchaseOrderItems.forEach(function (o) {
            o.DateSTR = o.Date.toGMTString();

        });
        if (scope.purchaseorder.Id == "0" || scope.purchaseorder.Id == "") {
            //Insert
            var res = ApiFactory.setData("PurchaseOrder/Add", scope.purchaseorder);
            res.success(function (data, status) {
                if (data.code == "0") {
                    toasterService.success("Purchase order was added");
                    scope.clear();
                    scope.purchaseorderClone = angular.copy(scope.purchaseorder);
                }
                else {
                    toasterService.error(data.error);

                }
            });
        }
        else {
            //Update
            var res = ApiFactory.setData("PurchaseOrder/Update", scope.purchaseorder);
            res.success(function (data, status) {
                if (data.code == "0") {
                    toasterService.success("Purchase order was updated");
                    scope.purchaseorder.PO1 = scope.purchaseorder.PO;
                    scope.purchaseorderClone = angular.copy(scope.purchaseorder);

                    //scope.clear();
                }
                else {
                    toasterService.error(data.error);

                }
            });

        }

    }

    scope.createrow = function () {
        scope.purchaseorder.PurchaseOrderItems.push(angular.copy(scope.purchaseOrderItem))
        angular.element(document).ready(function () {
            $('.productCS').selectpicker('refresh');
            $('.itemTypeCS').selectpicker('refresh');
        });
    }

    scope.delrow = function (index) {
        scope.purchaseorder.PurchaseOrderItems.splice(index, 1)
    }

    scope.setrate = function (index) {
        var data = scope.products.find(p=>p.Id == scope.purchaseorder.PurchaseOrderItems[index].ProductId);
        var dataCom = scope.companys.find(p=>p.Code == scope.purchaseorder.CompanyCode);
        var dataSup = scope.suppliers.find(p=>p.Code == scope.purchaseorder.SupplierCode);

        if (data != null && dataCom != null && dataSup != null) {
            scope.purchaseorder.PurchaseOrderItems[index].Rate = data.Rate;

            if ((dataCom.State).toLowerCase() == (dataSup.State).toLowerCase()) {

                scope.purchaseorder.PurchaseOrderItems[index].Tax = data.CGST + data.SGST;
            }
            else {
                scope.purchaseorder.PurchaseOrderItems[index].Tax = data.IGST;

            }
            scope.setAmout(scope.purchaseorder.PurchaseOrderItems[index]);
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
        scope.purchaseorder.PurchaseOrderItems.forEach(function (o) {
            if (!isNaN(o.TaxAmount) && o.TaxAmount != null) {
                subTotal += parseFloat(o.TaxAmount);
            }
        });

        var ddss = 0;
        if (scope.purchaseorder.IsPercentage == true) {
            scope.pItems = angular.copy(scope.purchaseorder.PurchaseOrderItems);
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
                if (scope.purchaseorder.IsPercentage == true) {
                    ovdisamt = afdis * parseFloat(scope.purchaseorder.Discount / 100);
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
            var mDisAmt = parseFloat(scope.purchaseorder.Discount);
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
        scope.purchaseorder.TotalAmount = ddss.toFixed(2);
        var ddssaamm = subTotal - ddss;
        if (isNaN(ddssaamm) || ddssaamm < 0)
            ddssaamm = 0;
        scope.purchaseorder.DiscountAmount = ddssaamm.toFixed(2);
    }

    scope.setAllIntTxtBox = function ($event) {
        if ($event.keyCode >= 48 && $event.keyCode <= 57 || $event.keyCode == 8 || $event.keyCode == 46) {

        }
        else {
            $event.preventDefault();
        }
    }
    scope.cancelDialog = function () {
        $('.modal').click();
    };

    scope.setSupplier = function () {
        var resSup = ApiFactory.getData("Supplier/GetAll");
        resSup.success(function (data, status) {
            if (data.data != null) {
                scope.suppliers = data.data;
                angular.element(document).ready(function () {
                    $('#purchaseorderForm_Supplier').selectpicker('refresh');
                });
            }
        });
    }
    scope.setSupplier();
    scope.setNewSupplier = function () {
        scope.purchaseorder.SupplierCode = "";
        angular.element(document).ready(function () {
            $('#purchaseorderForm_Supplier').selectpicker('refresh');
        });
        scope.setSupplier();
    }
    scope.showNewSupplier = function () {
        if (scope.purchaseorder.SupplierCode == "_") {
            dialogModelService.showTemplate('views/supplier/SupplierFormDialog.html', 'supplierFormCtrl').result.then(function () {
                //scope.setNewSupplier();
            }, function () {
                scope.setNewSupplier();
            });
        }
    }

    scope.setTerms = function () {
        var resTer = ApiFactory.getData("Terms/GetAll");
        resTer.success(function (data, status) {
            if (data.data != null) {
                scope.termss = data.data;
                angular.element(document).ready(function () {
                    $('#purchaseorderForm_Terms').selectpicker('refresh');
                });
            }
        });
    }
    scope.setTerms();
    scope.setNewTerms = function () {
        scope.purchaseorder.TermsId = "";
        angular.element(document).ready(function () {
            $('#purchaseorderForm_Terms').selectpicker('refresh');
        });
        scope.setTerms();
    }
    scope.showNewTerms = function () {
        if (scope.purchaseorder.TermsId == "_") {
            dialogModelService.showTemplate('views/terms/termsFormDialog.html', 'termsFormCtrl').result.then(function () {
                //scope.setNewSupplier();
            }, function () {
                scope.setNewTerms();
            });
        }
    }


    scope.setItemType = function () {
        var resIte = ApiFactory.getData("ItemType/GetAll");
        resIte.success(function (data, status) {
            if (data.data != null) {
                scope.itemtypes = data.data;
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
            $('#purchaseorderForm_ItemType').selectpicker('refresh');
        });
        scope.setItemType();
    }
    scope.showNewItemType = function (index) {
        if (index.ItemTypeId == "_") {
            dialogModelService.showTemplate('views/itemtype/itemtypeFormDialog.html', 'itemtypeFormCtrl').result.then(function () {
                //scope.setNewItemType();
            }, function () {
                scope.setNewItemType(index);
            });
        }
    }


    scope.POPDF = function () {
        var res = ApiFactory.getData("PurchaseOrder/GetPOPDF", scope.purchaseorder);
        res.success(function (data, status) {
            toasterService.success(AppConstant.ViewUrl + data.datam.URLData);
            console.log(AppConstant.ViewUrl + data.datam.URLData);
        });
    }

    scope.setvalidform = function () {
        var v1 = true;
        if ((!scope.purchaseorderForm.$valid) || (scope.purchaseorder.CompanyCode == '') || (scope.purchaseorder.SupplierCode == '') || (scope.purchaseorder.TermsId == '') || scope.purchaseorder.PurchaseOrderItems.some(o=>o.ProductId == "" || o.ItemTypeId == "")) {
            return true;
        }
        return false;
    }

    scope.showalert = function () {

        if (angular.equals(scope.purchaseorder, scope.purchaseorderClone)) {
            state.go('app.purchaseorder');
        }
        else {
            if (scope.purchaseorderClone != scope.purchaseorder) {
                dialogModelService.showBackTemplate().result.then(function () {
                    state.go('app.purchaseorder');
                });
            }
        }

    }
}]);
