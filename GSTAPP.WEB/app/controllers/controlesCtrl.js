'use strict';

angular.module('GSTApp')
.controller("controlesCtrl", ['$scope', '$state', '$timeout', '$window', 'ApiFactory', 'toasterService', function (scope, state, timeout, window, ApiFactory, toasterService) {

    //$(document).ready(function () {
    //    $('#example').DataTable();
    //});
    //scope.pop = function () {
    //    toasterService.success("hai");
    //    toasterService.error("hai");
    //    toasterService.warning("hai");
    //    toasterService.info("hai");
    //};

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
}]);