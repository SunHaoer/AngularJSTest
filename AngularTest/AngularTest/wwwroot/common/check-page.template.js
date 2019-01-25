angular.
module('common').
component('checkPage', {

    templateUrl: 'common/check-page.template.html',
    controller: ['$location', '$http', '$scope', function commonPage($location, $http, $scope) {

        $http({
            method: 'Get',
            url: 'api/DoubleCheck/GetTempPhone',
        }).then(function successCallback(response) {
            $scope.phone = response.data;
            //console.log($scope.phone.startDate);
        }, function errorCallback(response) {

        });

        this.test = "你还没点击";
        this.submitMsg = function() {
            this.test = "你点击了确定";

            // 更换的新手机存入数据库
            $http({
                method: 'Post',
                url: 'api/Phone/SaveUserPhone',
                params: ({
                    id: $scope.phone.id,
                    phoneUser: $scope.phone.phoneUser,
                    brand: $scope.phone.brand,
                    type: $scope.phone.type,
                    productNo: $scope.phone.productNo,
                    startDate: $scope.phone.startDate,
                    endDate: $scope.phone.endDate,
                    deleteDate: $scope.phone.deleteDate,
                    abandonReson: $scope.phone.abandonReson,
                    state: $scope.phone.state
                })
            }).then(function successCallback(response) {
                // 请求成功执行的代码

                // 更改旧手机的状态


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