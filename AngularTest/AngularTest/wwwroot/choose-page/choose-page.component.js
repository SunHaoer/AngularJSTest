angular.
module('choosePage').
component('choosePage',{
    templateUrl:'choose-page/choose-page.template.html',
    controller: ['$scope', '$http', '$location', function ChoosePageCtrl($scope, $http, $location) {

        $scope.register = function () {
            $http({
                method: 'POST',
                params: ({

                }),
                url: '/api/DoubleCheck/SetTempPhone',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                $location.url('/phone/registerPage');
            }, function error(response) {
                alert("error");
            });
        }

       
        $scope.getUserPhoneAll = function () {
            //alert("haha");
            $http({
                method: 'GET',
                params: ({

                }),
                url: '/api/Phone/GetUserPhoneAll',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                $scope.userPhoneAll = response.data;
                //console.log($scope.userPhoneAll);
                //alert('srccess');
                //alert('+' + $scope.phone.life);
            }, function error(response) {
                alert("error");
            });
        }
        $scope.getUserPhoneAll();
        
        $scope.remove = function (id, state) {
            //alert(state);
            if (state != 2) {
                $http({
                    method: 'POST',
                    params: ({
                        id: id
                    }),
                    url: '/api/DoubleCheck/SetTempPhoneById',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $location.url('/phone/deletePage');
                }, function error(response) {
                    alert("error");
                });
            } else {
                alert('The phone is already delete!');
            }

        }

        $scope.update = function(id) {
            $http({
                method: 'POST',
                params: ({
                    id: id
                }),
                url: '/api/DoubleCheck/SetTempPhoneById',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                $location.url('/phone/replacePage');
            }, function error(response) {
                alert("error");
            });
        }

    }]
});