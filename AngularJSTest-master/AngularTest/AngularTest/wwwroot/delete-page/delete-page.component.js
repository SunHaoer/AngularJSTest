angular.
    module("deletePage").
    component("deletePage", {
        templateUrl: "delete-page/delete-page.template.html",
        controller: ["$scope", "$http", "$location", function deletePageCtrl($scope, $http, $location) {
            var yalertStylePath = 'css/yalert.css';
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
                    if (model.isLogin && model.isVisitLegal) {
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
                        var isMatching = false;    // judge is ot not is 'otherReason'
                        for (var i = 0; i < $scope.deleteReasonList.length; i++) {
                            if ($scope.phone.deleteReason == $scope.deleteReasonList[i].deleteReasonName) {
                                isMatching = true;
                                break;
                            }
                        }
                        if (!isMatching && $scope.phone.deleteReason != null && $scope.phone.deleteReason != '') {
                            $scope.phone.otherReason = $scope.phone.deleteReason;
                            $scope.phone.deleteReason = 'other';
                        }
                    } else {
                        showAlert('hint', 'not login or illegal visit', yalertStylePath, '');
                        $location.url('/phone/errorPage');
                    }
                }, function error(response) {
                });
            }
            $scope.getDeletePhonePageViewModel();

            function validateDate(date1, date2) {
                var flag = true;
                if (date1.getFullYear() < 1900 || date1.getFullYear() > 2100) {
                    flag = false;
                } else if (date1.getFullYear() != date2.getFullYear()) {
                    flag = date1.getFullYear() > date2.getFullYear();
                } else if (date1.getMonth() != date2.getMonth()) {
                    flag = date1.getMonth() > date2.getMonth();
                } else {
                    flag = date1.getDate() >= date2.getDate();
                }
                return flag;
            }

            $scope.isDeleteDateLegal = true;
            $scope.validateDateLegal = function () {
                try {
                    $scope.isDeleteDateLegal = validateDate($scope.phone.deleteDate, $scope.today) && validateDate($scope.phone.deleteDate, $scope.phone.startDate);
                } catch {
                    $scope.isDeleteDateLegal = false;
                }
            }

            /*
             * validate 'deleleReason' not empty 
             */
            $scope.number = 0;
            $scope.isDeleteReasonLegal = true;
            $scope.validateDeleteReasonNotEmpty = function () {
                var deleteReason = $scope.phone.deleteReason;
                if (deleteReason == '' || deleteReason == null) {
                    $scope.isDeleteReasonLegal = false;
                } else {
                    $scope.isDeleteReasonLegal = true;
                }
                if ($scope.number == 1 && $scope.phone.deleteReason == 'other' && ($scope.phone.otherReason == '' || $scope.phone.otherReason == null)) {
                    $scope.isDeleteReasonLegal = false;
                    //$scope.number = 0;
                }
                if ($scope.number == 0 && $scope.phone.deleteReason == 'other' && ($scope.phone.otherReason == '' || $scope.phone.otherReason == null)) {
                    $scope.number = 1;
                }
            }
            $scope.Rea = false;
            $scope.validateOtherReasonNotEmpty = function () {

                if ($scope.Rea == true) $scope.isDeleteReasonLegal = true;
                else if ($scope.phone.deleteReason == 'other' && ($scope.phone.otherReason == '' || $scope.phone.otherReason == null)) {
                    $scope.isDeleteReasonLegal = false;
                }
                else $scope.isDeleteReasonLegal = true;

            }
            $scope.theReasonIsEmpty = function () {

                $scope.isDeleteReasonLegal = true;
            }
            $scope.theReasonChange = function () {
                $scope.Rea = true;
            }
            $scope.theDateIsRight = function () {
                $scope.isDeleteDateLegal = true;
            }
            $scope.changeRea = function () {
                $scope.Rea = false;
            }
            $scope.validate = function () {
                $scope.validateDateLegal();
                $scope.validateDeleteReasonNotEmpty();
                $scope.validateOtherReasonNotEmpty();
                if ($scope.phone.deleteReason == null) $scope.isDeleteReasonLegal = false;
                $scope.isOK = $scope.isDeleteDateLegal && $scope.isDeleteReasonLegal;
            }

            /*
             * submit
             */
            $scope.isOK = true;
            $scope.submitMsg = function () {
                $scope.validate();
                if ($scope.isDeleteDateLegal && $scope.isDeleteReasonLegal) {
                    $scope.isOK = true;
                    var phone = $scope.phone;
                    $http({
                        method: 'POST',
                        params: ({
                            deleteReason: phone.deleteReason == 'other' ? phone.otherReason : phone.deleteReason,
                            deleteDate: phone.deleteDate,
                            state: phone.state
                        }),
                        url: '/api/DeletePhone/SubmitMsg',
                        headers: { 'Content-Type': 'application/json' }
                    }).then(function success(response) {
                        if (response.data.isSuccess) {
                            $location.url('phone/doubleCheck');
                        } else {
                            alert('not legal');
                            $scope.isOK = false;
                        }
                    }, function error(response) {
                    });
                } else {
                    $scope.isOK = false;
                }
            }

            $scope.backToIndex = function () {
                $http({
                    method: 'GET',
                    params: ({
                    }),
                    url: '/api/DeletePhone/SetIsSubmit',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    if (response.data.isSuccess) {
                        showConfirm('', 'Back to index? Data will not be saved', yalertStylePath, function () {
                            window.location.href = '#!/phone/choosePage';
                        }, function () {
                        })
                    }
                }, function error(response) {
                });
            }

        }]
    });
