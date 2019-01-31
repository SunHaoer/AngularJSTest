angular.
module('choosePage').
component('choosePage',{
    templateUrl:'choose-page/choose-page.template.html',
    controller: ['$scope', '$http', '$location', function ChoosePageCtrl($scope, $http, $location) {
        $scope.notEmpty = false;
        $scope.myDate = new Date();
        $scope.myDate.toLocaleDateString();//获取当前日期

        $scope.checkLogin = function () {   // 需提取
            $http({
                method: 'GET',
                params: ({
                }),
                url: '/api/Phone/CheckLogin',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                if (response.data['notLogin'] == 'true') {
                    $location.url('/phone/errorPage');
                    //alert('Not logged in will back to the home page');
                } 
            }, function error(response) {
                //alert("error");
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
                //alert("error");
            });
        }
        $scope.GetLoginUsername();

        $scope.getPageIndex = function () {
            $http({
                method: 'GET',
                params: ({

                }),
                url: '/api/TempPhone/GetTempPageIndex',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                $scope.pageIndex = response.data;
                //alert('=' + $scope.pageIndex);
            }, function error(response) {
                //alert("error");
            });
        }
        //$scope.getPageIndex();

        $scope.setPageIndex = function () {
            $http({
                method: 'GET',
                params: ({
                    pageIndex: $scope.pageIndex
                }),
                url: '/api/TempPhone/SetTempPageIndex',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                $scope.pageIndex = response.data;
            }, function error(response) {
                //alert("error");
            });
        }

        $scope.register = function () {
            $scope.setPageIndex();
            $http({
                method: 'POST',
                params: ({

                }),
                url: '/api/TempPhone/SetNewTempPhone',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                $location.url('/phone/registerPage');
            }, function error(response) {
                //alert("error");
            });
        }

        $scope.logout = function () {
            if (confirm('logout?')) {
                $http({
                    method: 'GET',
                    params: ({

                    }),
                    url: '/api/Login/Logout',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $location.url('/#!/phone');
                }, function error(response) {
                    //alert("error");
                });
            }
        }

        $scope.getUserPhoneAll = function (pageIndex) {
            $http({
                method: 'GET',
                params: ({
                    pageIndex: pageIndex
                }),
                url: '/api/Phone/GetUserPhoneAll',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                $scope.userPhoneAll = response.data.list;
                //alert(response.data.pageIndex);
                //alert(response.data.totalPages);
                $scope.totalPages = response.data.totalPages;
                $scope.pageIndex = response.data.pageIndex;
                $scope.totalPagesList = new Array();
                for (var i = 0; i < $scope.totalPages; i++) {
                    $scope.totalPagesList[i] = i;
                }
                if ($scope.userPhoneAll.length == 0) {
                    $scope.notEmpty = false;
                } else {
                    $scope.notEmpty = true;
                }
                for (var i = 0; i < $scope.userPhoneAll.length; i++) {
                    if ($scope.userPhoneAll[i].state == 1) {
                        $scope.userPhoneAll[i].stateString = 'in using';
                        $scope.userPhoneAll[i].operate1 = 'replace';
                        $scope.userPhoneAll[i].operate2 = 'to abandon';
                        $scope.userPhoneAll[i].operate3 = 'delele';
                    } else if($scope.userPhoneAll[i].state == 2) {
                        $scope.userPhoneAll[i].stateString = 'abandoned';
                        $scope.userPhoneAll[i].operate1 = 'nothing';
                        $scope.userPhoneAll[i].operate2 = 'to using';
                        $scope.userPhoneAll[i].operate3 = 'delete';
                    } else if ($scope.userPhoneAll[i].state == 3) {
                        $scope.userPhoneAll[i].stateString = 'deleted';
                        $scope.userPhoneAll[i].operate1 = 'nothing';
                        $scope.userPhoneAll[i].operate2 = 'nothing';
                        $scope.userPhoneAll[i].operate3 = 'nothing';
                    }
                    if ($scope.userPhoneAll[i].deleteDate == "0001-01-01T00:00:00") {
                        $scope.userPhoneAll[i].deleteDate = "";
                    }
                }
            }, function error(response) {
                //alert("error");
            });
        }
        $scope.getUserPhoneAll(1);

        $scope.abandon = function (id, state) {
            if (state == 3) {
                alert('The phone is already delete!');
            } else if (state == 1) {
                if (confirm('Abandon?')) {
                    $http({
                        method: 'POST',
                        params: ({
                            id: id,
                            abandonDate: new Date($scope.myDate)
                        }),
                        url: '/api/Phone/AbandonUserPhoneById',
                        headers: { 'Content-Type': 'application/json' }
                    }).then(function success(response) {
                        //$location.url('/phone/deletePage');
                        alert('Abandon Success!');
                    }, function error(response) {
                        //alert("error");
                    });
                }

            } else if (state == 2) {
                if (confirm('Using?')) {
                    $http({
                        method: 'POST',
                        params: ({
                            id: id,
                            startDate: new Date($scope.myDate)
                        }),
                        url: '/api/Phone/UsingPhoneById',
                        headers: { 'Content-Type': 'application/json' }
                    }).then(function success(response) {
                        //$location.url('/phone/deletePage');
                        alert('Using Success!');
                    }, function error(response) {
                        //alert("error");
                    });
                }
            }
            location.reload();
        }
        
        $scope.delete = function (id, state) {
            $scope.setPageIndex();
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
                    //alert("error");
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
            $scope.setPageIndex();
            if (state == 1) {
                $http({
                    method: 'POST',
                    params: ({

                    }),
                    url: '/api/TempPhone/SetNewTempPhone',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {

                }, function error(response) {
                    //alert("error");
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
                    //alert("error");
                });
            } else if (state == 2) {
                alert('The phone is already abandon!')
            } else {
                alert('The phone is already delete!');
            }
        }

        $scope.tempId = null;
        $scope.showDetails = function (phone) {
            //alert(1);
            if ($scope.tempId == null) {
                $scope.showDetail = true;
            } else if ($scope.tempId == phone.id) {
                $scope.showDetail = $scope.showDetail == false ? true : false;
            } else {
                $scope.showDetail = true;
            }
            //alert(2);
            $scope.tempId = phone.id;
            $scope.phoneUser = phone.phoneUser;
            $scope.brand = phone.brand;
            $scope.type = phone.type;
            $scope.productNo = phone.productNo;
            $scope.startDate = phone.startDate;
            $scope.endDate = phone.endDate;
            $scope.abandonDate = phone.abandonDate;
            if ($scope.abandonDate == '0001-01-01T00:00:00') {
                $scope.abandonDate = '';
            }
            $scope.deleteDate = phone.deleteDate;
            $scope.deleteReason = phone.deleteReason;
            $scope.state = phone.state;
            $scope.stateString = phone.stateString;
            $scope.operate1 = phone.operate1;
            $scope.operate2 = phone.operate2;
            $scope.operate3 = phone.operate3;
            //alert(3);
        }
    }]
});