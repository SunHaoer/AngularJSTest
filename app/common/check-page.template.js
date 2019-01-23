angular.
module('common').
component('checkPage', {

    templateUrl: 'common/check-page.template.html',
    controller: ['$location', '$http', '$scope', function commonPage($location, $http, $scope) {

        $http({
            method: 'Get',
            url: '',
        }).then(function successCallback(response) {
            // 请求成功执行的代码


        }, function errorCallback(response) {
            // 请求失败执行代码

        });

        this.test = "你还没点击";
        this.submitMsg = function() {
            this.test = "你点击了确定";
            $location.url('/phone/successPage');

        };

        this.cancle = function() {
            this.test = "你点击了取消";
            $location.url('/phone/replacePage');
        };

    }]

})