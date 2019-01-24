angular.
    module('registerPage').
    component('registerPage', {
        templateUrl: 'common/register-page.template.html',
        controller: ['$scope', '$http','$location', function RegisterPageCtrl($scope, $http,$location) {
            $scope.brandRegex = '\\d+';
            $scope.flag = false;
            $scope.isRegister = true;
            $scope.isReplace = false;

            /**
             * 获取所有手机品牌
             * */
            $scope.getBrandAll = function () {
                $http({
                    method: 'GET',
                    url: '/api/BrandModel/GetBrandAll',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    var list = response.data;
                    $scope.brandList = [];
                    for (var i = 0; i < list.length; i++) {
                        $scope.brandList.push(list[i]["brand"]);
                    }
                }, function error(response) {
                    alert("error");
                });
            }
            $scope.getBrandAll();

            /**
             * 根据品牌获取型号
             * */
            $scope.getTypeByBrand = function () {
                var phone = $scope.phone;
                $http({
                    method: 'GET',
                    params: ({
                        brand: $scope.phone.brand,
                    }),
                    url: '/api/BrandTypeModels/GetTypeByBrand',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    var list = response.data;
                    $scope.typeList = [];
                    for (var i = 0; i < list.length; i++) {
                        $scope.typeList.push(list[i]["type"]);
                    }
                }, function error(response) {
                    alert("error");
                });
            }

            /**
             * 根据型号获取保质期
             * */
            $scope.getYearByType = function () {
                var typeFlag = $scope.phone.type;
                if (typeFlag != "none") {
                    $scope.flag = true;
                } else {
                    $scope.flag = false;
                }
                $http({
                    method: 'GET',
                    params: ({
                        type: $scope.phone.type
                    }),
                    url: '/api/TypeYear/GetYearByType',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $scope.phone.life = response.data;
                }, function error(response) {
                });
            }

            /**
             * 日期格式化
             * */
            $scope.formatDate = function () {
                var inputDate = $scope.phone.inputDate;
                var year = inputDate.getFullYear();
                var month = inputDate.getMonth() + 1;
                if (month < 10) month = '0' + month;
                var date = inputDate.getDate();
                if (date < 10) date = '0' + date;
                var startDate = year + '-' + month + '-' + date;
                var endDate = (year + $scope.phone.life) + '-' + month + '-' + date;
                $scope.phone.startDate = startDate;
                $scope.phone.endDate = endDate;
            }

            /**
             * 保存数据
             * */
            $scope.submitMsg = function () {

                alert('submit');

                $http({
                    method: 'POST',
                    params: ({
                        phoneUser: $scope.phone.phoneUser,
                        brand: $scope.phone.brand,
                        type: $scope.phone.type,
                        productNo: $scope.phone.productNo,
                        startDate: $scope.phone.startDate,
                        endDate: $scope.phone.endDate
                    }),
                    url: '/api/DoubleCheck/SetTempPhone',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $location.path("/phone/registerCheckPage");
                }, function error(response) {
                    alert("error");
                });
            }

            
        }]
    })