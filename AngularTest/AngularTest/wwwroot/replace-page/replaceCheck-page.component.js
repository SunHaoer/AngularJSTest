angular.
    module('replacePage').
    component('replaceCheckPage', {
        templateUrl: 'common/check-page.template.html',
        controller: ['$location', '$http', '$scope', function RegisterCheck($location, $http, $scope) {

            var state = 1;
            $scope.isReplace = true;

            $http({
                method: 'Get',
                url: 'api/DoubleCheck/GetTempPhone',
            }).then(function successCallback(response) {
                $scope.phone = response.data;
                state = $scope.phone.state;
            }, function errorCallback(response) {
                alert('error');
            });

            if (state == 1) {
                $scope.state = '正在使用';
            } else {
                $scope.state = '已停用';
            }

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

            // 获取旧id
            var oldId = 0;
            $http({
                method: 'GET',
                url: 'api/DoubleCheck/GetOldId',
            }).then(function successCallback(response) {
                oldId = response.data;
                //console.log(response.data);
            }, function errorCallback(response) {
                alert('保存旧id失败');
                });

            this.submitMsg = function () {
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
                    alert(oldId);
                    $http({
                        method: 'POST',
                        url: 'api/Phone/AbandonUserPhoneById',
                        params: ({
                            id: oldId
                        })
                    }).then(function successCallback(response) {

                    }, function errorCallback(response) {
                        alert('error');
                    });
                    $location.url('/phone/successPage');
                }, function errorCallback(response) {
                    $location.url('phone/errorPage');
                });
            };

            this.cancle = function () {
                $location.url('/phone/replacePage');
            };

        }]
    })