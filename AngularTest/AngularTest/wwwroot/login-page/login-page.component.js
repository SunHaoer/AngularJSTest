angular.
    module('loginPage').
    component('loginPage', {
        templateUrl: 'login-page/login-page.template.html',
        controller: ['$scope', '$http', '$location', function LoginpageCtrl($scope, $http, $location) {

            $scope.usernameReg = '[a-zA-Z0-9]*';
/*
            $scope.initPhoneDB = function () {
                $http({
                    method: 'GET',
                    url: '/api/Phone/InitPhoneDB',
                    params: ({

                    })
                }).then(function success(response) {
                    //alert(response.data);
                }, function error(response) {
                    //alert('error');
                })
            }
*/
            $scope.initTypeYearDB = function () {
                $http({
                    method: 'GET',
                    url: '/api/TypeYear/initTypeYearDB',
                    params: ({

                    })
                }).then(function success(response) {
                    //alert(response.data);
                }, function error(response) {
                    //alert('error');
                })
            }

            $scope.InitBrandModelDB = function () {
                $http({
                    method: 'GET',
                    url: '/api/BrandModel/InitBrandModelDB',
                    params: ({

                    })
                }).then(function success(response) {
                    //alert(response.data);
                }, function error(response) {
                    //alert('error');
                })
            }

            $scope.InitBrandTypeModelDB = function () {
                $http({
                    method: 'GET',
                    url: '/api/BrandTypeModels/InitBrandTypeModelDB',
                    params: ({

                    })
                }).then(function success(response) {
                    //alert(response.data);
                }, function error(response) {
                    //alert('error');
                })
            }

            $scope.InitBrandTypeProductNoDB = function () {
                $http({
                    method: 'GET',
                    url: '/api/BrandTypeProductNo/InitBrandTypeProductNoDB',
                    params: ({

                    })
                }).then(function success(response) {
                    //alert(response.data);
                }, function error(response) {
                    alert('error');
                })
            }

            $scope.initDB = function () {
                $scope.initPhoneDB();
                $scope.initTypeYearDB();
                $scope.InitBrandModelDB();
                $scope.InitBrandTypeModelDB();
                $scope.InitBrandTypeProductNoDB();
            }

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