angular.
    module("doubleCheck").
    component("doubleCheck", {
        templateUrl: 'DoubleCheck-page/recheck.html',
        controller: ['$scope', '$http', '$location', function DeleteDoubleCtrl($scope, $http, $location) {

            $scope.checkLogin = function () {   // 需提取
                $http({
                    method: 'GET',
                    params: ({
                    }),
                    url: '/api/Phone/CheckLogin',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    if (response.data['notLogin'] == 'true') {
                        $location.url('/phone/errorPage');
                    }
                }, function error(response) {
                    //alert("error");
                });
            }
            $scope.checkLogin();

            $scope.format = function () {
                var deleteDate = new Date($scope.checkPhone.deleteDate);
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
                    //$scope.checkPhone.deleteReason = response.data.abandonReason;
                    $scope.checkPhone.deleteDate = $scope.format();
                }, function error(response) {
                    //alert("error");
                });
            }
            $scope.getCheckPhone();

            $scope.changeStatus = function () {
                $http({
                    method: 'POST',
                    params: ({
                        id: $scope.checkPhone.id,
                        deleteDate: $scope.checkPhone.deleteDate,
                        deleteReason: $scope.checkPhone.deleteReason,
                    }),
                    url: '/api/Phone/DeleteUserPhoneById',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $location.url('/phone/successPage');
                    }, function error(response) {
                        $location.url('phone/errorPage');
                });
            }


            $scope.backToIndex = function () {
                if (confirm('Back to index?')) {
                    $location.path('/phone/choosePage');     
                }
            }
            $scope.previous = function () {
                $location.path('/phone/deletePage');
            }

        }]
    });