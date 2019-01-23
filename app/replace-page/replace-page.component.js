angular.
module('replacePage').
component('replacePage', {

    templateUrl: 'replace-page/replace-page.template.html',
    controller: ['$location', '$http', '$scope', function ReplacePage($location, $http) {

        // 从tempPhone获取需要修改的phone
        $http({
            method: 'Get',
            url: 'api/DoubleCheck/GetTempPhone',

        }).then(function successCallback(response) {
            // 请求成功执行的代码
            $scope.tempPhone = response.data;
        }, function errorCallback(response) {
            // 请求失败执行代码
            alert('error');
        });

        //  for test
        this.test = "你还没点击提交";

        // 点击确认
        this.submitMsg = function(tempPhone) {

            //  for test
            this.test = "你点击了提交";

            // 更换的新手机存入tempPhone
            $http({
                method: 'Post',
                url: 'api/DoubleCheck/SetTempPhone',
                data: tempPhone,

            }).then(function successCallback(response) {
                // 请求成功执行的代码
                $location.url('/phone/checkPage');
            }, function errorCallback(response) {
                // 请求失败执行代码
                alert('error');
            });

        };

        this.cancle = function() {
            $location.url('/phone');
        }
    }]

})