angular.
    module("deletePage").
    component("deletePage", {
        templateUrl: "delete-page/delete-page.template.html",
        controller: ["$scope", "$http", function deletePageCtrl($scope, $http) {
            //
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
            //格式化日期
            $scope.formatDate = function () {
                var deleteDate = $scope.TempPhone.deleteDate;
                var year = deleteDate.getFullYear();
                var month = ("0" + (deleteDate.getMonth() + 1)).slice(-2);
                var date = ("0" + deleteDate.getDate()).slice(-2);
                return year + "/" + month + "/" + date;
            }


            $scope.setTempPhone = function () {
                $http({
                    method: 'POST',
                    params: ({
                        id: $scope.TempPhone.id,
                        phoneUser: $scope.TempPhone.phoneUser,
                        brand: $scope.TempPhone.brand,
                        type: $scope.TempPhone.type,
                        productNo: $scope.TempPhone.productNo,
                        startDate: $scope.TempPhone.startDate,
                        endDate: $scope.TempPhone.endDate,
                        deleteDate: $scope.formatDate(),
                        AbandonReason: $scope.TempPhone.abandonReason,
                        state: $scope.TempPhone.state
                  
                    }),
                    url: '/api/DoubleCheck/SetTempPhone',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    //$scope.TempPhone = response.data;
                    //alert($scope.TempPhone.deleteDate);
                }, function error(response) {
                    alert("error");
                });
            }
               
        }]

    });

