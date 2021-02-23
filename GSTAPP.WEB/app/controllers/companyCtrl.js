'use strict';

angular.module('GSTApp')
.controller("companyCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', 'dialogModelService', 'toasterService', function (scope, state, timeout, window, ApiFactory, dialogModelService, toasterService) {
    scope.companys = [];

    function load() {
        var res = ApiFactory.getData("Company/GetAll");
        res.success(function (data, status) {
            scope.companys = data.data;
            angular.element(document).ready(function () {
                var table = $('#companyTbl').DataTable({
                    select: true,
                    "scrollX": true
                });
                $('[data-toggle="popover"]').popover({
                    placement: 'top',
                    trigger: 'hover',
                    html: true
                }).hover(function () {
                    $(this).popover('show');
                },
                function () {
                    $(this).popover('hide');
                });
            });
        });
    }
    load();
    scope.delete = function (data) {
        var res = ApiFactory.setData("Company/Delete", data);
        res.success(function (data, status) {
            $('#companyTbl').DataTable().destroy();
            toasterService.success("Company was deleted");
            load();
        });
    }
    scope.showDelete = function (data) {
        dialogModelService.show().result.then(function () {
            scope.delete(data);
        });
    }
    scope.setLogo = function (company) {
        if (company.Image.startsWith("data:image") == true) {
            return company.Image;
        }
        return null;
    }
}])
.controller("companyFormCtrl", ['$scope', '$state', '$timeout', 'AppConstant', '$window', 'ApiFactory', 'Upload', '$stateParams', 'dialogModelService', 'toasterService', function (scope, state, timeout, AppConstant, window, ApiFactory, Upload, stateParams, dialogModelService, toasterService) {

    scope.company = {
        CompanyName: "",
        CompanyName1: "",
        Description: "",
        Address: "",
        PhoneNo: "",
        MobileNo: "",
        Email: "",
        GSTIN: "",
        State: "",
        PostalCode: "",
        Code: "",
        Disabled: "",
        Fax: "",
        WebSite: "",
        Image: ""
    };
   
    if (state.$current.name == "app.companyForm") {
        scope.company.Code = stateParams.Code;
    }
    else {
        scope.company.Code = "0";
    }
    scope.companyClone = angular.copy(scope.company);
    if (scope.company.Code != "0") {
        var res = ApiFactory.setData("Company/GetSingle", scope.company);
        res.success(function (data, status) {
            if (data.datam != null) {
                scope.company.Code = data.datam.Code;
                scope.company.CompanyName = data.datam.CompanyName;
                scope.company.CompanyName1 = data.datam.CompanyName;
                scope.company.Description = data.datam.Description;
                scope.company.Address = data.datam.Address;
                scope.company.PhoneNo = data.datam.PhoneNo;
                scope.company.MobileNo = data.datam.MobileNo;
                scope.company.Email = data.datam.Email;
                scope.company.GSTIN = data.datam.GSTIN;
                scope.company.State = data.datam.State;
                scope.company.PostalCode = data.datam.PostalCode;
                scope.company.Disabled = data.datam.Disabled;
                scope.company.Image = data.datam.Image;
                scope.company.Fax = data.datam.Fax;
                scope.company.WebSite = data.datam.WebSite;
                scope.picFile = scope.company.Image;
                if (scope.picFile.startsWith("data:image") == false) {
                    scope.picFile = null;
                }
                scope.companyClone = angular.copy(scope.company);

                angular.element(document).ready(function () {
                    $('.selectpicker').selectpicker('refresh');
                });
            }
            else {
                scope.company = angular.copy(scope.companyClone);
            }
        });
    }
    scope.reset = function () {
        scope.company = angular.copy(scope.companyClone);
        scope.picFile = scope.company.Image;
        angular.element(document).ready(function () {
            $('.selectpicker').selectpicker('refresh');
        });
    }
    scope.clear = function () {
        scope.company = {
            CompanyName: "",
            Description: "",
            Address: "",
            PhoneNo: "",
            MobileNo: "",
            Email: "",
            GSTIN: "",
            State: "",
            PostalCode: "",
            Code: "0",
            Disabled: "",
            Fax: "",
            WebSite: "",
            Image: ""
        };
        scope.picFile = null;
        $('.selectpicker').selectpicker('val', '');
    }
    scope.save = function () {
        scope.company.Image = scope.picFile;
        if (scope.company.Code == "0" || scope.company.Code == "") {
            //Insert
            var res = ApiFactory.setData("Company/Add", scope.company);
            res.success(function (data, status) {
                if (data.code == 0) {
                    toasterService.success("Company was added");
                    scope.clear();
                    scope.companyClone = angular.copy(scope.company);
                }
                else {
                    toasterService.error(data.error);
                }
            });
        }
        else {
            //Update
            var res = ApiFactory.setData("Company/Update", scope.company);
            res.success(function (data, status) {
                if (data.code == 0) {
                    //scope.clear();
                    toasterService.success("Company was Updated");
                    scope.company.CompanyName1 = scope.company.CompanyName;
                    scope.companyClone = angular.copy(scope.company);
                               }
                else {
                    toasterService.error(data.error);
                  
                }
            });
        }
    }
    scope.picFile = null;
    scope.uploadPic = function (file) {
        scope.save();
    }
    scope.showConfirm = function (ev) {
        dialogModelService.show().result.then(function () {
            scope.picFile = null;
            $("#imgctn").attr('src', null);
        });
    };
    var res = ApiFactory.getData("Miscellaneous/GetAllState");
    res.success(function (data, status) {
        scope.states = data.data;
        angular.element(document).ready(function () {
            $('.selectpicker').selectpicker('refresh');
        });
    });

    scope.she = function () {
        if (scope.picFile != null) {
            return true;
        }
        return false;
    }

    scope.cancelDialog = function () {
        $('.modal').click();
    };
    scope.myImage = '';
    var handleFileSelect = function (evt) {
        var file = evt.currentTarget.files[0];
        var reader = new FileReader();
        reader.onload = function (evt) {
            scope.$apply(function ($scope) {
                $scope.myImage = evt.target.result;
                dialogModelService.logoCrop($scope.myImage).result.then(function (data) {
                    scope.picFile = data;
                }, function () {
                    angular.element(document.querySelector('#fileInput')).val(null);
                });
            });
        };
        reader.readAsDataURL(file);
    };
    angular.element(document.querySelector('#fileInput')).on('change', handleFileSelect);
    angular.element(document.querySelector('#btnfileInput')).on('click', function () {
        angular.element(document.querySelector('#fileInput')).click();
    });
    scope.showalert = function () {

        if (angular.equals(scope.company, scope.companyClone)) {
            state.go('app.company');
        }
        else {
            if (scope.companyClone != scope.company) {
                dialogModelService.showBackTemplate().result.then(function () {
                    state.go('app.company');
                });
            }
        }

    }
}]);


