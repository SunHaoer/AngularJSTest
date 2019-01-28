angular.
    module("doubleCheck").
    component("doubleCheck", {
        templateUrl: 'DoubleCheck-page/recheck.html',
        controller: ['$scope', '$http', '$location', function DeleteDoubleCtrl($scope, $http, $location) {

            $scope.format = function () {
                var deleteDate = $scope.checkPhone.deleteDate;
                var year = deleteDate.getFullYear();
                var month = ("0" + (deleteDate.getMonth() + 1)).slice(-2);
                var date = ("0" + deleteDate.getDate()).slice(-2);
                return year + "-" + month + "-" + date;
            }

            $scope.getCheckPhone = function () {
                $http({
                    method: 'GET',
                    params: ({
                    }),
                    url: '/api/TempPhone/GetNewTempPhone',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $scope.checkPhone = response.data;
                }, function error(response) {
                    alert("error");
                });
            }
            $scope.getCheckPhone();

            $scope.changeStatus = function () {
                $http({
                    method: 'POST',
                    params: ({
                        id: $scope.checkPhone.id
                        //phoneUser: $scope.checkPhone.phoneUser,
                        //brand: $scope.checkPhone.brand,
                        //type: $scope.checkPhone.type,
                        //productNo: $scope.checkPhone.productNo,
                        //startDate: $scope.checkPhone.startDate,
                        //endDate: $scope.checkPhone.endDate,
                        //deleteDate: $scope.checkPhone.deleteDate,
                        //AbandonReason: $scope.checkPhone.abandonReason,
                        //state: $scope.checkPhone.state
                    }),
                    url: '/api/Phone/DeleteUserPhoneById',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $location.url('/phone/successPage');
                    }, function error(response) {
                        $location.url('phone/errorPage');
                });
            }

        }]
    });