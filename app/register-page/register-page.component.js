angular.
module('registerPage').
component('registerPage', {
    templateUrl: 'common/register-page.template.html',
    controller: ['$scope', '$http', '$location', function RegisterPageCtrl($scope, $http, $location) {
        $scope.brandRegex = '\\d+';
        $scope.flag = false;
        
        // ע������ж�
        $scope.isRegister = true;
        $scope.isReplace = false;

        /**
         * ��ȡ�����ֻ�Ʒ��
         * */
        $scope.getBrandAll = function() {
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
                alert("brand error");
            });
        }
        $scope.getBrandAll();

        /**
         * ����Ʒ�ƻ�ȡ�ͺ�
         * */
        $scope.getTypeByBrand = function() {
            var phone = $scope.phone;
            $http({
                method: 'GET',
                params: ({
                    brand: phone.brand,
                }),
                url: '/api/BrandTypeModels/GetTypeByBrand',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                var list = response.data;
                $scope.typeList = [];
                for (var i = 0; i < list.length; i++) {
                    $scope.typeList.push(list[i]["type"]);
                }
            }, function error(response) {
                alert("type error");
            });
        }
        /**
         * �����ͺŻ�ȡ������
         * */
        $scope.getYearByType = function() {
            var typeFlag = $scope.phone.type;
            if (typeFlag != "none") {
                console.log(typeFlag);
                $scope.flag = true;
            } else {
                $scope.flag = false;
            }
            $http({
                method: 'GET',
                params: ({
                    type: $scope.phone.type
                }),
                url: '/api/TypeYear/GetYearByType',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                $scope.phone.life = response.data;
            }, function error(response) {});
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
            var startDate = year + '/' + month + '/' + date;
            var endDate = (year + $scope.phone.life) + '/' + month + '/' + date;
            $scope.phone.startDate = startDate;         
            $scope.phone.endDate = endDate;

        }        

        $scope.cancle = function() {
            $location.url('/phone');
        }

        // ���ȷ��
        $scope.submitMsg = function(phone) {

            // ���������ֻ�����tempPhone
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
                $location.url('/phone/registerCheckPage');

            }, function errorCallback(response) {
                // ����ʧ��ִ�д���
                alert('error');
            });

        };
    }]
})