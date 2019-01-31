angular.
module('common').
component('errorPage', {

    templateUrl: 'common/error-page.template.html',
    controller: ['$scope', function ChoosePageCtrl($scope) {
        $scope.checkLogin = function () {   // –ËÃ·»°
            $http({
                method: 'GET',
                params: ({
                }),
                url: '/api/Phone/CheckLogin',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                if (response.data['notLogin'] == 'true') {
                    $location.url('/#!/phone');
                } else {
                    $scope.loginUsername = response.data;
                }
            }, function error(response) {
                alert("error");
            });
        }
        //$scope.checkLogin();
    }]

})