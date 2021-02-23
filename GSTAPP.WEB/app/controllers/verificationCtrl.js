'use strict';

angular.module('GSTApp')
    .controller("verificationCtrl", ['$scope', '$state', 'ApiFactory', '$stateParams', 'toasterService', function (scope, state, ApiFactory, stateParams, toasterService) {
        scope.verification = {
            Email: "",
            PhoneNo: "",
            EmailVerificationCode: "",
            PhoneNoVerificationCode: "",
            PhoneNoVerification: false,
            EmailVerification: false
        }
        if (stateParams.Email.length <= 0 || stateParams.PhoneNo.length <= 0) {
            state.go("login");
        }

        scope.verification.Email = stateParams.Email;
        scope.verification.PhoneNo = stateParams.PhoneNo;

        scope.verifyEmail = function () {
            var rese = ApiFactory.setData("Registration/EmailVerification", scope.verification);
            rese.success(function (data, status) {
                if (data.code == "0") {
                    scope.verification.EmailVerification = data.datam.EmailVerification;
                    toasterService.success("Email Verified!");
                    ee();
                    login();
                    return;
                }
                toasterService.warning(data.error);
            });
        }

        scope.verifyPhone = function () {
            var rese = ApiFactory.setData("Registration/PhoneVerification", scope.verification);
            rese.success(function (data, status) {
                if (data.code == "0") {
                    scope.verification.PhoneNoVerification = data.datam.PhoneNoVerification;
                    toasterService.success("Phone number Verified!");
                    ee();
                    login();
                    return;
                }
                toasterService.warning(data.error);
            });
        }


        function login() {
            if (scope.verification.EmailVerification == true && scope.verification.PhoneNoVerification == true) {
                state.go("login");
            }
        }

        function ee() {
            var res = ApiFactory.setData("Registration/EmailandPhoneIsVerifid", scope.verification);
            res.success(function (data, status) {
                if (data.code == "0") {
                    scope.verification.EmailVerification = data.datam.EmailVerification;
                    scope.verification.PhoneNoVerification = data.datam.PhoneNoVerification;
                    login();
                }
            });

        }
        ee();


        scope.resendData = function () {
            var res = ApiFactory.setData("Registration/ResendData", scope.verification);
            res.success(function (data, status) {
                if (data.code == "0") {
                    toasterService.success("Verification code wes resended!");
                    //state.go("")
                }
            });
        }
    }])
;