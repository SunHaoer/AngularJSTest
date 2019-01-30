angular.
    module('registerPage').
    component('registerPage', {
        templateUrl: 'common/register-page.template.html',
        controller: ['$scope', '$http', '$location', function RegisterPageCtrl($scope, $http, $location) {

            $scope.productNoReg = '[a-zA-Z0-9]*';;
            $scope.brandRegex = '\\d+';
            $scope.flag = false;
            $scope.isRegister = true;
            $scope.isReplace = false;
            $scope.myDate = new Date();
            $scope.myDate.toLocaleDateString();//获取当前日期

            //alert('isRegister');

            $scope.checkLogin = function () {   // 需提取
                $http({
                    method: 'GET',
                    params: ({
                    }),
                    url: '/api/Phone/CheckLogin',
                    async: false,
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

            /**
             * 获取需要回填的phone
             */
            $scope.getNewTempPhone = function () {
                $http({
                    method: 'Get',
                    url: '/api/TempPhone/GetNewTempPhone',
                    async: false,
                }).then(function successCallback(response) {
                    $scope.phone = response.data;
                    if ($scope.phone.startDate == "0001-01-01T00:00:00") {
                        $scope.phone.startDate = new Date($scope.myDate);
                    }
                    $scope.phone.inputDate = new Date($scope.phone.startDate);
                    $scope.phone.phoneUser = $scope.loginUsername;
                    //alert('1' + $scope.phone.phoneUser);
                }, function errorCallback(response) {
                    //alert('error');
                });
            }

            $scope.GetLoginUsername = function () {     // 需提取
                $http({
                    method: 'GET',
                    params: ({

                    }),
                    url: '/api/Phone/GetLoginUsername',
                    async: false,
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $scope.loginUsername = response.data;
                    //$scope.getNewTempPhone();
                    //alert('2' + $scope.loginUsername);
                }, function error(response) {
                    //alert("error");
                });
            }

            /**
             * 获取所有手机品牌
             */
            $scope.getBrandAll = function () {
                $http({
                    method: 'GET',
                    url: '/api/BrandModel/GetBrandAll',
                    async: false,
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    var list = response.data;
                    $scope.brandList = [];
                    for (var i = 0; i < list.length; i++) {
                        $scope.brandList.push(list[i]["brand"]);
                    }
                }, function error(response) {
                    //alert("error");
                });
            }
            
            $scope.init = function () {
                $scope.GetLoginUsername();
                $scope.getBrandAll();
                $scope.getNewTempPhone();
            }
            //for (var i = 0; i < 5; i++) {
                $scope.init();
            //}

            $scope.getphoneUser = function () {
                $scope.phone.phoneUser = $scope.loginUsername;
            }

            /**
             * 根据品牌获取型号
             */
            $scope.getTypeByBrand = function () {
                //var phone = $scope.phone;
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
                    //alert("error");
                });
            }

            $scope.GetBrandTypeByProductNo = function () {
                $http({
                    method: 'GET',
                    params: ({
                        productNo: $scope.phone.productNo
                    }),
                    url: '/api/BrandTypeProductNo/GetBrandTypeByProductNo',
                    headers: { 'Content-Type': 'application/json' },
                }).then(function success(response) {
                    //$scope.ProductNoIsLegal = response.data;
                    //alert(response.data.type);
                    $scope.phone.brand = response.data.brand;
                    $scope.phone.type = response.data.type;
                }, function error(response) {
                    //alert('error');
                });
            }

            $scope.validateProductNo = function () {
                $http({
                    method: 'GET',
                    params: ({
                        productNo: $scope.phone.productNo,
                        brand: $scope.phone.brand,
                        type: $scope.phone.type,
                    }),
                    url: '/api/BrandTypeProductNo/ValidateProductNo',
                    headers: { 'Content-Type': 'application/json' },
                }).then(function success(response) {
                    $scope.ProductNoIsLegal = response.data;
                    if ($scope.ProductNoIsLegal) {
                        $scope.getTypeByBrand();
                    }
                    //alert(response.data);
                }, function error(response) {
                    //alert('error');
                });
            }

            /**
             * 根据型号获取保质期
             */
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
             */
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
             * 启用日期不能早于当前日期
             */
            $scope.daysBetween = function (DateOne, DateTwo) {
                var oneYear = DateOne.getFullYear();
                var twoYear = DateTwo.getFullYear();
                var oneMonth = ("0" + (DateOne.getMonth() + 1)).slice(-2);
                var twoMonth = ("0" + (DateTwo.getMonth() + 1)).slice(-2);
                var oneDate = ("0" + DateOne.getDate()).slice(-2);
                var twoDate = ("0" + DateTwo.getDate()).slice(-2);
                if (oneYear != twoYear) {
                    return oneYear >= twoYear;
                } else if (oneMonth != twoMonth) {
                    return oneMonth >= twoMonth;
                } else {
                    return oneDate >= twoDate;
                }
                //if ((oneYear - twoYear) < 0) return false;
                //if ((oneMonth - twoMonth) < 0) return false;
                //if ((oneDate - TwoDate) < 0) return false;
                //return true;
            }

            /**
             * 保存数据
             */
            $scope.submitMsg = function () {
                $scope.validateProductNo();
                if ($scope.ProductNoIsLegal == false) {
                    alert('ProductNo is not legal');
                    return;
                }
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
                    url: '/api/TempPhone/SetNewTempPhone',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    if ($scope.daysBetween($scope.phone.inputDate, $scope.myDate) == true) {
                        $location.path("/phone/registerCheckPage");
                    }
                    else {
                        alert('StartDate is too earyl!');
                    }
                }, function error(response) {
                    //alert("error");
                });
            }

            $scope.backToIndex = function () {
                //alert(1);
                if (confirm('Back to index? Data will not be saved')) {
                    $location.path('/phone/choosePage');     // ??????
                }
            }
            
        }]
    })