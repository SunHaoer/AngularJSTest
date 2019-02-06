angular.
    module('registerPage').
    component('registerPage', {
        templateUrl: 'common/register-page.template.html',
        controller: ['$scope', '$http', '$location', function RegisterPageCtrl($scope, $http, $location) {
            $scope.productNoReg = '[a-zA-Z0-9]*';;
            $scope.isRegister = true;
            $scope.today = new Date();
            $scope.today.toLocaleDateString();

           /*
            * get 'AddPhoneModel'
            */
            $scope.getAddPhoneModel = function () {
                $http({
                    method: 'GET',
                    params: ({
                    }),
                    url: '/api/AddPhone/GetAddPhoneModel',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $scope.addPhonePageViewModel = response.data;
                    var model = $scope.addPhonePageViewModel;
                    if (model.isLogin) {
                        $scope.brandList = model.brandList;
                        $scope.typeList = model.typeList;
                        $scope.phone = model.tempNewPhone;
                        if (model.tempNewPhone.startDate == "0001-01-01T00:00:00") {
                            $scope.phone.startDate = new Date($scope.today);
                        } else {
                            $scope.phone.startDate = new Date(model.tempNewPhone.startDate);
                        }
                    } else {
                        alert('not login');
                        $location.url('phone/errorPage');
                    }
                }, function error(response) {
                });
            }
            $scope.getAddPhoneModel();

            /*
             * validate branTypeProductNo 
             */
            $scope.isProdcutNoLegal = false;
            $scope.validateBrandTypeProductNo = function () {
                var phone = $scope.phone;
                $http({
                    method: 'GET',
                    params: ({
                        brand: phone.brand,
                        type: phone.type,
                        productNo: phone.productNo
                    }),
                    url: '/api/AddPhone/ValidateBrandTypeProductNo',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $scope.isProdcutNoLegal = response.data.isSuccess;
                }, function error(response) {
                });
            }

            $scope.isStartDateLegal = true;
            $scope.validateDateLegal = function () {
                var date1 = $scope.phone.startDate;
                var date2 = $scope.today;
                if (date1.getFullYear() < 1900 || date1.getFullYear() > 2100) {
                    $scope.isStartDateLegal = false;
                } else if (date1.getFullYear() != date2.getFullYear()) {
                    $scope.isStartDateLegal = date1.getFullYear() > date2.getFullYear();
                } else if (date1.getMonth() != date2.getMonth()) {
                    $scope.isStartDateLegal = date1.getMonth() > date2.getMonth();
                } else {
                    $scope.isStartDateLegal = date1.getDate() >= date2.getDate();
                }
            }

            /*
             * not empty
             */
            var parameterNotEmpty = function () {
                var phone = $scope.phone;
                return phone.productNo != null && phone.productNo != '' && phone.brand != null && phone.brand != '' && phone.type != null && phone.type != '';
            }

            /*
             * submit
             */
            $scope.isOK = true;
            $scope.submitMsg = function () {
                $scope.validateDateLegal();
                //$scope.validateBrandTypeProductNo();
                if (parameterNotEmpty() && $scope.isStartDateLegal) {
                    $scope.isOK = true;
                    var phone = $scope.phone;
                    $http({
                        method: 'GET',
                        params: ({
                            brand: phone.brand,
                            type: phone.type,
                            productNo: phone.productNo,
                            startDate: phone.startDate
                        }),
                        url: '/api/AddPhone/SubmitMsg',
                        headers: { 'Content-Type': 'application/json' }
                    }).then(function success(response) {
                        if (response.data.isSuccess) {
                            $location.path("/phone/registerCheckPage");
                        } else {
                            $scope.isOK = false;
                            //alert('not legal');
                        }
                    }, function error(response) {
                    });
                } else {
                    $scope.isOK = false;
                }
            }

            $scope.backToIndex = function () {
                if (confirm('Back to index? Data will not be saved')) {
                    $location.path('/phone/choosePage');
                }
            }
        }]
    })