angular.
module('choosePage').
component('choosePage',{
    templateUrl:'choose-page/choose-page.template.html',
    controller: ['$scope', '$http', function ChoosePageCtrl($scope, $http) {
        var brand2 = "OPPO";
            var phone = this.phone;
            $http({
                method: 'GET',
                params: ({
                    brand: brand2,
                }),
                url: '/api/BrandTypeModels/GetTypeByBrand',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                var typeList = response.data;
                console.log(typeList);
                var list = [];
                for (var i = 0; i < typeList.length; i++) {
                    list.push(typeList[i]["type"]);
                }
                console.log(list);
            }, function error(response) {
                alert("error");
            });
        

    }]
});