angular.
module('replacePage').
component('replacePage', {

    templateUrl: 'common/register-page.template.html',
    controller: ['$location', '$http', '$scope', function ReplacePage($location, $http, $scope) {

        $scope.isRegister = false;
        $scope.isReplace = true;
        //var oldId = 0;
        $scope.myDate = new Date();
        $scope.myDate.toLocaleDateString();//获取当前日期

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
                alert("error");
            });
        }
        $scope.checkLogin();

        $scope.GetLoginUsername = function () {     // 需提取
            $http({
                method: 'GET',
                params: ({

                }),
                url: '/api/Phone/GetLoginUsername',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                $scope.loginUsername = response.data;
                //$scope.getNewTempPhone();
            }, function error(response) {
                alert("error");
            });
        }
        $scope.GetLoginUsername();
        
        // 从NewTempPhone获取需要修改的phone
        $scope.getPhoneToNewTemp = function () {
            $http({
                method: 'GET',
                url: 'api/TempPhone/GetNewTempPhone',
            }).then(function successCallback(response) {
                $scope.phone = response.data;
                $scope.phone.inputDate = new Date($scope.phone.startDate);
                if ($scope.phone.startDate == "0001-01-01T00:00:00") {
                    $scope.phone.startDate = new Date($scope.myDate);
                }
                $scope.phone.inputDate = new Date($scope.phone.startDate);
                $scope.phone.phoneUser = $scope.loginUsername;
            }, function errorCallback(response) {
                alert('error');
            });
        }
        $scope.getPhoneToNewTemp();

        $scope.GetOldTempPhone = function () {
            $http({
                method: 'Get',
                url: 'api/TempPhone/GetOldTempPhone',
            }).then(function successCallback(response) {
                $scope.oldPhone = response.data;
            }, function errorCallback(response) {
                alert('error');
            });
        }
        $scope.GetOldTempPhone();

        //// 保存旧id
        //$scope.saveOldIdTotemp = function (oldId) {
        //    $http({
        //        method: 'POST',
        //        url: 'api/TempPhone/SetOldId',
        //        params: {
        //            id: oldId
        //        }
        //    }).then(function successCallback(response) {
                               
        //    }, function errorCallback(response) {
        //        alert('保存旧id失败');
        //    });
        //}

        /**
         * 获取所有手机品牌
         * */
        $scope.getBrandAll = function() {
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
        $scope.getTypeByBrand = function() {
            
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
        $scope.getYearByType = function() {
            var typeFlag = $scope.phone.type;
            if (typeFlag != "none") {
                //console.log(typeFlag);
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
                //alert($scope.phone.life);
                }, function error(response) {
                    alert('error1');
            });
        }


        /**
         * 日期格式化
         * */
        $scope.formatDate = function () {
            //alert($scope.phone.inputDate);
            var inputDate = $scope.phone.inputDate;
            var year = inputDate.getFullYear();
            //alert(year);
            var month = inputDate.getMonth() + 1;
            if (month < 10) month = '0' + month;
            var date = inputDate.getDate();
            if (date < 10) date = '0' + date;
            var startDate = year + '-' + month + '-' + date;
            year = year + $scope.phone.life;
            //alert(year);
            var endDate = year + '-' + month + '-' + date;
            $scope.phone.startDate = startDate;
            $scope.phone.endDate = endDate;
            //alert('endDate' + endDate);
        }        

        //  for test
        this.test = "你还没点击提交";

        //时间大小比较
        $scope.daysBetween = function (DateOne, DateTwo) {
            //alert(DateOne + '\n' + DateTwo);
            var oneYear = DateOne.getFullYear();
            var twoYear = DateTwo.getFullYear();
            var oneMonth = ("0" + (DateOne.getMonth() + 1)).slice(-2);
            var twoMonth = ("0" + (DateTwo.getMonth() + 1)).slice(-2);
            var oneDate = ("0" + DateOne.getDate()).slice(-2);
            var TwoDate = ("0" + DateTwo.getDate()).slice(-2);
            if ((oneYear - twoYear) < 0) return false;
            if ((oneMonth - twoMonth) < 0) return false;
            if ((oneDate - TwoDate) < 0) return false;
            return true;
        }

        // 点击确认
        $scope.submitMsg = function() {
            this.test = "你点击了提交";
            //alert($scope.phone.abandonDate);
            $scope.oldPhone.abandonDate = $scope.phone.startDate;
            $http({
                method: 'Post',
                url: '/api/TempPhone/UpdateOldTempPhoneAbandonDate',
                params: ({
                    abandonDate: $scope.oldPhone.abandonDate,
                })
            }).then(function successCallback(response) {

            }, function errorCallback(response) {
                alert('error1');
            });
            //alert(endDate);
            // 更换的新手机存入newTempPhone
            $http({
                method: 'Post',
                url: '/api/TempPhone/SetNewTempPhone',
                params: ({
                    id: $scope.phone.id,
                    phoneUser: $scope.phone.phoneUser,
                    brand: $scope.phone.brand,
                    type: $scope.phone.type,
                    productNo: $scope.phone.productNo,
                    startDate: $scope.phone.startDate,
                    endDate: $scope.phone.endDate,
                    //abandonDate: $scope.phone.abandonDate,
                    //abandonReson: $scope.phone.abandonReson,
                    //state: $scope.phone.state
                })
            }).then(function successCallback(response) {
                //if ($scope.daysBetween($scope.phone.inputDate, $scope.phone.abandonDate) == true) {
                    $location.url('/phone/replaceCheckPage');
                //} else {
                //    alert('StartDate can not early then abandonDate!');
                //}
            }, function errorCallback(response) {
                alert('error2');
            });

        };

        $scope.cancle = function(phone) {
            $location.url('/phone');
        }
    }]

})