angular.
    module("doubleCheck").
    component("doubleCheck", {
        templateUrl: 'DoubleCheck-page/recheck.html',
        controller: ['$scope', '$http', '$location', function DeleteDoubleCtrl($scope, $http, $location) {
            var yalertStylePath = 'css/yalert.css';

            /*
             * get 'DeletePhoneCheckPageViewModel'
             */
            $scope.getDeletePhoneCheckPageViewModel = function () {
                $http({
                    method: 'GET',
                    params: ({
                    }),
                    url: '/api/DeletePhoneCheck/GetDeletePhoneCheckPageViewModel',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $scope.deletePhoneCheckPageViewModel = response.data;
                    var model = $scope.deletePhoneCheckPageViewModel;
                    if (model.isLogin && model.isVisitLegal) {
                        $scope.phone = model.tempNewPhone;
                        $scope.phone.startDate = new Date(model.tempNewPhone.startDate);
                        $scope.phone.deleteDate = new Date(model.tempNewPhone.deleteDate);
                    } else {
                        showAlert('hint', 'not login or illegal visit', yalertStylePath, '');
                        $location.url('phone/errorPage');
                    }
                }, function error(response) {
                });
            }
            $scope.getDeletePhoneCheckPageViewModel();

            /* 
             * submit
             */ 
            $scope.submitMsg = function () {
                $http({
                    method: 'GET',
                    url: '/api/DeletePhoneCheck/SubmitMsg',
                    params: ({
                    }),
                    headers: { 'Content-Type': 'application/json' }
                }).then(function successCallback(response) {
                    $scope.formFeedbackViewModel = response.data;
                    var model = $scope.formFeedbackViewModel;
                    if (model.isSuccess) {
                        //alert('success');
                        $location.url('phone/successPage');
                    } else {
                        alert('not legal');
                    }
                }, function errorCallback(response) {
                    $location.url('phone/errorPage');
                });
            }

            $scope.cancle = function (value) {
                $http({
                    method: 'GET',
                    params: ({
                    }),
                    url: '/api/DeletePhoneCheck/SetIsSubmit',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    if (response.data.isSuccess) {
                        if (value == 1) {
                            showConfirm('', 'Back to index? Data will not be saved', yalertStylePath, function () {
                                window.location.href = '#!/phone/choosePage';
                            }, function () {
                            })
                        } else if (value == 2) {
                            window.location.href = '#!/phone/deletePage';
                        }
                    }
                }, function error(response) {
                });
            }

        }]
    });