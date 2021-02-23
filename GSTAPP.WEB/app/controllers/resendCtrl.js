'use strict';

angular.module('GSTApp')
.controller("resendCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', 'toasterService', 'AppConstant', '$stateParams', function (scope, state, timeout, window, ApiFactory, toasterService, AppConstant, stateParams) {
    scope.resend = {
        Email: "",
        URLData: AppConstant.ViewUrl + "#/EmailVerification/"
    }
    scope.resend.Email = stateParams.Email;
    scope.resendData = function () {
        var res = ApiFactory.setData("Registration/ResendData", scope.resend);
        res.success(function (data, status) {
            if (data.code == "0") {
                toasterService.success("Verification code wes resended!");
                state.go("resendSuccess")
            }
        });
    }
}]);