angular.
    module("doubleCheck").
    component("doubleCheck", {
        templateUrl: 'DoubleCheck-page/recheck.html',
        controller: ['$scope', '$http', '$location', function DeleteDoubleCtrl($scope, $http, $location) {

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
                    if (model.isLogin) {
                        $scope.phone = model.tempNewPhone;
                        $scope.phone.startDate = new Date(model.tempNewPhone.startDate);
                        $scope.phone.deleteDate = new Date(model.tempNewPhone.deleteDate);
                    } else {
                        alert('not login');
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
                alert('submit');
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
                        alert('success');
                        $location.url('phone/successPage');
                    } else {
                        alert('not legal');
                    }
                }, function errorCallback(response) {
                    $location.url('phone/errorPage');
                });
            }

            $scope.backToIndex = function () {
                if (confirm('Back to index?')) {
                    $location.path('/phone/choosePage');     
                }
            }
            $scope.previous = function () {
                $location.path('/phone/deletePage');
            }

        }]
    });