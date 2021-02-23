'use strict';

angular.module('GSTApp')
.directive("dialogForm", function () {
    return {
        template: '<div class="modal-header">' +
                  '    <button class="btn btn-warning pull-right" ng-click="cancelDialog()"><span class="glyphicon glyphicon-remove"></span></button>' +
                  '</div>'
    };
});
////.directive('JQDataTable', function () {
////    return {
////        // angular passes the element reference to you
////        //compile: function(element) {
////            //$(element).DataTable();
////        //}
////    }
////});