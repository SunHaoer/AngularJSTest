angular.
module('choosePage').
component('choosePage',{
    templateUrl:'choose-page/choose-page.template.html',
    controller: ['$scope', '$http', '$location', function ChoosePageCtrl($scope, $http, $location) {
        var yalertStylePath = 'css/yalert.css';
        var today = new Date();
        today.toLocaleDateString();

        /*
         * clear up 'phoneList'
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
                $scope.phoneList[i].stateString = stateStringArray[state - 1];
                $scope.phoneList[i].operate1 = operateArray[0][state - 1];
                $scope.phoneList[i].operate2 = operateArray[1][state - 1];
                $scope.phoneList[i].operate3 = operateArray[2][state - 1];
                if ($scope.phoneList[i].deleteDate == '0001-01-01T00:00:00') {
                    $scope.phoneList[i].deleteDate = '';
                }
            }
        }

        /*
         * get 'ChoosePageViewModel'
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
                if (model.isLogin && model.isVisitLegal) {
                    $scope.loginUsername = model.loginUsername;
                    $scope.loginUserId = model.loginUserId;
                    formatPhoneList(model.phoneList);
                } else {
                    showAlert('hint', 'not login or illegal visit', yalertStylePath, '');
                    $location.url('/phone/errorPage');
                }
            }, function error(response) {
            });
        }
        $scope.getChoosePageViewModel(1);    // default select the first page

        /*
         * logout
         */
        $scope.logout = function () {
            showConfirm('', ' Are you sure logout?', yalertStylePath, function () {
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
            }, function () {
            })
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
        }

        /*
         * Turn to addNewPhonePage 
         */
        $scope.AddNewPhone = function () {
            $http({
                method: 'GET',
                params: ({
                }),
                url: '/api/ChoosePage/SetIsSubmit',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                if (response.data.isSuccess) {
                    $location.url('/phone/registerPage');
                }
            }, function error(response) {
            });
        }

        function setUsingToAbandon(id) {
            showConfirm('', 'are you sure to abandon the phone ?', yalertStylePath, function () {
                $http({
                    method: 'GET',
                    params: ({
                        id: id,
                        abandonDate: new Date(today)
                    }),
                    url: '/api/ChoosePage/SetUsingToAbandonById',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    //showAlert('', 'success!', yalertStylePath, '')
                }, function error(response) {
                });
                location.reload();
            }, function () {
            })
        }

        function setAbandonToUsing(id) {
            showConfirm('', 'using?', yalertStylePath, function () {
                $http({
                    method: 'GET',
                    params: ({
                        id: id,
                        startDate: new Date(today)
                    }),
                    url: '/api/ChoosePage/SetAbanddonToUsingById',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                   
                }, function error(response) {
                });
                location.reload();
            }, function () {
            })
        }

        /*
         * to abandon or to using 
         */
        $scope.abandon = function (id, state) {
            if (state == 3) {
                //alert('The phone is already delete!');
            } else if (state == 1) {
                setUsingToAbandon(id);
            } else if (state == 2) {
                setAbandonToUsing(id);
            }
            //location.reload();
        }

        $scope.replace = function (id, state) {
            if (state == 1) {
                $http({
                    method: 'GET',
                    params: ({
                        id: id
                    }),
                    url: '/api/ChoosePage/SetTempOldPhoneById',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $location.url('/phone/replacePage');
                }, function error(response) {
                });
            } else if (state == 2) {
                alert('The phone is already abandon!')
            } else {
                alert('The phone is already delete!');
            }
        }

        $scope.delete = function (id, state) {
            if (state != 3) {
                $http({
                    method: 'GET',
                    params: ({
                        id: id
                    }),
                    url: '/api/ChoosePage/SetTempNewPhoneById',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $location.url('/phone/deletePage');
                }, function error(response) {
                });
            } else {
                alert('The phone is already delete!');
            }
        }
    }]
});