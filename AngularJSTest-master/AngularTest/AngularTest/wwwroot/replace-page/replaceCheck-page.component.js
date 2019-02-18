angular.
    module('replacePage').
    component('replaceCheckPage', {
        templateUrl: 'common/check-page.template.html',
        controller: ['$location', '$http', '$scope', function RegisterCheck($location, $http, $scope) {
            var yalertStylePath = 'css/yalert.css';
            $scope.isReplace = true;
            $scope.myDate = new Date();
            $scope.myDate.toLocaleDateString();

            /*
             * get 'ReplacePhoneCheckPageViewModel'
             */
            $scope.getReplacePhoneCheckPageViewModel = function () {
                $http({
                    method: 'GET',
                    params: ({
                    }),
                    url: '/api/ReplacePhoneCheck/GetReplacePhoneCheckPageViewModel',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $scope.replacePhoneCheckPageViewModel = response.data;
                    var model = $scope.replacePhoneCheckPageViewModel;
                    if (model.isLogin && model.isVisitLegal) {
                        $scope.phone = model.tempNewPhone;
                        $scope.oldPhone = model.tempOldPhone;
                    } else {
                        showAlert('hint', 'not login or illegal visit', yalertStylePath, '');
                        $location.url('phone/errorPage');
                    }
                }, function error(response) {
                });
            }
            $scope.getReplacePhoneCheckPageViewModel();

            /*
             * submit
             */
            $scope.submitMsg = function () {
                $http({
                    method: 'GET',
                    url: '/api/ReplacePhoneCheck/SubmitMsg',
                    params: ({
                    })
                }).then(function successCallback(response) {
                    $scope.FormFeedbackViewModel = response.data;
                    var model = $scope.FormFeedbackViewModel;
                    if (model.isSuccess) {
                        //alert('success');
                        $location.url('phone/successPage');
                    } else {
                        alert('not legal');
                    }
                }, function errorCallback(response) {
                    $location.url('phone/errorPage');
                });
            };

            $scope.cancle = function (value) {
                $http({
                    method: 'GET',
                    params: ({
                    }),
                    url: '/api/ReplacePhoneCheck/SetIsSubmit',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    if (response.data.isSuccess) {
                        if (value == 1) {
                            showConfirm('', 'Back to index? Data will not be saved', yalertStylePath, function () {
                                window.location.href = '#!/phone/choosePage';
                            }, function () {
                            })
                        } else if (value == 2) {
                            window.location.href = '#!/phone/replacePage';
                        }
                    }
                }, function error(response) {
                });
            }

        }]
    })