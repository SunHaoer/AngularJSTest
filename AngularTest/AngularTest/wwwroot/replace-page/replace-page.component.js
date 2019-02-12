angular.
module('replacePage').
component('replacePage', {
    templateUrl: 'common/register-page.template.html',
    controller: ['$location', '$http', '$scope', function ReplacePage($location, $http, $scope) {
        var yalertStylePath = 'css/yalert.css';
        $scope.isReplace = true;
        $scope.today = new Date();
        $scope.today.toLocaleDateString();

        /*
         * get 'ReplacePhoneModel'
         */
        $scope.ReplacePhoneModel = function () {
            $http({
                method: 'GET',
                params: ({
                }),
                url: '/api/ReplacePhone/GetReplacePhonePageViewModel',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                $scope.replacePhoneModel = response.data;
                var model = $scope.replacePhoneModel;
                if (model.isLogin && model.isVisitLegal) {
                    $scope.brandList = model.brandList;
                    $scope.typeList = model.typeList;
                    $scope.phone = model.tempNewPhone;
                    if (model.tempNewPhone.startDate == "0001-01-01T00:00:00") {
                        $scope.phone.startDate = new Date($scope.today);
                    } else {
                        $scope.phone.startDate = new Date(model.tempNewPhone.startDate);
                    }
                    $scope.oldPhone = model.tempOldPhone;
                    if (model.tempOldPhone.startDate == "0001-01-01T00:00:00") {
                        $scope.oldPhone.startDate = new Date($scope.today);
                    } else {
                        $scope.oldPhone.startDate = new Date(model.tempOldPhone.startDate);
                    }
                    if (model.tempOldPhone.abandonDate == "0001-01-01T00:00:00") {
                        $scope.oldPhone.abandonDate = new Date($scope.today);
                    } else {
                        $scope.oldPhone.abandonDate = new Date(model.tempOldPhone.abandonDate);
                    }
                } else {
                    showAlert('hint', 'not login or illegal visit', yalertStylePath, '');
                    $location.url('phone/errorPage');
                }
            }, function error(response) {
            });
        }
        $scope.ReplacePhoneModel();

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
                url: '/api/ReplacePhone/ValidateBrandTypeProductNo',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                $scope.isProdcutNoLegal = response.data.isSuccess;
            }, function error(response) {
            });
        }

        $scope.isStartDateLegal = true;
        $scope.validateDateLegal = function () {
            try {
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
            } catch {
                $scope.isStartDateLegal = false;
            }

        }

        $scope.isAbandonDateLegal = true;
        $scope.validateAbandonDateLegal = function () {
            var date1 = $scope.phone.startDate;
            var date2 = $scope.oldPhone.abandonDate;
            if (date1.getFullYear() != date2.getFullYear() || date1.getMonth() != date2.getMonth() || date1.getDate() != date2.getDate()) {
                $scope.isAbandonDateLegal = false;
            } else {
                $scope.isAbandonDateLegal = true;
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
            $scope.validateAbandonDateLegal();
            if (!$scope.isAbandonDateLegal) {
                $location.url('phone/errorPage');
            } else if (parameterNotEmpty() && $scope.isStartDateLegal) {
                $scope.isOK = true;
                var phone = $scope.phone;
                $http({
                    method: 'POST',
                    params: ({
                        brand: phone.brand,
                        type: phone.type,
                        productNo: phone.productNo,
                        startDate: phone.startDate,
                        abandonDate: $scope.oldPhone.abandonDate
                    }),
                    url: '/api/ReplacePhone/SubmitMsg',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    if (response.data.isSuccess) {
                        $location.path("/phone/replaceCheckPage");
                    } else {
                        $scope.isOK = false;
                        $scope.validateBrandTypeProductNo();
                    }
                }, function error(response) {
                });
            } else {
                $scope.isOK = false;
            }
        }

        $scope.backToIndex = function () {
            $http({
                method: 'GET',
                params: ({
                }),
                url: '/api/ReplacePhone/SetIsSubmit',
                headers: { 'Content-Type': 'application/json' }
            }).then(function success(response) {
                if (response.data.isSuccess) {
                    showConfirm('', 'Back to index? Data will not be saved', yalertStylePath, function () {
                        window.location.href = '#!/phone/choosePage';
                    }, function () {
                    })
                }
            }, function error(response) {
            });
        }
    }]
})