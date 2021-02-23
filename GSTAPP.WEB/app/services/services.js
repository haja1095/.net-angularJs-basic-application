'use strict';

angular.module('GSTApp')
.service('dialogModelService', ['$uibModal', function ($uibModal) {
        return {
            show: function () {
                var modalInstance = $uibModal.open({
                    templateUrl: 'views/DialogModel.html',
                    controller: 'dialogModelCtrl',
                    backdrop: 'static'
                });
                //modalInstance.result.then(function () {
                //    fun();
                //}, function () {

                //});
                return modalInstance;
            },
            showLogoutDialog: function () {
                var modalInstance = $uibModal.open({
                    templateUrl: 'views/logoutDialogModel.html',
                    controller: 'logoutDialogModelCtrl',
                    backdrop: 'static'
                });
                //modalInstance.result.then(function () {
                //    fun();
                //}, function () {

                //});
                return modalInstance;
            },
            showTemplate: function (tmpUrl,tmpCtrl) {
                var modalInstance = $uibModal.open({
                    templateUrl: tmpUrl,
                    controller: tmpCtrl,
                    size: "formDialogSize"//,
                    //backdrop: 'static'
                });
                return modalInstance;
            },
            showBackTemplate: function () {
                var modalInstance = $uibModal.open({
                    templateUrl: "views/backDialogModel.html",
                    controller: "backDialogCtrl",
                    backdrop: 'static'
                });
                return modalInstance;
            },
            logoCrop: function (img) {
                var modalInstance = $uibModal.open({
                    templateUrl: 'views/logoCrop.html',
                    controller: 'logoCropCtrl',
                    backdrop: 'static',
                    //size: "formDialogSize",
                    resolve: {
                        img: function () { return img }
                    }
                });
                //modalInstance.result.then(function () {
                //    fun();
                //}, function () {

                //});
                return modalInstance;
            }
        };
}])
.service('toasterService', ['toaster', function (toaster) {
    return {
        success: function (data) {
            toaster.success({ title: data, body: "" });
        },
        error: function (data) {
            toaster.error({ title: data, body: "" });
        },
        warning: function (data) {
            toaster.warning({ title: data, body: "" });
        },
        info: function (data) {
            toaster.info({ title: data, body: "" });
        },
    };
}])
    ;