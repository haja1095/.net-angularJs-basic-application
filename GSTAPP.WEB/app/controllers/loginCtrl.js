'use strict';

angular.module('GSTApp')
.controller("loginCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', 'authFac', 'toasterService', function (scope, state, timeout, window, ApiFactory, authFac, toasterService) {
    scope.loginData = {
        UserName: "",
        Password: ""
    }
    authFac.setLogout();
    if (authFac.getTocken() != null && authFac.getTocken().length > 0) {
        state.go('app');
    }
    //Functions
    scope.loginData = {};
    scope.loginData.UserName = "";
    scope.loginData.Password = "";
    scope.login = function () {
        authFac.setLogout();

        var res = ApiFactory.setData("Login/LoginUser", scope.loginData);
        res.success(function (data, status) {
            if (data.IsLogin == true) {
                authFac.setLogin(data);
                toasterService.success("Login sucess");
                state.go('app');
                return;
            }
            toasterService.warning("Login Error !");
        });
    }
}]);

//http://www.c-sharpcorner.com/blogs/basic-authentication-in-webapi 
//md5 algorithm