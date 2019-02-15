angular.
module('common').
component('successPage', {
    templateUrl: 'common/success-page.template.html',
    controller: ['$scope', '$timeout', '$http', '$location', function ChoosePageCtrl($scope, $timeout, $http, $location) {
        var yalertStylePath = 'css/yalert.css';

        $scope.getSuccessPageViewModel = function () {
            $http({
                method: 'GET',
                params: ({
                }),
                url: '/api/SuccessPage/GetSuccessPageViewModel',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                $scope.successPageViewModel = response.data;
                var model = $scope.successPageViewModel;
                if (!model.isLogin || !model.isVisitLegal) {
                    showAlert('hint', 'not login or illegal visit', yalertStylePath, '');
                    $location.url('/phone/errorPage');
                } 
            }, function error(response) {
            });
        }
        $scope.getSuccessPageViewModel();    

        $scope.turnToIndex = function () {
            $http({
                method: 'GET',
                params: ({
                }),
                url: '/api/SuccessPage/SetIsSubmit',
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