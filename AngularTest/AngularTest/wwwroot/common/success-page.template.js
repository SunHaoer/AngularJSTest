angular.
module('common').
component('successPage', {
    templateUrl: 'common/success-page.template.html',
    controller: ['$scope', '$timeout', '$http', '$location', function ChoosePageCtrl($scope, $timeout, $http, $location) {

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
                    alert('not login or illegal visit');
                    $location.url('/phone/errorPage');
                } 
            }, function error(response) {
            });
        }
        $scope.getSuccessPageViewModel();    
    }]
})