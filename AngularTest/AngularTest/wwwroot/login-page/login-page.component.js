angular.
    module('loginPage').
    component('loginPage', {
        templateUrl: 'login-page/login-page.template.html',
        controller: ['$scope', '$http', '$location', function LoginpageCtrl($scope, $http, $location) {
            $scope.usernameReg = '[a-zA-Z0-9]*';
            $scope.isOK = true;

            $scope.initLogin = function () {
                $http({
                    method: 'GET',
                    url: '/api/Login/InitLogin',
                    params: ({
                    }),
                }).then(function success(response) {
                }), function error(response) {
                }
            }
            $scope.initLogin();

            //Verify user name and password
            $scope.login = function () {
                var info = $scope.info;
                $http({
                    method: 'POST',
                    url: '/api/Login/Login',
                    params: ({
                        username: info.username,
                        password: info.password
                    }),
                }).then(function success(response) {
                    $scope.loginPageViewModel = response.data;
                    var model = $scope.loginPageViewModel;
                    if (model.isLegal) {
                        $location.url('/phone/choosePage');
                    } else {
                        $scope.isOK = false;
                    }
                }), function error(response) {
                    //alert('error');
                }
            }

            $scope.changeIsOK = function () {
                $scope.isOK = true;
            }

            //Reset input
            $scope.info = {};
            $scope.reset = function () {
                $scope.info.username = '';
                $scope.info.password = '';
            }
        }]
    })