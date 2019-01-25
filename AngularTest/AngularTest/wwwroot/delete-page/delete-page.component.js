angular.
    module("deletePage").
    component("deletePage", {
        templateUrl: "delete-page/delete-page.template.html",
        controller: ["$scope", "$http", "$location", function deletePageCtrl($scope, $http, $location) {
            $scope.myDate = new Date();
            $scope.myDate.toLocaleDateString();//获取当前日期

            //导入数据
            $scope.getTempPhone = function () {
                $http({
                    method: 'GET',
                    params: ({

                    }),
                    url: '/api/DoubleCheck/GetTempPhone',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $scope.TempPhone = response.data;
                    if ($scope.TempPhone.deleteDate == "0001-01-01T00:00:00") {
                        $scope.TempPhone.deleteDate = new Date($scope.myDate);
                    }
                    else
                    $scope.TempPhone.deleteDate = new Date($scope.TempPhone.deleteDate);
                }, function error(response) {
                    alert("error");
                });
            }
            $scope.getTempPhone();

            //日期格式化
            $scope.formatDate = function () {
                var deleteDate = $scope.TempPhone.deleteDate;
                var year = deleteDate.getFullYear();
                var month = ("0" + (deleteDate.getMonth() + 1)).slice(-2);
                var date = ("0" + deleteDate.getDate()).slice(-2);
                return year + "-" + month + "-" + date;
            }

            //时间大小比较
            $scope.daysBetween = function (DateOne, DateTwo) {
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

            //将修改值传入temp
            $scope.setTempPhone = function () {
                $http({
                    method: 'Post',
                    params: ({
                        id: $scope.TempPhone.id,
                        phoneUser: $scope.TempPhone.phoneUser,
                        brand: $scope.TempPhone.brand,
                        type: $scope.TempPhone.type,
                        productNo: $scope.TempPhone.productNo,
                        startDate: $scope.TempPhone.startDate,
                        endDate: $scope.TempPhone.endDate,
                        deleteDate: $scope.formatDate(),
                        abandonReason: $scope.TempPhone.abandonReason,
                        state: $scope.TempPhone.state
                    }),
                    url: '/api/DoubleCheck/SetTempPhone',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    if ($scope.daysBetween($scope.TempPhone.deleteDate, $scope.myDate) == true) {
                        $location.url("/phone/doubleCheck");
                    }
                    else {
                        alert('DeleteDate is too early!');
                    }
                }, function error(response) {
                    alert("error");
                });
            }

        }]

    });
