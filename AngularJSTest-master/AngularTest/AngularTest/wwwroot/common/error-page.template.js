angular.
module('common').
component('errorPage', {
    templateUrl: 'common/error-page.template.html',
    controller: ['$scope', '$http', '$location', function ChoosePageCtrl($scope, $http, $location) {
        var yalertStylePath = 'css/yalert.css';

        /*
         * get 'ErrorPageViewModel'
         */
        $scope.getErrorPageViewModel = function () {
            $http({
                method: 'GET',
                params: ({
                }),
                url: '/api/ErrorPage/GetErrorPageViewModel',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                $scope.errorPageViewModel = response.data;
                var model = $scope.errorPageViewModel;
                if (model.isLogin) {
                    $scope.isLogin = true;
                } else {
                    $scope.isLogin = false;
                }
            }, function error(response) {
            });
        }
        $scope.getErrorPageViewModel();    

        $scope.turnToIndex = function () {
            $http({
                method: 'GET',
                params: ({
                }),
                url: '/api/ErrorPage/SetIsSubmit',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                if (response.data.isSuccess) {
                    $location.url('/phone/choosePage');
                }
            }, function error(response) {
            });
        }

    }]
})