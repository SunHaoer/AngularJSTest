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
                for (var i = 0; i < $scope.userPhoneAll.length; i++) {
                    //console.log($scope.userPhoneAll[i].id + " " + $scope.userPhoneAll[i].state + '\n')
                    if ($scope.userPhoneAll[i].state == 1) $scope.userPhoneAll[i].stateString = 'using';
                    else $scope.userPhoneAll[i].stateString = 'abandon';
                    if ($scope.userPhoneAll[i].deleteDate == "0001-01-01T00:00:00")
                        $scope.userPhoneAll[i].deleteDate = "";
                }
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

        $scope.stateFilter = function (phoneState) {

            console.log(phoneState);

            if (phoneState == 1) {
                $scope.state = 'using';
            }
            else {
                $scope.state = 'abandon';
            }

            console.log($scope.state);

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