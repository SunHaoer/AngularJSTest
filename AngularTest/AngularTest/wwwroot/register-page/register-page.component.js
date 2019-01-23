angular.
    module('registerPage').
    component('registerPage', {
        templateUrl: 'register-page/register-page.template.html',
        controller: ['$scope', '$http', function RegisterPageCtrl($scope, $http) {
            $scope.brandRegex = '\\d+';
            $scope.flag = false;
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
                    alert("brand error");
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
                    alert("type error");
                });
            }

            /**
             * 根据型号获取保质期
             * */
            $scope.getYearByType = function () {
                var typeFlag = $scope.phone.type;
                if (typeFlag != "none") {
                    console.log(typeFlag);
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
                var inputDate = $scope.inputDate;
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
            $scope.sendToTempPhone = function () {
                var phone = $scope.phone;
                $http({
                    method: 'POST',
                    params: ({
                        phoneUser: phone.phoneUser,
                        brand: phone.brand,
                        type: phone.type,
                        productNo: phone.productNo,
                        inputDate: phone.startDate,
                        endDate: phone.endDate
                    }),
                    url: '/api/DoubleCheck/SetTempPhone',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    alert(response.data);
                }, function error(response) {
                    alert("save error");
                });
            }


            


        }]
    })