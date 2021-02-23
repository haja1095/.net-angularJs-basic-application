'use strict';

angular.module('GSTApp')
.controller("itemcategoryCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', 'dialogModelService', 'toasterService', function (scope, state, timeout, window, ApiFactory, dialogModelService, toasterService) {
    scope.itemcategorys = [];

    function load() {
        var res = ApiFactory.getData("ItemCategory/GetAll");
        res.success(function (data, status) {
            scope.itemcategorys = data.data;
            angular.element(document).ready(function () {
                var table = $('#itemcategoryTbl').DataTable({
                    select: true,
                    "scrollX": true
                });
            });
        });

    }
    load();
    scope.delete = function (data) {
        var res = ApiFactory.setData("ItemCategory/Delete", data);
        res.success(function (data, status) {
            $('#itemcategoryTbl').DataTable().destroy();
            toasterService.success("Item Category was deleted");
            load();
        });

    }
    scope.showDelete = function (data) {
        dialogModelService.show().result.then(function () {
            scope.delete(data);
        });
    }

}])


.controller("itemcategoryFormCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', '$stateParams', 'dialogModelService', 'toasterService', function (scope, state, timeout, window, ApiFactory, stateParams, dialogModelService, toasterService) {

    scope.itemcategory = {
        Id: "",
        ItemCategoryName: "",
        ItemCategoryName1: "",
        Company: "",
        Company1: "",
        Disabled: ""

    };
    if (state.$current.name == "app.itemcategoryForm") {
        scope.itemcategory.Id = stateParams.Code;
    }
    else {
        scope.itemcategory.Id = "0";
    }
    scope.itemcategoryClone = angular.copy(scope.itemcategory);
    if (scope.itemcategory.Id != "0") {
        var res = ApiFactory.setData("ItemCategory/GetSingle", scope.itemcategory);
        res.success(function (data, status) {
            if (data.datam != null) {
                scope.itemcategory.ItemCategoryName = data.datam.ItemCategoryName;
                scope.itemcategory.ItemCategoryName1 = data.datam.ItemCategoryName;
                scope.itemcategory.Id = data.datam.Id;
                scope.itemcategory.Company = data.datam.Company.split(',');
                scope.itemcategory.Company1 = data.datam.Company.split(',');
                scope.itemcategory.Disabled = data.datam.Disabled;
                scope.itemcategoryClone = angular.copy(scope.itemcategory);

            }
            else {
                scope.itemcategory = angular.copy(scope.itemcategoryClone);
            }
        });

    }

    scope.reset = function () {
        scope.itemcategory = angular.copy(scope.itemcategoryClone);
        angular.element(document).ready(function () {
            $('.selectpicker').selectpicker('refresh');
        });
    }
    scope.clear = function () {
        scope.itemcategory = {
            Id: "0",
            ItemCategoryName: "",
            Company: "",
            Disabled: ""
        };
        $('.selectpicker').selectpicker('val', []);
    }

    scope.save = function () {

        scope.itemcategory.Company = "";
        scope.itemcategory.Company1.forEach(function (o) {
            scope.itemcategory.Company += o;

            if (scope.itemcategory.Company1.indexOf(o) != (scope.itemcategory.Company1.length - 1)) {
                scope.itemcategory.Company += ","
            }
        });

        if (scope.itemcategory.Id == "0" || scope.itemcategory.Id == "") {
            //Insert
            var res = ApiFactory.setData("ItemCategory/Add", scope.itemcategory);
            res.success(function (data, status) {
                if (data.code == "0") {
                    toasterService.success("Item Category was added");
                    scope.clear();
                    scope.itemcategoryClone = angular.copy(scope.itemcategory);
                }
                else {
                    toasterService.error(data.error);
                }
            });
        }
        else {
            //Update
            var res = ApiFactory.setData("ItemCategory/Update", scope.itemcategory);
            res.success(function (data, status) {
                if (data.code == "0") {
                    toasterService.success("Item Category was updated");
                    scope.itemcategory.ItemCategoryName1 = scope.itemcategory.ItemCategoryName;
                    scope.itemcategoryClone = angular.copy(scope.itemcategory);

                    //scope.clear();
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
                $('#itemcategoryForm_Company').selectpicker();
            });
        }
    });
    scope.cancelDialog = function () {
        $('.modal').click();
    };

    scope.showalert = function () {
        
        if (angular.equals(scope.itemcategory, scope.itemcategoryClone)) {
            state.go('app.itemcategory');
        }
        else {
            if (scope.itemcategoryClone != scope.itemcategory) {
                dialogModelService.showBackTemplate().result.then(function () {
                    state.go('app.itemcategory');
                });
            }
        }

    }


}])
;