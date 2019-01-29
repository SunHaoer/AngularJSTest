angular.
module('common').
component('checkPage', {

    templateUrl: 'common/check-page.template.html',
    controller: ['$location', '$http', '$scope', function commonPage($location, $http, $scope) {
        $scope.isReplace = true;

        $scope.checkLogin = function () {   // 需提取
            $http({
                method: 'GET',
                params: ({
                }),
                url: '/api/Phone/CheckLogin',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                if (response.data['notLogin'] == 'true') {
                    $location.url('/#!/phone');
                } else {
                    $scope.loginUsername = response.data;
                }
            }, function error(response) {
                alert("error");
            });
        }
        $scope.checkLogin();

        $http({
            method: 'Get',
            url: '/api/TempPhone/GetNewTempPhone',
        }).then(function successCallback(response) {
            $scope.phone = response.data;
        }, function errorCallback(response) {

        });

        this.test = "你还没点击";
        this.submitMsg = function() {
            this.test = "你点击了确定";
            
            // 更换的新手机存入数据库
            $http({
                method: 'Post',
                url: '/api/Phone/SaveUserPhone',
                params: ({
                    id: $scope.phone.id,
                    phoneUser: $scope.phone.phoneUser,
                    brand: $scope.phone.brand,
                    type: $scope.phone.type,
                    productNo: $scope.phone.productNo,
                    startDate: $scope.phone.startDate,
                    endDate: $scope.phone.endDate,
                    deleteDate: $scope.phone.deleteDate,
                    abandonReason: $scope.phone.deleteReason,
                    state: $scope.phone.state
                })
            }).then(function successCallback(response) {
                $location.url('/phone/replacePage');
            }, function errorCallback(response) {
                $location.url('phone/errorPage');
            });
        };

        this.cancle = function() {
            this.test = "你点击了取消";
            $location.url('/phone/replacePage');
        };

    }]

})