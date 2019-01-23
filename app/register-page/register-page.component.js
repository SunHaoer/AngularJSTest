angular.
    module('registerPage').
    component('registerPage', {
        templateUrl: 'register-page/register-page.template.html',
        controller: ['$scope', '$http', function RegisterPageCtrl($scope, $http) {
            $scope.brandRegex = '\\d+';

            $scope.isRegister = true;
            $scope.isReplace = false;

            /**
             * ��ȡ�����ֻ�Ʒ��
             * */
            $scope.getBrandAll = function () {
                $http({
                    method: 'GET',
                    url: '/api/BrandModel/GetBrandAll',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    var list = response.data;
                    $scope.brandList = [];
                    for (var i = 0; i < list.length; i++) {
                        $scope.brandList.push(list[i]["brand"]);
                    }
                    
                }, function error(response) {
                    alert("error");
                });
            }
            $scope.getBrandAll();

            /**
             * ����Ʒ�ƻ�ȡ�ͺ�
             * */
            $scope.getTypeByBrand = function () {
                var phone = $scope.phone;
                $http({
                    method: 'GET',
                    params: ({
                        brand: $scope.phone.brand,
                    }),
                    url: '/api/BrandTypeModels/GetTypeByBrand',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    var list = response.data;
                    console.log(list);
                    $scope.typeList = [];
                    for (var i = 0; i < list.length; i++) {
                        $scope.typeList.push(list[i]["type"]);
                    }
                }, function error(response) {
                    alert("error");
                });
            }

            /**
             * �����ͺŻ�ȡ������
             * */
            $scope.getYearByType = function() {
                $http({
                    method: 'GET',
                    params: ({
                        type: $scope.phone.type
                    }),
                    url: '/api/TypeYear/GetYearByType',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $scope.phone.life = response.data;
                    //alert('+' + $scope.phone.life);
                }, function error(response) {
                    alert("error");
                });
            }

            /**
             * ���ڸ�ʽ��
             * */
            $scope.formatDate = function () {
                var inputDate = $scope.phone.inputDate;
                var year = inputDate.getFullYear();
                var month = inputDate.getMonth() + 1;
                if (month < 10) month = '0' + month;
                var date = inputDate.getDate();
                if (date < 10) date = '0' + date;
                var startDate = year + '' + month + '' + date;
                var endDate = (year + $scope.phone.life) + '' + month + '' + date;
                $scope.phone.startDate = startDate;
                $scope.phone.endDate = endDate;
                //alert('-' + $scope.phone.life);
                //alert(startDate);
                //alert(endDate);
            }

            /**
             * ��������
             * */
            $scope.sendToTempPhone = function () {
                var phone = $scope.phone;
                $http({
                    method: 'POST',
                    params: ({
                        phoneUser: 'Dillon',
                        brand: phone.brand,
                        type: phone.type,
                        productNo: phone.productNo,
                        inputDate: phone.startDate,
                        endDate: phone.endDate
                    }),
                    url: '/api/DoubleCheck/SetTempPhone',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    alert(response.data);
                }, function error(response) {
                    alert("error");
                });
            }





        }]
    })