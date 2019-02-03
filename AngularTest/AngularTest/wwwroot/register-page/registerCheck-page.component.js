angular.
module('registerPage').
component('registerCheckPage', {
    templateUrl: 'common/check-page.template.html',
    controller: ['$location', '$http', '$scope', function RegisterCheck($location, $http, $scope) {
        $scope.isReplace = false;

        /*
         * get 'AddPhonePageViewModel'
         */
        $scope.getAddPhonePageViewModel = function () {
            $http({
                method: 'GET',
                params: ({
                }),
                url: '/api/AddPhoneCheck/GetAddPhoneCheckPageViewModel',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                $scope.addPhonePageViewModel = response.data;
                var model = $scope.addPhonePageViewModel;
                if (model.isLogin) {
                    $scope.phone = model.tempNewPhone;
                } else {
                    $location.url('phone/errorPage');
                }
            }, function error(response) {
            });
        }
        $scope.getAddPhonePageViewModel();

        /*
         * submit
         */
        $scope.submitMsg = function () {
            $http({
                method: 'GET',
                url: '/api/AddPhoneCheck/SubmitMsg',
                params: ({
                }),
                headers: { 'Content-Type': 'application/json' }
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

        $scope.cancle = function() {
            $location.url('/phone/registerPage');
        };

        $scope.backToIndex = function () {
            if (confirm('Back to index? Data will not be saved')) {
                $location.path('/phone/choosePage');    
            }
        }

    }]

})