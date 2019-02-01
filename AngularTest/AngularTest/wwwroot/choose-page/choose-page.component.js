angular.
module('choosePage').
component('choosePage',{
    templateUrl:'choose-page/choose-page.template.html',
    controller: ['$scope', '$http', '$location', function ChoosePageCtrl($scope, $http, $location) {
        var today = new Date();
        today.toLocaleDateString();

        /**
         * clear up 'phoneList'
         * @param {any} phoneList
         */
        function formatPhoneList(phoneList) {
            var stateStringArray = ['in using', 'abandoned', 'deleted'];
            var operateArray = [['replace', 'nothing', 'nothing'], ['to abandon', 'to using', 'nothing'], ['delele', 'delete', 'nothing']];
            $scope.pageIndex = phoneList.pageIndex;
            $scope.totalPages = phoneList.totalPages;
            $scope.phoneList = phoneList.list;
            if ($scope.totalPages == 0) {
                $scope.notEmpty = false;
            } else {
                $scope.notEmpty = true;
            }
            for (var i = 0; i < $scope.phoneList.length; i++) {
                var state = $scope.phoneList[i].state;
                $scope.phoneList[i].stateString = stateStringArray[state];
                $scope.phoneList[i].operate1 = operateArray[0][state - 1];
                $scope.phoneList[i].operate2 = operateArray[1][state - 1];
                $scope.phoneList[i].operate3 = operateArray[2][state - 1];
                if ($scope.phoneList[i].deleteDate == "0001-01-01T00:00:00") {
                    $scope.phoneList[i].deleteDate = "";
                }
            }
        }

        /**
         * get 'ChoosePageViewModel'
         * @param {any} pageIndex
         */
        $scope.getChoosePageViewModel = function (pageIndex) {
            $http({
                method: 'GET',
                params: ({
                    pageIndex: pageIndex
                }),
                url: '/api/ChoosePage/GetChoosePageViewModel',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                $scope.choosePageViewModel = response.data;
                var model = $scope.choosePageViewModel;
                if (!model.isLogin) {
                    $location.url('/phone/errorPage');
                } else {
                    $scope.loginUsername = model.loginUsername;
                    $scope.loginUserId = model.loginUserId;
                    formatPhoneList(model.phoneList);

                }
            }, function error(response) {
            });
        }
        $scope.getChoosePageViewModel(1);    // default select the first page

        /**
         * logout
         * @param {any} pageIndex
         */
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
                });
            }
        }

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

        /*
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
            });
        }
        */

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