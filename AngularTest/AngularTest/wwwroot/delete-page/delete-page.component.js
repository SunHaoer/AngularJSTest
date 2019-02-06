angular.
    module("deletePage").
    component("deletePage", {
        templateUrl: "delete-page/delete-page.template.html",
        controller: ["$scope", "$http", "$location", function deletePageCtrl($scope, $http, $location) {
            $scope.today = new Date();
            $scope.today.toLocaleDateString();//获取当前日期

            /*
             * get 'ReplacePhoneModel'
             */
            $scope.getDeletePhonePageViewModel = function () {
                $http({
                    method: 'GET',
                    params: ({
                    }),
                    url: '/api/DeletePhone/GetDeletePhonePageViewModel',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $scope.deletePhonePageViewModel = response.data;
                    var model = $scope.deletePhonePageViewModel;
                    if (model.isLogin) {
                        $scope.deleteReasonList = model.deleteReasonList;
                        $scope.phone = model.tempNewPhone;
                        if (model.tempNewPhone.startDate == "0001-01-01T00:00:00") {
                            $scope.phone.startDate = new Date($scope.today);
                        } else {
                            $scope.phone.startDate = new Date(model.tempNewPhone.startDate);
                        }
                        if (model.tempNewPhone.abandonDate == "0001-01-01T00:00:00") {
                            $scope.phone.abandonDate = null;
                        } else {
                            $scope.phone.abandonDate = new Date($scope.phone.abandonDate);
                        }
                        if (model.tempNewPhone.deleteDate == "0001-01-01T00:00:00") {
                            $scope.phone.deleteDate = new Date($scope.today);
                        } else {
                            $scope.phone.deleteDate = new Date(model.tempNewPhone.deleteDate);
                        }
                        $scope.phone.deleteReason = model.tempNewPhone.deleteReason;
                    } else {
                        alert('not login');
                        $location.url('/phone/errorPage');
                    }
                }, function error(response) {
                });
            }
            $scope.getDeletePhonePageViewModel();

            $scope.isDeleteDateLegal = true;
            $scope.validateDateLegal = function () {
                var date1 = $scope.phone.deleteDate;
                var date2 = $scope.today;
                if (date1.getFullYear() < 1900 || date1.getFullYear() > 2100) {
                    $scope.isDeleteDateLegal = false;
                } else if (date1.getFullYear() != date2.getFullYear()) {
                    $scope.isDeleteDateLegal = date1.getFullYear() > date2.getFullYear();
                } else if (date1.getMonth() != date2.getMonth()) {
                    $scope.isDeleteDateLegal = date1.getMonth() > date2.getMonth();
                } else {
                    $scope.isDeleteDateLegal = date1.getDate() >= date2.getDate();
                }
            }

            /*
             * validate 'deleleReason' not empty 
             */
            $scope.isDeleteReasonLegal = true;
            $scope.validateDeleteReasonNotEmpty = function () {
                var deleteReason = $scope.phone.deleteReason;
                var otherReason = $scope.phone.otherReason;
                if (deleteReason == '' || deleteReason == null) {
                    $scope.isDeleteReasonLegal = false;
                } else if (deleteReason == 'other' && (otherReason == '' || otherReason == null)) {
                    $scope.isDeleteReasonLegal = false;
                } else {
                    if (deleteReason == 'other') {
                        $scope.phone.deleteReason = otherReason;
                    }
                    $scope.isDeleteReasonLegal = true;
                }
                alert($scope.isDeleteReasonLegal);
            }

            /*
             * submit
             */
            $scope.submitMsg = function () {
                $scope.validateDateLegal();
                $scope.validateDeleteReasonNotEmpty();
                if ($scope.isDeleteDateLegal && $scope.isDeleteReasonLegal) {
                    var phone = $scope.phone;
                    $http({
                        method: 'GET',
                        params: ({
                            deleteReason: phone.deleteReason,
                            deleteDate: phone.deleteDate
                        }),
                        url: '/api/DeletePhone/SubmitMsg',
                        headers: { 'Content-Type': 'application/json' }
                    }).then(function success(response) {
                        if (response.data.isSuccess) {
                            alert('success');
                            $location.url('phone/doubleCheck');
                        } else {
                            alert('not legal');
                        }
                    }, function error(response) {
                    });
                }
            }

            $scope.backToIndex = function () {
                if (confirm('Back to index? Data will not be saved')) {
                    $location.path('/phone/choosePage');
                }
            }
        }]
    });
