angular.
module('choosePage').
component('choosePage', {
    templateUrl: 'choose-page/choose-page.template.html',
    controller: ['$scope', '$http', '$location', function ChoosePageCtrl($scope, $http, $location) {

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

        this.replace = function(oldId) {

            $scope.phone = {
                'id': 1,
                'phoneUser': 'Hujun',
                'brand': 'OPPO',
                'type': 'K1',
                'productNo': '123456',
                'startDate': '20190101',
                'endDate': '',
                'deleteDate': '',
                'abandonReson': '',
                'state': 0
            }

            console.log(oldId);

            $http({
                method: 'Post',
                url: 'api/DoubleCheck/SetTempPhone',
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
                // ����ɹ�ִ�еĴ���
                $location.url('/phone/replacePage');

            }, function errorCallback(response) {
                // ����ʧ��ִ�д���
                alert('error');
            });

        };

    }]
});