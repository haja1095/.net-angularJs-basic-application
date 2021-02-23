'use strict';

angular.module('GSTApp')
.controller("EmailVerificationCtrl", ['$scope', '$state', 'ApiFactory', '$stateParams', 'toasterService', function (scope, state, ApiFactory, stateParams, toasterService) {
    scope.EmailVerification = {
        Email: "",
        EmailVerificationCode: "",
        EmailVerification: false
    }

    if (stateParams.Email.length <= 0 || stateParams.EmailVerificationCode.length <= 0) {
        toasterService.warning("Not valid Link");
    }

    scope.EmailVerification.Email = stateParams.Email;
    scope.EmailVerification.EmailVerificationCode = stateParams.EmailVerificationCode;

    scope.verifyEmail = function () {
        var rese = ApiFactory.setData("Registration/EmailVerification", scope.EmailVerification);
        rese.success(function (data, status) {
            if (data.code == "0") {
                scope.EmailVerification.EmailVerification = data.datam.EmailVerification;
                state.go('EmailVerificationSuccess');
                return;
            }
            else if (data.code == "2") {

                state.go('resend', { Email: scope.EmailVerification.Email });

            }
            else {
                toasterService.warning(data.error);
            }
        });
    }

    scope.verifyEmail();
}]);
;