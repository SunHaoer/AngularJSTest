angular.
module('common').
component('successPage', {

    templateUrl: 'common/success-page.template.html',
    controller: ['$scope', '$timeout', '$http', '$location', function ChoosePageCtrl($scope, $timeout, $http, $location) {

        //$timeout(function() {
        //    $location.url('/phone/choosePage');
        //}, 50000);

        $scope.checkLogin = function () {   // –ËÃ·»°
            $http({
                method: 'GET',
                params: ({
                }),
                url: '/api/Phone/CheckLogin',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                if (response.data['notLogin'] == 'true') {
                    $location.url('/phone/errorPage');
                }
            }, function error(response) {
                alert("error");
            });
        }
        $scope.checkLogin();

    }]
})