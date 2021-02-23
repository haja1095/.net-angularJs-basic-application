'use strict';

angular.module('GSTApp')
.controller("dialogModelCtrl", ['$scope', '$uibModalInstance', function (scope, uibModalInstance) {

    scope.ok = function () {
        uibModalInstance.close();
    };

    scope.cancel = function () {
        uibModalInstance.dismiss('cancel');
    };
}])
    .controller("logoutDialogModelCtrl", ['$scope', '$uibModalInstance', function (scope, uibModalInstance) {

        scope.ok = function () {
            uibModalInstance.close();
        };

        scope.cancel = function () {
            uibModalInstance.dismiss('cancel');
        };
    }])
.controller("logoCropCtrl", ['$scope', '$uibModalInstance','img', function (scope, uibModalInstance,img) {

    scope.img = img;
    scope.resultImage = img;
    scope.getCropperApi = function (api) {
        api.zoomIn(3);
        api.zoomOut(2);
        api.rotate(270);
        api.fit();
        scope.resultImage = api.crop();
    };
    scope.myButtonLabels = {
        rotateLeft: '<i class="glyphicon glyphicon-repeat gly-flip-horizontal"></i>',
        rotateRight: '<i class="glyphicon glyphicon-repeat"></i>',
        zoomIn: '<i class="glyphicon glyphicon-zoom-in"></i>',
        zoomOut: '<i class="glyphicon glyphicon-zoom-out"></i>',
        fit: '<i class="glyphicon glyphicon-screenshot"></i>',
        crop: '<i class="glyphicon glyphicon-ok"></i>' // You can pass html too.
    }
    scope.updateResultImage = function(base64) {
        scope.resultImage = base64;
        scope.$apply(); // Apply the changes.
    };

    scope.ok = function () {
        $(".imgCropper-controls button")[3].click();
        uibModalInstance.close(scope.resultImage);
    };

    scope.cancel = function () {
        uibModalInstance.dismiss('cancel');
    };
    angular.element(document).ready(function () {
        $(".imgCropper-controls button").addClass("btn btn-default").css("width", "20%").click(function () {
            if ($(this) != $(".imgCropper-controls button")[3]) {
                $(".imgCropper-controls button")[3].click();
            }
        });
        $($(".imgCropper-controls button")[3]).hide();
    });
    scope.setsaveimg = function () {
        $(".imgCropper-controls button")[3].click();
    }
}])
.controller("backDialogCtrl", ['$scope', '$uibModalInstance', function (scope, uibModalInstance) {

    scope.ok = function () {
        uibModalInstance.close();
    };

    scope.cancel = function () {
        uibModalInstance.dismiss('cancel');
    };
}])
;