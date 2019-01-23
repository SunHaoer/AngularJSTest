angular.
module('choosePage').
component('choosePage',{
    templateUrl:'choose-page/choose-page.template.html',
    controller: ['$scope', '$http', function ChoosePageCtrl($scope, $http) {

     
        //alert('srccess');
        $scope.getUserPhoneAll = function () {
            //alert("haha");
            $http({
                method: 'GET',
                params: ({

                }),
                url: '/api/Phone/GetUserPhoneAll',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                $scope.userPhoneAll = response.data;
                //alert('srccess');
                //alert('+' + $scope.phone.life);
            }, function error(response) {
                alert("error");
            });
        }
        $scope.getUserPhoneAll();

        $scope.remove = function (id) {
            $http({
                method: 'POST',
                params: ({
                    id: id
                }),
                url: '/api/DoubleCheck/SetTempPhoneById',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                //alert($scope.phone);
            }, function error(response) {
                alert("error");
            });
        
        }

    }]

    
});