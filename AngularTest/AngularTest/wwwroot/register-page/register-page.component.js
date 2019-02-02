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

           /*
            * get 'AddPhoneModel'
            */
            $scope.getAddPhoneModel = function () {
                $http({
                    method: 'GET',
                    params: ({
                    }),
                    url: '/api/AddPhone/GetAddPhoneModel',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $scope.addPhonePageViewModel = response.data;
                    var model = $scope.addPhonePageViewModel;
                    if (model.isLogin) {
                        $scope.brandList = model.brandList;
                        $scope.typeList = model.typeList;
                        $scope.phone = model.tempNewPhone;
                        $scope.phone.startDate = new Date(model.tempNewPhone.startDate);
                    } else {
                        $location.url('phone/errorPage');
                    }
                }, function error(response) {
                });
            }
            $scope.getAddPhoneModel();

            /*
             * submit
             */
            $scope.submitMsg = function () {
                if (true) {
                    var phone = $scope.phone;
                    $http({
                        method: 'GET',
                        params: ({
                            brand: phone.brand,
                            type: phone.type,
                            productNo: phone.productNo,
                            startDate: phone.startDate
                        }),
                        url: '/api/AddPhone/SubmitMsg',
                        headers: { 'Content-Type': 'application/json' }
                    }).then(function success(response) {
                        if (response.data.isSuccess) {
                            $location.path("/phone/registerCheckPage");
                        } else {
                            alert('not legal');
                        }
                        //if ($scope.daysBetween($scope.phone.inputDate, $scope.myDate) == true) {
                        //    $location.path("/phone/registerCheckPage");
                        //} else {
                        //    alert('StartDate is too earyl!');
                        //}
                    }, function error(response) {
                    });
                }
            }

            $scope.backToIndex = function () {
                if (confirm('Back to index? Data will not be saved')) {
                    $location.path('/phone/choosePage');
                }
            }

            //$scope.GetBrandTypeByProductNo = function () {
            //    $http({
            //        method: 'GET',
            //        params: ({
            //            productNo: $scope.phone.productNo
            //        }),
            //        url: '/api/BrandTypeProductNo/GetBrandTypeByProductNo',
            //        headers: { 'Content-Type': 'application/json' },
            //    }).then(function success(response) {
            //        //$scope.ProductNoIsLegal = response.data;
            //        //alert(response.data.type);
            //        $scope.phone.brand = response.data.brand;
            //        $scope.phone.type = response.data.type;
            //    }, function error(response) {
            //        //alert('error');
            //    });
            //}

            //$scope.validateProductNo = function () {
            //    //alert(1);
            //    $http({
            //        method: 'GET',
            //        params: ({
            //            productNo: $scope.phone.productNo,
            //            brand: $scope.phone.brand,
            //            type: $scope.phone.type,
            //        }),
            //        url: '/api/BrandTypeProductNo/ValidateProductNo',
            //        headers: { 'Content-Type': 'application/json' },
            //    }).then(function success(response) {
            //        $scope.ProductNoIsLegal = response.data;
            //        if ($scope.ProductNoIsLegal) {
            //            $scope.getTypeByBrand();
            //        }
            //        //alert(response.data);
            //    }, function error(response) {
            //        //alert('error');
            //    });
            //}

            ///**
            // * 根据型号获取保质期
            // */
            //$scope.getYearByType = function () {
            //    var typeFlag = $scope.phone.type;
            //    if (typeFlag != "none") {
            //        $scope.flag = true;
            //    } else {
            //        $scope.flag = false;
            //    }
            //    $http({
            //        method: 'GET',
            //        params: ({
            //            type: $scope.phone.type
            //        }),
            //        url: '/api/TypeYear/GetYearByType',
            //        headers: { 'Content-Type': 'application/json' }
            //    }).then(function success(response) {
            //        $scope.phone.life = response.data;
            //    }, function error(response) {
            //    });
            //}

            ///**
            // * 日期格式化
            // */
            //$scope.formatDate = function () {
            //    var inputDate = $scope.phone.inputDate;
            //    var year = inputDate.getFullYear();
            //    var month = inputDate.getMonth() + 1;
            //    if (month < 10) month = '0' + month;
            //    var date = inputDate.getDate();
            //    if (date < 10) date = '0' + date;
            //    var startDate = year + '-' + month + '-' + date;
            //    var endDate = (year + $scope.phone.life) + '-' + month + '-' + date;
            //    $scope.phone.startDate = startDate;
            //    $scope.phone.endDate = endDate;
            //}

            ///**
            // * 启用日期不能早于当前日期
            // */
            //$scope.daysBetween = function (DateOne, DateTwo) {
            //    var oneYear = DateOne.getFullYear();
            //    var twoYear = DateTwo.getFullYear();
            //    var oneMonth = ("0" + (DateOne.getMonth() + 1)).slice(-2);
            //    var twoMonth = ("0" + (DateTwo.getMonth() + 1)).slice(-2);
            //    var oneDate = ("0" + DateOne.getDate()).slice(-2);
            //    var twoDate = ("0" + DateTwo.getDate()).slice(-2);
            //    if (oneYear != twoYear) {
            //        return oneYear >= twoYear;
            //    } else if (oneMonth != twoMonth) {
            //        return oneMonth >= twoMonth;
            //    } else {
            //        return oneDate >= twoDate;
            //    }
            //    //if ((oneYear - twoYear) < 0) return false;
            //    //if ((oneMonth - twoMonth) < 0) return false;
            //    //if ((oneDate - TwoDate) < 0) return false;
            //    //return true;
            //}

            /**
             * 保存数据
             */
            //$scope.submitMsg = function () {
            //    $scope.validateProductNo();
            //    if ($scope.ProductNoIsLegal && $scope.inputDateIsLegal) {
            //        $http({
            //            method: 'POST',
            //            params: ({
            //                phoneUser: $scope.phone.phoneUser,
            //                brand: $scope.phone.brand,
            //                type: $scope.phone.type,
            //                productNo: $scope.phone.productNo,
            //                startDate: $scope.phone.startDate,
            //                endDate: $scope.phone.endDate
            //            }),
            //            url: '/api/TempPhone/SetNewTempPhone',
            //            headers: { 'Content-Type': 'application/json' }
            //        }).then(function success(response) {
            //            if ($scope.daysBetween($scope.phone.inputDate, $scope.myDate) == true) {
            //                $location.path("/phone/registerCheckPage");
            //            } else {
            //                alert('StartDate is too earyl!');
            //            }
            //        }, function error(response) {
            //            //alert("error");
            //        });
            //    }
            //}



            //$scope.inputDateIsLegal = true;
            //$scope.validateInputDate = function () {
            //    //alert(new Date(1900, 1, 1, 0, 0, 0, 0));
            //    if (!$scope.daysBetween($scope.phone.inputDate, $scope.myDate) || $scope.phone.inputDate < new Date(1900, 1, 1, 0, 0, 0, 0) || $scope.phone.inputDate > new Date(2100, 1, 1, 0, 0, 0, 0)) {
            //        $scope.inputDateIsLegal = false;
            //    } else {
            //        $scope.inputDateIsLegal = true;
            //    }
            //}

            //$scope.productNoIsExist = false;
            //$scope.checkProductNoIsExist = function() {
            //    $http({
            //        method: 'GET',
            //        params: ({
            //            productNo: $scope.phone.productNo
            //        }),
            //        url: '/api/BrandTypeProductNo/ProductNoIsExist',
            //        headers: { 'Content-Type': 'application/json' }
            //    }).then(function success(response) {
            //        $scope.productNoIsExist = response.data;
            //    }, function error(response) {
            //    });
            //}
            
        }]
    })