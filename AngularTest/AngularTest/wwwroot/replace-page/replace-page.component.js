angular.
module('replacePage').
component('replacePage', {

    templateUrl: 'common/register-page.template.html',
    controller: ['$location', '$http', '$scope', function ReplacePage($location, $http, $scope) {

        $scope.isRegister = false;
        $scope.isReplace = true;
        var oldId = 0;
        
        // 从tempPhone获取需要修改的phone
        $scope.savePhoneToTemp = function () {
            //alert('savetemp');
            $http({
                method: 'Get',
                url: 'api/DoubleCheck/GetTempPhone',
            }).then(function successCallback(response) {
                // 请求成功执行的代码
                $scope.phone = response.data;
                //console.log(response.data);
                oldId = $scope.phone.id;
                //console.log(oldId);
                //alert(oldId);
                $scope.saveOldIdTotemp(oldId);
                $scope.phone.inputDate = new Date($scope.phone.startDate);
                //$scope.phone.deleteDate = new Date($scope.phone.deleteDate);
                if ($scope.phone.deleteDate == "0001-01-01T00:00:00") {
                    $scope.phone.deleteDate = new Date($scope.myDate);
                } else {
                    $scope.phone.deleteDate = new Date($scope.phone.deleteDate);
                }
            }, function errorCallback(response) {
                alert('error');
            });
        }
        $scope.savePhoneToTemp();

        // 保存旧id
        $scope.saveOldIdTotemp = function (oldId) {
            $http({
                method: 'POST',
                url: 'api/DoubleCheck/SetOldId',
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
            //alert('submit');
            //  for test
            this.test = "你点击了提交";

            //console.log($scope.phone.id + " " + $scope.phone.phoneUser + " " + $scope.phone.brand + " " + $scope.phone.type + " " + $scope.phone.startDate + " " + $scope.phone.endDate + " " + $scope.phone.deleteDate);
            // 更换的新手机存入tempPhone
            $http({
                method: 'Post',
                url: '/api/DoubleCheck/SetTempPhone',
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
                //alert($scope.phone.inputDate + '\n' + $scope.phone.deleteDate);
                if ($scope.daysBetween($scope.phone.inputDate, $scope.phone.deleteDate) == true) {
                    $location.url('/phone/replaceCheckPage');
                }
                else {
                    alert('StartDate can not early then deleteDate!');
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