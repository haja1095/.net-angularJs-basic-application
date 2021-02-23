'use strict';

angular.module('GSTApp')
.controller("productgroupCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', 'dialogModelService','toasterService', function (scope, state, timeout, window, ApiFactory, dialogModelService, toasterService) {
    scope.productgroups = [];

    function load() {
        var res = ApiFactory.getData("ProductGroup/GetAll");
        res.success(function (data, status) {
            scope.productgroups = data.data;
            angular.element(document).ready(function () {
                var table = $('#productgroupTbl').DataTable({
                    select: true,
                    "scrollX": true
                });
            });
        });
        
    }
    load();
    scope.delete = function (data) {
        var res = ApiFactory.setData("ProductGroup/Delete", data);
        res.success(function (data, status) {
            $('#productgroupTbl').DataTable().destroy();
            toasterService.success("Product Group was deleted");
            load();
        });
       
    }
    scope.showDelete = function (data) {
        dialogModelService.show().result.then(function () {
            scope.delete(data);
        });
    }
}])
.controller("productgroupFormCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory','$stateParams','dialogModelService','toasterService', function (scope, state, timeout, window, ApiFactory,stateParams,dialogModelService,toasterService) {
    scope.productgroup = {
        
        ProductGroupName: "",
        ProductGroupName1: "",
        Id: "",
        Company: "",
        Company1: ""
    };
    if (state.$current.name == "app.productgroupForm") {
        scope.productgroup.Id = stateParams.Code;
    }
    else {
        scope.productgroup.Id = "0";
    }
    scope.productgroupClone = angular.copy(scope.productgroup);

    if (scope.productgroup.Id != "0") {
        var res = ApiFactory.setData("ProductGroup/GetSingle", scope.productgroup);
        res.success(function (data, status) {
            if (data.datam != null) {
                
                scope.productgroup.ProductGroupName = data.datam.ProductGroupName;
                scope.productgroup.ProductGroupName1 = data.datam.ProductGroupName;
                scope.productgroup.Id = data.datam.Id;
                scope.productgroup.Company = data.datam.Company.split(',');
                scope.productgroup.Company1 = data.datam.Company.split(',');
                scope.productgroupClone = angular.copy(scope.productgroup);
            } 
            else {
                scope.productgroup = angular.copy(scope.productgroupClone);
            }
        });
    }
    scope.reset = function () {
        scope.productgroup = angular.copy(scope.productgroupClone);
        angular.element(document).ready(function () {
            $('.selectpicker').selectpicker('refresh');
        });
    }
    scope.clear = function () {
        scope.productgroup = {
            ProductGroupName: "",
            Id: "0",
            Company: ""
        };
        $('.selectpicker').selectpicker('val', []);
    }
    scope.save = function () {

        scope.productgroup.Company = "";
        scope.productgroup.Company1.forEach(function (o) {
            scope.productgroup.Company += o;

            if (scope.productgroup.Company1.indexOf(o) != (scope.productgroup.Company1.length - 1)) {
                scope.productgroup.Company += ","
            }
        });

        if (scope.productgroup.Id == "0" || scope.productgroup.Id == "") {
            //Insert
            var res = ApiFactory.setData("ProductGroup/Add", scope.productgroup);
            res.success(function (data, status) {
                if (data.code == "0") {
                    toasterService.success("Product Group was added");
                    scope.clear();
                    scope.productgroupClone = angular.copy(scope.productgroup);

                }
                else {
                    toasterService.error(data.error);
                }
            });
        }
        else {
            //Update
            var res = ApiFactory.setData("ProductGroup/Update", scope.productgroup);
            res.success(function (data, status) {
                if (data.code == "0") {
                    toasterService.success("Product Group was updated");
                    scope.productgroup.ProductGroupName1 = scope.productgroup.ProductGroupName;
                    scope.productgroupClone = angular.copy(scope.productgroup);

                       }
                else {
                    toasterService.error(data.error);
                }
            });
        }
    }
    var resCom = ApiFactory.getData("Company/GetAll");
    resCom.success(function (data, status) {
        if (data.data != null) {
            scope.companys = data.data;
            angular.element(document).ready(function () {
                $('#productgroupForm_Company').selectpicker();
            });
        }
    });
    scope.cancelDialog = function () {
        $('.modal').click();
    };

    scope.showalert = function () {

        if (angular.equals(scope.productgroup, scope.productgroupClone)) {
            state.go('app.productgroup');
        }
        else {
            if (scope.productgroupClone != scope.productgroup) {
                dialogModelService.showBackTemplate().result.then(function () {
                    state.go('app.productgroup');
                });
            }
        }

    }
   }]);
