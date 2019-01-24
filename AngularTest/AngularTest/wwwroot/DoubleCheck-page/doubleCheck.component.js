angular.
    module("doubleCheck").
    component("doubleCheck", {
        templateUrl: 'DoubleCheck-page/recheck.html',
        controller: ['$scope', '$http', '$location',function DeleteDoubleCtrl($scope, $http,$location) {
            $scope.getCheckPhone = function () {
                $http({
                    method: 'GET',
                    params: ({

                    }),
                    url: '/api/DoubleCheck/GetTempPhone',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $scope.checkPhone = response.data;
                    //alert($scope.checkPhone);
                }, function error(response) {
                    alert("error");
                });
            }
            $scope.getCheckPhone();

            $scope.changeStatus = function () {
                //alert('hhh');
                $http({
                    method: 'POST',
                    params: ({
                        id: $scope.checkPhone.id,
                        phoneUser: $scope.checkPhone.phoneUser,
                        brand: $scope.checkPhone.brand,
                        type: $scope.checkPhone.type,
                        productNo: $scope.checkPhone.productNo,
                        startDate: $scope.checkPhone.startDate,
                        endDate: $scope.checkPhone.endDate,
                        deleteDate: $scope.checkPhone.deleteDate,
                        AbandonReason: $scope.checkPhone.abandonReason,
                        state: $scope.checkPhone.state
                    }),
                    url: '/api/Phone/AbandonUserPhoneById',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $location.url ('/phone');
                    //$scope.checkPhone = response.data;
                    //alert(response.data);
                }, function error(response) {
                    alert("error");
                });
            }
        }]
    });