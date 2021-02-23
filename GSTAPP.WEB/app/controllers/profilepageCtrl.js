'use strict';

angular.module('GSTApp')
.controller("profilepageCtrl", ['$scope', '$state', '$timeout', 'AppConstant', '$window', 'ApiFactory', 'Upload', '$stateParams', 'dialogModelService', 'toasterService', function (scope, state, timeout, AppConstant, window, ApiFactory, Upload, stateParams, dialogModelService, toasterService) {
    var res = ApiFactory.getData("Company/GetAll");
    res.success(function (data, status) {
        scope.companys = data.data;
        angular.element(document).ready(function () {
            $('[data-toggle="popover"]').popover({
                placement: 'top',
                trigger: 'hover',
                html: true
            }).hover(function () {
                $(this).popover('show');
            },
            function () {
                $(this).popover('hide');
            });
        });
    });

    scope.setLogo = function (company) {
        if (company.Image.startsWith("data:image") == true) {
            return company.Image;
        }
        return null;
    }
}]);

