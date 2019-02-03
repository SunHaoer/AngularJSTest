angular.
    module('replacePage').
    component('replaceCheckPage', {
        templateUrl: 'common/check-page.template.html',
        controller: ['$location', '$http', '$scope', function RegisterCheck($location, $http, $scope) {

            $scope.isReplace = true;
            $scope.myDate = new Date();
            $scope.myDate.toLocaleDateString();//获取当前日期

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
                    if (model.isLogin) {
                        $scope.phone = model.tempNewPhone;
                        $scope.oldPhone = model.tempOldPhone;
                    } else {
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
                        alert('success');
                        $location.url('phone/successPage');
                    } else {
                        alert('not legal');
                    }
                }, function errorCallback(response) {
                    $location.url('phone/errorPage');
                });
            };

            $scope.cancle = function () {
                $location.url('/phone/replacePage');
            };

            $scope.backToIndex = function () {
                if (confirm('Back to index? Data will not be saved')) {
                    $location.path('/phone/choosePage');    
                }
            }

        }]
    })