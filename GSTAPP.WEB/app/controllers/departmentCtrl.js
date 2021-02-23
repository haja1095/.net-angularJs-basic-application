'use strict';

angular.module('GSTApp')
.controller("departmentCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', 'dialogModelService', 'toasterService', function (scope, state, timeout, window, ApiFactory, dialogModelService, toasterService) {
    scope.departments = [];
    function load() {
        var res = ApiFactory.getData("Department/GetAll");
        res.success(function (data, status) {
            scope.departments = data.data;
            angular.element(document).ready(function () {
                var table = $('#departmentTbl').DataTable({
                    select: true,
                    "scrollX": true
                });
            });
        });

    }
    load();
    scope.delete = function (data) {
        var res = ApiFactory.setData("department/Delete", data);
        res.success(function (data, status) {
            $('#departmentTbl').DataTable().destroy();
            toasterService.success("Department was deleted");
            load();
        });

    }
    scope.showDelete = function (data) {
        dialogModelService.show().result.then(function () {
            scope.delete(data);
        });
    }

}])

.controller("departmentFormCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', '$stateParams', 'dialogModelService', 'toasterService', function (scope, state, timeout, window, ApiFactory, stateParams, dialogModelService, toasterService) {

    scope.department = {
        Id: "",
        DepartmentName: "",
        DepartmentName1: "",
        Company: "",
        Company1: ""
    }

    scope.departmentClone = angular.copy(scope.department);
    if (state.$current.name == "app.departmentForm") {
        scope.department.Id = stateParams.Code;
    }
    else {
        scope.department.Id = "0";
    }
    if (scope.department.Id != "0") {
        var res = ApiFactory.setData("department/GetSingle", scope.department);
        res.success(function (data, status) {
            if (data.datam != null) {
                scope.department.DepartmentName = data.datam.DepartmentName
                scope.department.DepartmentName1 = data.datam.DepartmentName;
                scope.department.Id = data.datam.Id;
                scope.department.Company = data.datam.Company.split(',');
                scope.department.Company1 = data.datam.Company.split(',');
                scope.department.Disabled = data.datam.Disabled;

                scope.departmentClone = angular.copy(scope.department);
            }
            else {
                scope.department = angular.copy(scope.departmentClone);
            }
        });

    }
    scope.reset = function () {
        scope.department = angular.copy(scope.departmentClone);
        angular.element(document).ready(function () {
            $('.selectpicker').selectpicker('refresh');
        });
    }
    scope.clear = function () {
        scope.department = {
            Id: "0",
            DepartmentName: "",
            Company: []
        };
        $('.selectpicker').selectpicker('val', []);
    }
    scope.save = function () {
        scope.department.Company = "";
        scope.department.Company1.forEach(function (o) {
            scope.department.Company += o;

            if (scope.department.Company1.indexOf(o) != (scope.department.Company1.length - 1)) {
                scope.department.Company += ","
            }
        });
        if (scope.department.Id == "0" || scope.department.Id == "") {
            //Insert
            var res = ApiFactory.setData("Department/Add", scope.department);
            res.success(function (data, status) {
                if (data.code == "0") {
                    toasterService.success("Department was added");
                    scope.clear();
                    scope.departmentClone = angular.copy(scope.department);
                }
                else {
                    toasterService.error(data.error);
                }
            });
        }
        else {
            //Update
            var res = ApiFactory.setData("Department/Update", scope.department);
            res.success(function (data, status) {
                if (data.code == "0") {
                    toasterService.success("Department was updated");
                    scope.departmentClone = angular.copy(scope.department);
                    scope.department.DepartmentName1 = scope.department.DepartmentName;
                    scope.departmentClone = angular.copy(scope.department);
                }
                else {
                    toasterService.error(data.error);

                }
            });

        }
    }
    scope.setCompany = function () {
        var resCom = ApiFactory.getData("Company/GetAll");
        resCom.success(function (data, status) {
            if (data.data != null) {
                scope.companys = data.data;
                angular.element(document).ready(function () {
                    $('#departmentForm_Company').selectpicker('refresh');
                });
            }
        });
    }
    scope.setCompany();
    scope.setNewCompany = function () {
        scope.department.Company1.splice(scope.department.Company1.indexOf('_'), 1);
        angular.element(document).ready(function () {
            $('#departmentForm_Company').selectpicker('refresh');
        });
        scope.setCompany();
    }
    scope.showNewCompany = function () {
        if (scope.department.Company1.findIndex(p=>p == "_") >= 0) {
            dialogModelService.showTemplate('views/company/companyFormDialog.html', 'companyFormCtrl').result.then(function () {

            }, function () {
                scope.setNewCompany();
            });
        }
    }
    scope.cancelDialog = function () {
        $('.modal').click();
    };
    scope.showalert = function () {

        if (angular.equals(scope.department, scope.departmentClone)) {
            state.go('app.department');
        }
        else {
            if (scope.departmentClone != scope.department) {
                dialogModelService.showBackTemplate().result.then(function () {
                    state.go('app.department');
                });
            }
        }

    }

}]);
