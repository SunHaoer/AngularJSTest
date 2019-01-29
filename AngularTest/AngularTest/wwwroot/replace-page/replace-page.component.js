angular.
module('replacePage').
component('replacePage', {

    templateUrl: 'common/register-page.template.html',
    controller: ['$location', '$http', '$scope', function ReplacePage($location, $http, $scope) {

        $scope.isRegister = false;
        $scope.isReplace = true;
        var oldId = 0;
        //alert('isReplace');
        
        // 从NewTempPhone获取需要修改的phone
        $scope.savePhoneToNewTemp = function () {
            $http({
                method: 'Get',
                url: 'api/TempPhone/GetNewTempPhone',
            }).then(function successCallback(response) {
                $scope.phone = response.data;
                oldId = $scope.phone.id;
                $scope.saveOldIdTotemp(oldId);
                $scope.phone.inputDate = new Date($scope.phone.startDate);
                if ($scope.phone.abandonDate == "0001-01-01T00:00:00") {
                    $scope.phone.abandonDate = new Date($scope.myDate);
                } else {
                    $scope.phone.abandonDate = new Date($scope.phone.abandonDate);
                }
            }, function errorCallback(response) {
                alert('error');
            });
        }
        $scope.savePhoneToNewTemp();

        // 保存旧id
        $scope.saveOldIdTotemp = function (oldId) {
            $http({
                method: 'POST',
                url: 'api/TempPhone/SetOldId',
                params: {
                    id: oldId
                }
            }).then(function successCallback(response) {
                               
            }, function errorCallback(response) {
                alert('保存旧id失败');
            });
        }

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
                }, function error(response) {
                    alert('error1');
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
            alert($scope.phone.abandonDate);
            $http({
                method: 'Post',
                url: '/api/TempPhone/UpdateOldTempPhoneAbandonDate',
                params: ({
                    abandonDate: $scope.phone.abandonDate,
                })
            }).then(function successCallback(response) {

            }, function errorCallback(response) {
                alert('error');
            });

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
                if ($scope.daysBetween($scope.phone.inputDate, $scope.phone.abandonDate) == true) {
                    $location.url('/phone/replaceCheckPage');
                } else {
                    alert('StartDate can not early then abandonDate!');
                }
            }, function errorCallback(response) {
                alert('error');
            });

        };

        $scope.cancle = function(phone) {
            $location.url('/phone');
        }
    }]

})