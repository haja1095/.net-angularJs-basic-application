'use strict';

angular.module('GSTApp')
    .controller("validationCtrl", ['$scope', '$state', 'ApiFactory', 'toasterService', function (scope, state, ApiFactory, toasterService) {
        scope.validationSend = {
            Email: "",
            PhoneNo: ""
        }

        scope.validate = function () {
            scope.validationSend.PhoneNo = scope.validationSend.Email;
            var res = ApiFactory.setData("Registration/GetSingleByEmailorPhoneNo", scope.validationSend);
            res.success(function (data, status) {
                if (data.datam != null) {
                    state.go("verification", { Email: data.datam.Email, PhoneNo: data.datam.PhoneNo });
                }
                else {
                    toasterService.error(data.error);
                }
            });
            //state.go("verification", { Email: scope.validationSend.Email, PhoneNo: scope.validationSend.PhoneNo });
        }
    }])
;