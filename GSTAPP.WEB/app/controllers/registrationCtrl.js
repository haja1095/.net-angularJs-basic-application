'use strict';

angular.module('GSTApp')
.controller("registrationCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', 'toasterService', 'AppConstant', function (scope, state, timeout, window, ApiFactory, toasterService, AppConstant) {
    scope.registration = {
        Email: "",
        PhoneNo: "",
        Password: "",
        URLData: AppConstant.ViewUrl + "#/EmailVerification/"
    }
    scope.regUser = function () {
        var res = ApiFactory.setData("Registration/Register", scope.registration);
        res.success(function (data, status) {
            if (data.code == "0") {
                state.go("registrationSuccess");
                //toasterService.success("Registration Success.");
                //toasterService.info("Verification link was sended to " + scope.registration.Email);
                
                scope.registration = {
                    Email: "",
                    PhoneNo: "",
                    Password: "",
                    URLData: AppConstant.ViewUrl + "#/EmailVerification/"
                }
                return;
            }
            toasterService.warning(data.error);
        });
    }
}]);