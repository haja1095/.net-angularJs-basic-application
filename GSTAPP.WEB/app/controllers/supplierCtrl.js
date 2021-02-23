'use strict';

angular.module('GSTApp')
.controller("supplierCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', 'dialogModelService','toasterService', function (scope, state, timeout, window, ApiFactory, dialogModelService,toasterService) {
    scope.suppliers = [];

    function load() {
        var res = ApiFactory.getData("Supplier/GetAll");
        res.success(function (data, status) {
            scope.suppliers = data.data;
            angular.element(document).ready(function () {
                var table = $('#supplierTbl').DataTable({
                    select: true,
                    "scrollX": true
                });
            });
        });
        
    }
    load();
    scope.delete = function (data) {
        var res = ApiFactory.setData("Supplier/Delete", data);
        res.success(function (data, status) {
            $('#supplierTbl').DataTable().destroy();
            toasterService.success("Supplier was deleted");
            load();
        });
      
    }
    scope.showDelete = function (data) {
        dialogModelService.show().result.then(function () {
            scope.delete(data);
        });
    }
}])
.controller("supplierFormCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', '$stateParams',  'dialogModelService','toasterService', function (scope, state, timeout, window, ApiFactory, stateParams,  dialogModelService,toasterService) {
    

    scope.supplier = {
        Code: "",
        CompanyName: "",
        PhoneNo: "",
        MobileNo: "",
        WhatsAppNo: "",
        WebSite: "",
        Fax: "",
        ContactPerson: "",
        BankName: "",
        BranchName: "",
        IFSCCode: "",
        AccountNo: "",
        Email:"",
        Address:"",
        State:"",
        PostalCode:"",
        Description: "",
        GSTIN: "",
        Place: ""
    };
   
    if (state.$current.name == "app.supplierForm") {
        scope.supplier.Code = stateParams.Code;
    }
    else {
        scope.supplier.Code = "0";
    }
    scope.supplierClone = angular.copy(scope.supplier);
    if (scope.supplier.Code != "0") {
        var res = ApiFactory.setData("Supplier/GetSingle", scope.supplier);
        res.success(function (data, status) {
            if (data.datam != null) {
                scope.supplier.Code = data.datam.Code;
                scope.supplier.CompanyName = data.datam.CompanyName;
                scope.supplier.CompanyName1 = data.datam.CompanyName;
                scope.supplier.PhoneNo = data.datam.PhoneNo;
                scope.supplier.MobileNo = data.datam.MobileNo;
                scope.supplier.WhatsAppNo = data.datam.WhatsAppNo;
                scope.supplier.WebSite = data.datam.WebSite;
                scope.supplier.Fax = data.datam.Fax;
                scope.supplier.ContactPerson = data.datam.ContactPerson;
                scope.supplier.BankName = data.datam.BankName;
                scope.supplier.BranchName = data.datam.BranchName;
                scope.supplier.IFSCCode = data.datam.IFSCCode;
                scope.supplier.AccountNo = data.datam.AccountNo;
                scope.supplier.Email = data.datam.Email;
                scope.supplier.Address = data.datam.Address;
                scope.supplier.Place = data.datam.Place;
                scope.supplier.State = data.datam.State;
                scope.supplier.PostalCode = data.datam.PostalCode;
                scope.supplier.Description = data.datam.Description;
                scope.supplier.GSTIN = data.datam.GSTIN;
                scope.supplierClone = angular.copy(scope.supplier);
            }
            else {
                scope.supplier = angular.copy(scope.supplierClone);
            }
        });
    }
    scope.reset = function () {
        scope.supplier = angular.copy(scope.supplierClone);
    }
    scope.clear = function () {
        scope.supplier= {
            Code: "0",
            CompanyName: "",
            PhoneNo: "",
            MobileNo: "",
            WhatsAppNo: "",
            WebSite: "",
            Fax: "",
            ContactPerson: "",
            BankName: "",
            BranchName: "",
            IFSCCode: "",
            AccountNo: "",
            Email: "",
            Address: "",
            State: "",
            PostalCode: "",
            Description: "",
            GSTIN: "",
            Place: ""
        };
        $('.selectpicker').selectpicker('val', []);
    }
    scope.save = function () {
        if (scope.supplier.Code == "0" || scope.supplier.Code == "") {
            //Insert
            var res = ApiFactory.setData("Supplier/Add", scope.supplier);
            res.success(function (data, status) {
                if (data.code == "0") {
                    toasterService.success("Supplier was added");
                    scope.clear();
                    scope.supplierClone = angular.copy(scope.supplier);
                }
                else {
                    toasterService.error(data.error);
                }
            });
           }
        else {
            //Update
            var res = ApiFactory.setData("Supplier/Update", scope.supplier);
            res.success(function (data, status) {
                if (data.code == "0") {
                    toasterService.success("Supplier was updated");
                    scope.supplier.CompanyName1 = data.datam.CompanyName;
                    scope.supplierClone = angular.copy(scope.supplier);
                }
                else {
                    toasterService.error(data.error);
                }
            });
        }
    }
    var resSup = ApiFactory.getData("Miscellaneous/GetAllState");
    resSup.success(function (data, status) {
        if (data.data != null) {
            scope.states = data.data;
            angular.element(document).ready(function () {
                $('.selectpicker').selectpicker();
            });
        }
    });
    scope.cancelDialog = function () {
        $('.modal').click();
    };
    scope.showalert = function () {

        if (angular.equals(scope.supplier, scope.supplierClone)) {
            state.go('app.supplier');
        }
        else {
            if (scope.supplierClone != scope.supplier) {
                dialogModelService.showBackTemplate().result.then(function () {
                    state.go('app.supplier');
                });
            }
        }

    }
}])
;