angular.
    module('loginPage').
    component('loginPage', {
        templateUrl: 'login-page/login-page.template.html',
        controller: ['$scope', '$http', '$location', function LoginpageCtrl($scope, $http, $location) {
            $scope.usernameReg = '[a-zA-Z0-9]*';

            //Verify user name and password
            $scope.login = function () {
                var info = $scope.info;
                $http({
                    method: 'GET',
                    url: '/api/Login/Login',
                    params: ({
                        username: info.username,
                        password: info.password
                    }),
                }).then(function success(response) {
                    $scope.loginPageViewModel = response.data;
                    var model = $scope.loginPageViewModel;
                    if (model.isLegal) {
                        //$scope.initDB();
                        $location.url('/phone/choosePage');
                    } else {
                        alert(model.isLegal);
                    }
                }), function error(response) {
                    alert('error');
                }
            }

            //Reset input
            $scope.info = {};
            $scope.reset = function () {
                $scope.info.username = '';
                $scope.info.password = '';
            }
        }]
    })