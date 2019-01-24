angular.
    module("deletePage").
    component("deletePage", {
        templateUrl: "delete-page/delete-page.template.html",
        controller: ["$scope", "$http", "$location", function deletePageCtrl($scope, $http, $location) {
            $scope.getTempPhone = function () {
                $http({
                    method: 'GET',
                    params: ({

                    }),
                    url: '/api/DoubleCheck/GetTempPhone',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $scope.TempPhone = response.data;

                }, function error(response) {
                    alert("error");
                });
            }
            $scope.getTempPhone();
            $scope.formatDate = function () {
                var deleteDate = $scope.TempPhone.deleteDate;
                var year = deleteDate.getFullYear();
                var month = ("0" + (deleteDate.getMonth() + 1)).slice(-2);
                var date = ("0" + deleteDate.getDate()).slice(-2);
                return year + "-" + month + "-" + date;
            }
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
                //if ((oneYear - twoYear) >= 0) return true;
                //if ((oneMonth - twoMonth) >= 0) return true;
                //if ((oneDate - TwoDate) >= 0) return true;
                //return false;
            }
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
                        //deleteDate: $scope.TempPhone.deleteDate,
                        abandonReason: $scope.TempPhone.abandonReason,
                        state: $scope.TempPhone.state
                    }),
                    url: '/api/DoubleCheck/SetTempPhone',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $scope.TempPhone.deleteDate = new Date($scope.TempPhone.deleteDate);
                    var myDate = new Date();
                    myDate.toLocaleDateString();

                    if ($scope.daysBetween($scope.TempPhone.deleteDate, myDate) == true) {
                        $location.url("/phone/doubleCheck");
                    }
                    else alert("日期不对啊兄弟");
                }, function error(response) {
                    alert("error");
                });
            }

        }]

    });
