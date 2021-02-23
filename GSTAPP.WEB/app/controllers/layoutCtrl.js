'use strict';

angular.module('GSTApp')
.controller("layoutCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', 'dialogModelService', function (scope, state, timeout, window, ApiFactory, dialogModelService) {
    scope.logout = function () {
        dialogModelService.showLogoutDialog().result.then(function () {
            state.go("login");
        });
    }
    if (window.localStorage.length > 0 && window.localStorage["Name"] !== null && window.localStorage["Name"] != "" && window.localStorage["Name"].length > 0) {
        scope.Name = window.localStorage["Name"];
    }
    else {
        state.go("login");
    }

    scope.labels = ["January", "February", "March", "April", "May", "June", "July"];
    scope.series = ['Series A', 'Series B'];
    scope.data = [
      [65, 59, 80, 81, 56, 55, 40],
      [28, 48, 40, 19, 86, 27, 90]
    ];
    scope.onClick = function (points, evt) {
        console.log(points, evt);
    };

    // Simulate async data update
    timeout(function () {
        scope.data = [
          [28, 48, 40, 19, 86, 27, 90],
          [65, 59, 80, 81, 56, 55, 40]
        ];
    }, 3000);

    scope.labels1 = ["Download Sales", "In-Store Sales", "Mail-Order Sales"];
    scope.data1 = [300, 500, 100];

    scope.labels2 = ["Download Sales", "In-Store Sales", "Mail-Order Sales"];
    scope.data2 = [300, 500, 100];


    scope.options = { colors: ['#803690', '#00ADF9', '#DCDCDC', '#46BFBD', '#FDB45C', '#949FB1', '#4D5360'] };
}]);