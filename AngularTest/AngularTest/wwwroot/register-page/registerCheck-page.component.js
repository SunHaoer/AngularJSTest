angular.
module('registerPage').
component('registerCheckPage', {
    templateUrl: 'common/check-page.template.html',
    controller: ['$location', '$http', '$scope', function RegisterCheck($location, $http, $scope) {
        var state = 1;

        $scope.isReplace = false;

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
                //alert("error");
            });
        }
        $scope.checkLogin();

        $http({
            method: 'Get',
            url: '/api/TempPhone/GetNewTempPhone',
        }).then(function successCallback(response) {
            $scope.phone = response.data;
            state = $scope.phone.state;
            dateString = $scope.phone.startDate
        }, function errorCallback(response) {
            //alert('error');
        });
        if (state == 1) {
            $scope.state = 'using';
        } else if(state == 2) {
            $scope.state = 'abandoned';
        }
        //console.log(dateString);

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
        
        this.submitMsg = function () {
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
                $location.url('/phone/successPage');
            }, function errorCallback(response) {
                $location.url('phone/errorPage');
            });
        };

        this.cancle = function() {
            $location.url('/phone/registerPage');
        };

        $scope.backToIndex = function () {
            alert(1);
            if (confirm('Back to index? Data will not be saved')) {
                $location.path('/phone/choosePage');     // ??????
            }
        }

    }]

})