'use strict';

angular.module('GSTApp')
.controller("customerCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', 'dialogModelService', 'toasterService', function (scope, state, timeout, window, ApiFactory, dialogModelService, toasterService) {
    scope.customers = [];

    function load() {
        var res = ApiFactory.getData("Customer/GetAll");
        res.success(function (data, status) {
            scope.customers = data.data;
            angular.element(document).ready(function () {
                var table = $('#customerTbl').DataTable({
                    select: true,
                    "scrollX": true
                });
            });
        });
    }
    load();
    scope.delete = function (data) {
        var res = ApiFactory.setData("Customer/Delete", data);
        res.success(function (data, status) {
            $('#customerTbl').DataTable().destroy();
            toasterService.success("Customer was deleted");
            load();
        });
    }
    scope.showDelete = function (data) {
        dialogModelService.show().result.then(function () {
            scope.delete(data);
        });

    }
}])

.controller("customerFormCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', '$stateParams', 'toasterService', 'dialogModelService', function (scope, state, timeout, window, ApiFactory, stateParams, toasterService, dialogModelService) {

    scope.customer = {

        Code: "",
        CompanyName: "",
        CompanyName1: "",
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
        Disabled: "",
        Address: "",
        State: "",
        StateCode: "",
        Description: "",
        GSTIN: "",
        Place: "",
        ShippingAddressTitles: "",
        ShippingAddressValues: "",
        ShippingAddress: []
    };
    if (state.$current.name == "app.customerForm") {
        scope.customer.Code = stateParams.Code;
    }
    else {
        scope.customer.Code = "0";
    }
    scope.customerClone = angular.copy(scope.customer);
    if (scope.customer.Code != "0") {
        var res = ApiFactory.setData("Customer/GetSingle", scope.customer);
        res.success(function (data, status) {
            if (data.datam != null) {
                scope.customer.Code = data.datam.Code;
                scope.customer.CompanyName = data.datam.CompanyName;
                scope.customer.CompanyName1 = data.datam.CompanyName;
                scope.customer.Description = data.datam.Description;
                scope.customer.ContactPerson = data.datam.ContactPerson;
                scope.customer.Address = data.datam.Address;
                scope.customer.PhoneNo = data.datam.PhoneNo;
                scope.customer.MobileNo = data.datam.MobileNo;
                scope.customer.WhatsAppNo = data.datam.WhatsAppNo;
                scope.customer.Email = data.datam.Email;
                scope.customer.GSTIN = data.datam.GSTIN;
                scope.customer.State = data.datam.State;
                scope.customer.PostalCode = data.datam.PostalCode;
                scope.customer.AccountNo = data.datam.AccountNo;
                scope.customer.IFSCCode = data.datam.IFSCCode;
                scope.customer.BranchName = data.datam.BranchName;
                scope.customer.BankName = data.datam.BankName;
                scope.customer.Disabled = data.datam.Disabled;
                scope.customer.Fax = data.datam.Fax;
                scope.customer.WebSite = data.datam.WebSite;
                scope.customer.Place = data.datam.Place;

                var titles = [];
                var titlesValues = [];
                scope.customer.ShippingAddress = [];
                titles = data.datam.ShippingAddressTitles.split("<==>");
                titlesValues = data.datam.ShippingAddressValues.split("<==>");
                if (titles.length > 0 && titlesValues.length > 0) {
                    titles.filter(p=>p.length > 0).forEach(o=>scope.customer.ShippingAddress.push({
                        ShippingAddressTitle: o,
                        ShippingAddressValue: titlesValues[titles.indexOf(o)]
                    }));
                }
                //scope.customer.ShippingAddress = data.datam.ShippingAddress;
                scope.customerClone = angular.copy(scope.customer);
                angular.element(document).ready(function () {
                    $('.selectpicker').selectpicker('refresh');
                        $(".myaddbody").hide(750);
                        $(".addrsave").hide();
                    });
            }
            else {
                scope.customer = angular.copy(scope.customerClone);

            }

        });
    }

    scope.reset = function () {
        scope.customer = angular.copy(scope.customerClone);
        angular.element(document).ready(function () {
            $(".myaddbody").hide(750);
        });
    }
    scope.clear = function () {
        scope.customer = {
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
            Disabled: "",
            Address: "",
            State: "",
            StateCode: "",
            Description: "",
            GSTIN: "",
            Place: "",
            ShippingAddressTitles: "",
            ShippingAddressValues: "", 
            ShippingAddress: []

        };
        angular.element(document).ready(function () {
            $('.selectpicker').selectpicker('refresh',[]);
        });

    }
    scope.setShippingAddress = function () {

        for (var i = 0; i < scope.customer.ShippingAddress.length; i++) {
            scope.customer.ShippingAddressTitles += scope.customer.ShippingAddress[i].ShippingAddressTitle;
            scope.customer.ShippingAddressValues += scope.customer.ShippingAddress[i].ShippingAddressValue;
            if (i != scope.customer.ShippingAddress.length - 1) {
                scope.customer.ShippingAddressTitles += "<==>";
                scope.customer.ShippingAddressValues += "<==>";
            }
        }
        if (scope.customer.ShippingAddress.length == 0) {
            scope.customer.ShippingAddressTitles += "<==>";
            scope.customer.ShippingAddressValues += "<==>";
        }
    };
    scope.save = function () {
        scope.setShippingAddress();
        if (scope.customer.Code == "0" || scope.customer.Code == "") {

            //Insert
            var res = ApiFactory.setData("Customer/Add", scope.customer);
            res.success(function (data, status) {
                if (data.code == "0") {
                    toasterService.success("Customer was added");
                    scope.clear();
                    scope.customerClone = angular.copy(scope.customer);
                }
                else {
                    toasterService.error(data.error);
                }
            });

        }
        else {
            //Update
            var res = ApiFactory.setData("Customer/Update", scope.customer);
            res.success(function (data, status) {
                if (data.code == "0") {
                    toasterService.success("Customer was updated");
                    scope.customer.CompanyName1 = scope.customer.CompanyName;
                    scope.customerClone = angular.copy(scope.customer);
                                   }
                else {
                    toasterService.error(data.error);

                }
            });

        }
    }
    scope.add = function (event) {
        $(".myaddbody").hide(750);
        scope.customer.ShippingAddress.push({
            ShippingAddressTitle: "",
            ShippingAddressValue: ""
        });
        angular.element(document).ready(function () {
            $(".addrsave:not(:last)").click();
            $(".addredit:last").hide();
        });
    }
    scope.remove = function (index) {
        dialogModelService.show().result.then(function () {
            scope.customer.ShippingAddress.splice(index, 1);
        });
    }
    scope.sethidepanels = function (event) {
        angular.element(document).ready(function () {
            $(event.target).parents(".myaddhead").siblings('.myaddbody').hide(750);
            if ($(event.target).is(".glyphicon")==true) {
                $(event.target).parent().hide();
                $(event.target).parent().siblings('.addredit').show();
            }
            else {
                $(event.target).hide();
                $(event.target).siblings('.addredit').show();
            }
        });
    }
    angular.element(document).ready(function () {
        $(".myaddbody").hide(750);
        $(".addrsave").hide();
    });

    scope.setshowpanel = function (event) {
        $(event.target).parents(".myaddhead").siblings('.myaddbody').show(750);
        $(event.target).siblings('.addrsave').show();
        $(event.target).hide();
    }

    var resCus = ApiFactory.getData("Miscellaneous/GetAllState");
    resCus.success(function (data, status) {
        if (data.data != null) {
            scope.states = data.data;
            angular.element(document).ready(function () {
                $('.selectpicker').selectpicker('refresh');
            });
        }
    });
    scope.cancelDialog = function () {
        $('.modal').click();
    };

    scope.setvalidform = function () {
        if (((!scope.customerForm.$valid) || (scope.customer.State == ''))) {
            return true;
        }
        return false;
    }
    scope.showalert = function () {

        if (angular.equals(scope.customer, scope.customerClone)) {
            state.go('app.customer');
        }
        else {
            if (scope.customerClone != scope.customer) {
                dialogModelService.showBackTemplate().result.then(function () {
                    state.go('app.customer');
                });
            }
        }

    }
   
}]
);