angular.
module('registerPage').
component('registerCheckPage', {

    templateUrl: 'common/check-page.template.html',
    controller: ['$location', '$http', '$scope', function RegisterCheck($location, $http, $scope) {

        var state = 1;
        var dateString = '';

        $http({
            method: 'Get',
            url: 'api/DoubleCheck/GetTempPhone',
        }).then(function successCallback(response) {
            // 请求成功执行的代码
            $scope.phone = response.data;
            state = $scope.phone.state;
            dateString = $scope.phone.startDate

        }, function errorCallback(response) {
            // 请求失败执行代码

        });

        if (state == 1) {
            $scope.state = '正在使用';
        } else {
            $scope.state = '已停用';
        }

        console.log(dateString);

        /*
         * 
        $scope.formatDate = function () {
            var inputDate = $scope.phone.inputDate;
            var year = inputDate.getFullYear();
            var month = inputDate.getMonth() + 1;
            if (month < 10) month = '0' + month;
            var date = inputDate.getDate();
            if (date < 10) date = '0' + date;
            var startDate = year + '' + month + '' + date;
            var endDate = (year + $scope.phone.life) + '' + month + '' + date;
            $scope.phone.startDate = startDate;
            $scope.phone.endDate = endDate;

        }
        *
        */
        
        this.submitMsg = function() {

            // 更换的新手机存入tempPhone
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

                $location.url('/phone/successPage');

            }, function errorCallback(response) {
                // 请求失败执行代码
                $location.url('phone/errorPage');
            });

        };

        this.cancle = function() {

            $location.url('/phone/registerPage');
        };

    }]

})