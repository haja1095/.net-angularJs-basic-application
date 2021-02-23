'use strict';

angular.module('GSTApp')
.controller("productCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', 'dialogModelService', 'toasterService', function (scope, state, timeout, window, ApiFactory, dialogModelService,toasterService) {
    scope.products = [];

    function load() {
        var res = ApiFactory.getData("product/GetAll");
        res.success(function (data, status) {
            scope.products = data.data;
            angular.element(document).ready(function () {
                var table = $('#productTbl').DataTable({
                    select: true,
                    "scrollX": true
                });
            });
        });
       
    }
    load();
    scope.delete = function (data) {
        var res = ApiFactory.setData("product/Delete", data);
        res.success(function (data, status) {
            $('#productTbl').DataTable().destroy();
            toasterService.success("Product was deleted");
            load();
        });
        
    }
    scope.showDelete = function (data) {
        dialogModelService.show().result.then(function () {
            scope.delete(data);
        });
    }



}])
.controller("productFormCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', '$stateParams', 'dialogModelService','toasterService', function (scope, state, timeout, window, ApiFactory, stateParams, dialogModelService,toasterService) {

    scope.product = {
        Id: "",
        ProductName: "",
        Company: "",
        Company1: "",
        Department: "",
        ProductGroup: "",
        Rate: "",
        Taxable: "false",
        IGST: "",
        CGST: "",
        SGST: "",
        VAT: "",
        Barcode: "false",
        HSNCode: "",
    };
    if (state.$current.name == "app.productForm") {
        scope.product.Id = stateParams.Code;
    }
    else {
        scope.product.Id = "0";
    }
    scope.productClone = angular.copy(scope.product);

    if (scope.product.Id != "0") {
        var res = ApiFactory.setData("Product/GetSingle", scope.product);
        res.success(function (data, status) {
            if (data.datam!= null) {

                scope.product.Id = data.datam.Id;
                scope.product.ProductName = data.datam.ProductName;
                scope.product.ProductName1 = data.datam.ProductName;
                scope.product.Company = data.datam.Company.split(',');
                scope.product.Company1 = data.datam.Company.split(',');
                scope.product.Department= data.datam.Department+"";
                scope.product.ProductGroup = data.datam.ProductGroup+"";
                scope.product.Barcode = data.datam.Barcode;
                scope.product.IGST = data.datam.IGST;
                scope.product.CGST = data.datam.CGST;
                scope.product.SGST = data.datam.SGST;
                scope.product.HSNCode = data.datam.HSNCode;
                scope.product.Rate = data.datam.Rate;
                scope.product.Taxable = data.datam.Taxable;
                scope.product.VAT = data.datam.VAT;
                scope.productClone = angular.copy(scope.product);
            }
            else {
                scope.product = angular.copy(scope.productClone);
            }
        });
       
    }
        scope.reset = function () {
            scope.product = angular.copy(scope.productClone);
        }
        scope.clear = function () {
            scope.product = {
                Id: "0",
                ProductName: "",
                Department: "",
                Company: "",
                ProductGroup: "",
                Barcode: "",
                IGST: "",
                CGST: "",
                SGST: "",
                HSNCode: "",
                Rate: "",
                Taxable: "",
                VAT:""

            };
            $('.selectpicker').selectpicker('val', []);
        }
    
        scope.save = function () {
            scope.product.Company = "";
            scope.product.Company1.forEach(function (o) {
                scope.product.Company += o;

                if (scope.product.Company1.indexOf(o) != (scope.product.Company1.length-1)) {
                    scope.product.Company +=  ","
                }
            });
        if (scope.product.Id == "0" || scope.product.Id == "") {
            //Insert
            var res = ApiFactory.setData("Product/Add", scope.product);
            res.success(function (data, status) {
                if (data.code == "0") {
                    toasterService.success("Product was added");
                    scope.clear();
                    scope.productClone = angular.copy(scope.product);
                }
                else {
                    toasterService.error(data.error);
                }
            });
            }
        else {
          
            //Update
            var res = ApiFactory.setData("Product/Update", scope.product);
            res.success(function (data, status) {
                if (data.code == "0") {
                    toasterService.success("Product was updated");
                    scope.product.ProductName1 = data.datam.ProductName;
                    scope.productClone = angular.copy(scope.product);
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
                $('#productForm_Company').selectpicker();
            });
        }
    });
    scope.cancelDialog = function () {
        $('.modal').click();
    };
    scope.setDepartment = function () {
        var resDep = ApiFactory.getData("Department/GetAll");
        resDep.success(function (data, status) {
            if (data.data != null) {
                scope.departments = data.data;
                angular.element(document).ready(function () {
                    $('#productForm_Department').selectpicker();
                });
            }
        });
    }
    
    scope.setDepartment();
    scope.setNewDepartment = function () {
        scope.product.Department = "";
        angular.element(document).ready(function () {
            $('#productForm_Department').selectpicker('refresh');
        });
        scope.setDepartment();
    }
    scope.showNewDepartment = function () {
        if (scope.product.Department == "_") {
            dialogModelService.showTemplate('views/department/departmentFormDialog.html', 'departmentFormCtrl').result.then(function () {
                //$('#purchaseinvoiceForm_TermsId').selectpicker('val','');
            }, function () {
                scope.setNewDepartment();
            });
        }
    }
    scope.setProductGroup = function () {
        var resPrGro = ApiFactory.getData("ProductGroup/GetAll");
        resPrGro.success(function (data, status) {
            if (data.data != null) {
                scope.productgroups = data.data;
                angular.element(document).ready(function () {
                    $('#productForm_ProductGroup').selectpicker();
                });
            }
        });
    }
    scope.setProductGroup();
    scope.setNewProductGroup = function () {
        scope.product.ProductGroup = "";
        angular.element(document).ready(function () {
            $('#productForm_ProductGroup').selectpicker('refresh');
        });
        scope.setProductGroup();
    }
    scope.showNewProductGroup = function () {
        if (scope.product.ProductGroup == "_") {
            dialogModelService.showTemplate('views/productgroup/productgroupFormDialog.html', 'productgroupFormCtrl').result.then(function () {
                //$('#purchaseinvoiceForm_TermsId').selectpicker('val','');
            }, function () {
                scope.setNewTerms();
            });
        }
    }
    scope.showalert = function () {

        if (angular.equals(scope.product, scope.productClone)) {
            state.go('app.product');
        }
        else {
            if (scope.productClone != scope.product) {
                dialogModelService.showBackTemplate().result.then(function () {
                    state.go('app.product');
                });
            }
        }

    }


}])
;