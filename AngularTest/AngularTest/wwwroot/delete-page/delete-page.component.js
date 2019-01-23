angular.
    module("deletePage").
    component("deletePage", {
        templateUrl: "delete-page/delete-page.template.html",
        controller: ["$scope", "$http", function deletePageCtrl($scope, $http) {
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

               
        }]

    });

