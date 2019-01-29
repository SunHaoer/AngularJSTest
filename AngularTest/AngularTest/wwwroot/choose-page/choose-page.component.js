angular.
module('choosePage').
component('choosePage',{
    templateUrl:'choose-page/choose-page.template.html',
    controller: ['$scope', '$http', '$location', function ChoosePageCtrl($scope, $http, $location) {
        $scope.notEmpty = false;


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
        $scope.checkLogin();

        $scope.GetLoginUsername = function () {
            $http({
                method: 'GET',
                params: ({
                    
                }),
                url: '/api/Phone/GetLoginUsername',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                $scope.loginUsername = response.data;
            }, function error(response) {
                alert("error");
            });
        }
        $scope.GetLoginUsername();

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

        $scope.logout = function () {
            $http({
                method: 'GET',
                params: ({

                }),
                url: '/api/Login/Logout',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                $location.url('/#!/phone');
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
                if ($scope.userPhoneAll.length == 0) {
                    $scope.notEmpty = false;
                } else {
                    $scope.notEmpty = true;
                }
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

        $scope.abandon = function (id, state) {
            if (state == 3) {
                alert('The phone is already delete!');
            } else if (state == 1) {
                if (confirm('Abandon?')) {
                    $http({
                        method: 'POST',
                        params: ({
                            id: id
                        }),
                        url: '/api/Phone/AbandonUserPhoneById',
                        headers: { 'Content-Type': 'application/json' }
                    }).then(function success(response) {
                        //$location.url('/phone/deletePage');
                        alert('Abandon Success!');
                    }, function error(response) {
                        alert("error");
                    });
                }

            } else if (state == 2) {
                if (confirm('Using?')) {
                    $http({
                        method: 'POST',
                        params: ({
                            id: id
                        }),
                        url: '/api/Phone/UsingPhoneById',
                        headers: { 'Content-Type': 'application/json' }
                    }).then(function success(response) {
                        //$location.url('/phone/deletePage');
                        alert('Using Success!');
                    }, function error(response) {
                        alert("error");
                    });
                }
            }
            location.reload();
        }
        
        $scope.delete = function (id, state) {
            if (state != 3) {
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
        $scope.replace = function (id, state) {
            if (state == 1) {
                $http({
                    method: 'POST',
                    params: ({

                    }),
                    url: '/api/TempPhone/SetNewTempPhone',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {

                }, function error(response) {
                    alert("error");
                });

                $http({
                    method: 'POST',
                    params: ({
                        id: id
                    }),
                    url: '/api/TempPhone/SetOldTempPhoneById',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    //alert('/phone/replacePage');
                    $location.url('/phone/replacePage');
                }, function error(response) {
                    alert("error");
                });
            } else if (state == 2) {
                alert('The phone is already abandon!')
            } else {
                alert('The phone is already delete!');
            }
        }

        $scope.tempId = null;
        $scope.showDetails = function (phone) {
            if ($scope.tempId == null) {
                $scope.showDetail = true;
            } else if ($scope.tempId == phone.id) {
                $scope.showDetail = $scope.showDetail == false ? true : false;
            } else {
                $scope.showDetail = true;
            }
            $scope.tempId = phone.id;
            $scope.phoneUser = phone.phoneUser;
            $scope.brand = phone.brand;
            $scope.type = phone.type;
            $scope.productNo = phone.productNo;
            $scope.startDate = phone.startDate;
            $scope.endDate = phone.endDate;
            $scope.abandonDate = phone.abandonDate;
            $scope.deleteDate = phone.deleteDate;
            $scope.deleteReason = phone.deleteReason;
            $scope.stateString = phone.stateString;
        }
    }]
});