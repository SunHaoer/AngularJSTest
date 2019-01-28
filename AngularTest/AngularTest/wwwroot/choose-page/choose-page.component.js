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
                url: '/api/TempPhone/SetNewTempPhone',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                $location.url('/phone/registerPage');
            }, function error(response) {
                alert("error");
            });
        }

        $scope.getUserPhoneAll = function () {
            $http({
                method: 'GET',
                params: ({

                }),
                url: '/api/Phone/GetUserPhoneAll',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                $scope.userPhoneAll = response.data;
                for (var i = 0; i < $scope.userPhoneAll.length; i++) {
                    if ($scope.userPhoneAll[i].state == 1) {
                        $scope.userPhoneAll[i].stateString = 'using';
                    } else if($scope.userPhoneAll[i].state == 2) {
                        $scope.userPhoneAll[i].stateString = 'abandoned';
                    } else if ($scope.userPhoneAll[i].state == 3) {
                        $scope.userPhoneAll[i].stateString = 'deleted';
                    }
                    if ($scope.userPhoneAll[i].deleteDate == "0001-01-01T00:00:00") {
                        $scope.userPhoneAll[i].deleteDate = "";
                    }
                }
            }, function error(response) {
                alert("error");
            });
        }
        $scope.getUserPhoneAll();
        
        $scope.remove = function (id, state) {
            if (state != 2) {
                $http({
                    method: 'POST',
                    params: ({
                        id: id
                    }),
                    url: '/api/TempPhone/SetNewTempPhoneById',
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
        /*
        $scope.stateFilter = function (phoneState) {
            if (phoneState == 1) {
                $scope.state = 'using';
            } else if (phoneState == 2) {
                $scope.state = 'abandon';
            } else if (phoneState == 3) {
                $scope.state = 'delelte';
            }
        } 
        */
        $scope.update = function(id) {
            $http({
                method: 'POST',
                params: ({
                    id: id
                }),
                url: '/api/TempPhone/SetNewTempPhoneById',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                $location.url('/phone/replacePage');
            }, function error(response) {
                alert("error");
            });
        }

    }]
});