'use strict';
angular.module('GSTApp')
.controller("itemtypeCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', 'dialogModelService', 'toasterService', function (scope, state, timeout, window, ApiFactory, dialogModelService, toasterService) {
    scope.itemtypes = [];

    function load() {
        var res = ApiFactory.getData("ItemType/GetAll");
        res.success(function (data, status) {
            scope.itemtypes = data.data;
            angular.element(document).ready(function () {
                var table = $('#itemtypeTbl').DataTable({
                    select: true,
                    "scrollX": true
                });
            });
        });
        
    }
    load();
    scope.delete = function (data) {
        var res = ApiFactory.setData("ItemType/Delete", data);
        res.success(function (data, status) {
            $('#itemtypeTbl').DataTable().destroy();
            toasterService.success("Item Type was deleted");
            load();
        });
        
    }
    scope.showDelete = function (data) {
        dialogModelService.show().result.then(function () {
            scope.delete(data);
        });
    }



}])
.controller("itemtypeFormCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', '$stateParams', 'dialogModelService', 'toasterService', function (scope, state, timeout, window, ApiFactory, stateParams, dialogModelService,toasterService) {
    scope.itemtype = {
        
        ItemTypeName: "",
        ItemTypeName1: "",
        ItemCategory: "",
        Company: "",
        Company1: "",
        Id: ""
    };
    if (state.$current.name == "app.itemtypeForm") {
        scope.itemtype.Id = stateParams.Code;
    }
    else {
        scope.itemtype.Id = "0";
    }
    scope.itemtypeClone = angular.copy(scope.itemtype);

    if (scope.itemtype.Id != "0") {
        var res = ApiFactory.setData("ItemType/GetSingle", scope.itemtype);
        res.success(function (data, status) {
            if (data.datam != null) {
                
                scope.itemtype.ItemTypeName = data.datam.ItemTypeName;
                scope.itemtype.ItemTypeName1 = data.datam.ItemTypeName;
                scope.itemtype.Company = data.datam.Company.split(',');
                scope.itemtype.Company1 = data.datam.Company.split(',');
                scope.itemtype.ItemCategory = data.datam.ItemCategory+"";
                scope.itemtypeClone = angular.copy(scope.itemtype);
                
                
            }
            else {
                scope.itemtype = angular.copy(scope.itemtypeClone);
                angular.element(document).ready(function () {
                    $('.selectpicker').selectpicker('refresh');
                });
            }
        });
         
    }

   
    scope.reset = function () {
        scope.itemtype = angular.copy(scope.itemtypeClone);
        angular.element(document).ready(function () {
            $('.selectpicker').selectpicker('refresh');
        });
    }
    scope.clear = function () {
        scope.itemtype = {
            ItemTypeName: "",
            ItemCategory: "",
            Company: "",
            Company1:"",
            Id:"0"
        };
        angular.element(document).ready(function () {
            $('.selectpicker').selectpicker('val',[]);
        });
    }
    scope.save = function () {

        scope.itemtype.Company = "";
        scope.itemtype.Company1.forEach(function (o) {
            scope.itemtype.Company += o;

            if (scope.itemtype.Company1.indexOf(o) != (scope.itemtype.Company1.length - 1)) {
                scope.itemtype.Company += ","
            }
        });

        if (scope.itemtype.Id == "0" || scope.itemtype.Id == "") {
            //Insert
            var res = ApiFactory.setData("ItemType/Add", scope.itemtype);
            res.success(function (data, status) {
                if (data.code == "0") {
                    toasterService.success("Item Type was added");
                    scope.clear();
                    scope.itemtypeClone = angular.copy(scope.itemtype);

                }
            else {
                    toasterService.error(data.error);
            }
            });
    }
        else {
            //Update
            var res = ApiFactory.setData("ItemType/Update", scope.itemtype);
            res.success(function (data, status) {
                if (data.code == "0") {
                    toasterService.success("Item Type was updated");
                    scope.itemtype.ItemTypeName1 = scope.itemtype.ItemTypeName;
                    scope.itemtypeClone = angular.copy(scope.itemtype);
                    //scope.clear();
                }
                else
                {
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
                    $('#itemtypeForm_Company').selectpicker();
                });
            }
        });

        scope.cancelDialog = function () {
            $('.modal').click();
        };
    
        scope.setItemCategory = function () {
            var resItCat = ApiFactory.getData("ItemCategory/GetAll");
            resItCat.success(function (data, status) {
                if (data.data != null) {
                    scope.itemCategorys = data.data;
                    angular.element(document).ready(function () {
                        $('#itemtypeForm_ItemCategory').selectpicker('refresh');
                    });
                }
            });
        }
        scope.setItemCategory();
        scope.setNewItemCategory = function () {
            scope.itemtype.ItemCategory = "";
            angular.element(document).ready(function () {
                $('#itemtypeForm_Company').selectpicker('refresh');
            });
            scope.setItemCategory();
        }
        scope.showNewItemCategory = function () {
            if (scope.itemtype.ItemCategory == "_") {
                dialogModelService.showTemplate('views/itemcategory/itemcategoryFormDialog.html', 'itemcategoryFormCtrl').result.then(function () {
                    //scope.setNewItemCategory();
                }, function () {
                    scope.setNewItemCategory();
                });
            }
        }

        scope.showalert = function () {

            if (angular.equals(scope.itemtype, scope.itemtypeClone)) {
                state.go('app.itemtype');
            }
            else {
                if (scope.termsClone != scope.itemtype) {
                    dialogModelService.showBackTemplate().result.then(function () {
                        state.go('app.itemtype');
                    });
                }
            }

        }
}])
;



