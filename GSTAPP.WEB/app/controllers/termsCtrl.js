'use strict';

angular.module('GSTApp')
.controller("termsCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', 'dialogModelService', 'toasterService', function (scope, state, timeout, window, ApiFactory, dialogModelService, toasterService) {
    scope.termss = [];
    
    function load() {
        var res = ApiFactory.getData("Terms/GetAll");
        res.success(function (data, status) {
            scope.termss = data.data;
            angular.element(document).ready(function () {
                var table = $('#termsTbl').DataTable({
                    select: true,
                    "scrollX": true
                });
            });
        });
       
    }
    load();
    scope.delete = function (data) {
        var res = ApiFactory.setData("Terms/Delete", data);
        res.success(function (data, status) {
            $('#termsTbl').DataTable().destroy();
            toasterService.success("Terms was deleted");
            load();
        });
        
    }
    scope.showDelete = function (data) {
        dialogModelService.show().result.then(function () {
            scope.delete(data);
        });
    }

}])
.controller("termsFormCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', '$stateParams', 'dialogModelService', 'toasterService', function (scope, state, timeout, window, ApiFactory, stateParams, dialogModelService ,toasterService) {

    scope.terms = {
        Code:"",
        TermsName: "",
        TermsName1: "",
        NumberOfDays: "",
        Remarks: "",
        Company: "",
        Company1: "",
        Disabled: ""
        
    };


    if (state.$current.name == "app.termsForm") {
        scope.terms.Code = stateParams.Code;
    }
    else {
        scope.terms.Code = "0";
    }
    scope.termsClone = angular.copy(scope.terms);

    if (scope.terms.Code != "0") {
        var res = ApiFactory.setData("Terms/GetSingle", scope.terms);
        res.success(function (data, status) {
            if (data.datam != null) {
                scope.terms.TermsName = data.datam.TermsName;
                scope.terms.TermsName1 = data.datam.TermsName;
                scope.terms.Code = data.datam.Code;
                scope.terms.NumberOfDays = data.datam.NumberOfDays;
                scope.terms.Remarks = data.datam.Remarks;
                scope.terms.Company = data.datam.Company.split(',');
                scope.terms.Company1 = data.datam.Company.split(',');
                scope.terms.Disabled = data.datam.Disabled;
                scope.termsClone = angular.copy(scope.terms);
            }
            else {
                scope.terms = angular.copy(scope.termsClone);
            }
        });
    }
        scope.reset = function () {
            scope.terms = angular.copy(scope.termsClone);
            angular.element(document).ready(function () {
                $('.selectpicker').selectpicker('refresh');
            });
        }
        scope.clear = function () {
            scope.terms = {
                Code: "0",
                TermsName: "",
                NumberOfDays: "",
                Remarks: "",
                Company: "",
                Disabled: ""
            };
            angular.element(document).ready(function () {
                $('.selectpicker').selectpicker('refresh', []);
            });
        }
        scope.save = function () {
            scope.terms.Company = "";
            scope.terms.Company1.forEach(function (o) {
                scope.terms.Company += o;

                if (scope.terms.Company1.indexOf(o) != (scope.terms.Company1.length - 1)) {
                    scope.terms.Company += ","
                }
            });

            if (scope.terms.Code == "0" || scope.terms.Code == "") {
                //Insert
                var res = ApiFactory.setData("Terms/Add", scope.terms);
                res.success(function (data, status) {
                    if (data.code == "0") {
                        toasterService.success("Terms was added");
                        scope.clear();
                        scope.termsClone = angular.copy(scope.terms);

                    }
                    else {
                        toasterService.error(data.error);
                    }
                });
            }
            else {
                //Update
                var res = ApiFactory.setData("Terms/Update", scope.terms);
                res.success(function (data, status) {
                    if (data.code == "0") {
                        toasterService.success("Terms was updated");
                        scope.terms.TermsName1 = scope.terms.TermsName;
                        scope.termsClone = angular.copy(scope.terms);

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
                    $('#termsForm_Company').selectpicker();
                });
            }
        });

    
    scope.cancelDialog = function () {
        $('.modal').click();
    };

    scope.showalert = function () {

        if (angular.equals(scope.terms, scope.termsClone)) {
            state.go('app.terms');
        }
        else {
            if (scope.termsClone != scope.terms) {
                dialogModelService.showBackTemplate().result.then(function () {
                    state.go('app.terms');
                });
            }
        }

    }
}])
;