angular.
    module('replacePage').
    component('replaceCheckPage', {
        templateUrl: 'common/check-page.template.html',
        controller: ['$location', '$http', '$scope', function RegisterCheck($location, $http, $scope) {

            var state = 1;
            $scope.isReplace = true;
            $scope.myDate = new Date();
            $scope.myDate.toLocaleDateString();//获取当前日期

            $http({
                method: 'Get',
                url: 'api/TempPhone/GetNewTempPhone',
            }).then(function successCallback(response) {
                $scope.phone = response.data;
                state = $scope.phone.state;
            }, function errorCallback(response) {
                alert('error');
            });

            $http({
                method: 'Get',
                url: 'api/TempPhone/GetOldTempPhone',
            }).then(function successCallback(response) {
                $scope.oldPhone = response.data;
                state = $scope.oldPhone.state;
            }, function errorCallback(response) {
                alert('error');
            });

            if (state == 1) {
                $scope.state = 'using';
            } else if (state == 2) {
                $scope.state = 'abandoned';
            } else if (state === 3) {
                $scope.state = 'deleted';
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
                url: 'api/TempPhone/GetOldId',
            }).then(function successCallback(response) {
                oldId = response.data;
            }, function errorCallback(response) {
                alert('保存旧id失败');
            });

            // 更换的新手机存入newTempPhone
            this.submitMsg = function () {
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
                        abandonReson: $scope.phone.deleteReson,
                        state: $scope.phone.state
                    })
                }).then(function successCallback(response) {
                    // 请求成功执行的代码
                    $http({
                        method: 'POST',
                        url: 'api/Phone/AbandonUserPhoneById',
                        params: ({
                            id: oldId,
                            deleteDate: $scope.phone.deleteDate
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