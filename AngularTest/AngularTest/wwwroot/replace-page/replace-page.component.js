angular.
module('replacePage').
component('replacePage', {
    templateUrl: 'common/register-page.template.html',
    controller: ['$location', '$http', '$scope', function ReplacePage($location, $http, $scope) {
        var yalertStylePath = 'css/yalert.css';
        $scope.isReplace = true;
        $scope.today = new Date();
        $scope.today.toLocaleDateString();
        $scope.isBack = false;

        /*
         * get 'ReplacePhoneModel'
         */
        $scope.getReplacePhoneModel = function () {
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
                    if ($scope.phone.phoneUser != null) {
                        $scope.isBack = true;
                    }
                } else {
                    showAlert('hint', 'not login or illegal visit', yalertStylePath, '');
                    $location.url('phone/errorPage');
                }
            }, function error(response) {
            });
        }
        $scope.getReplacePhoneModel();

        $scope.isProductNoClick = false;
        $scope.productNoClick = function (value) {
            if (value == 1) {
                $scope.isProductNoClick = true;
            } else if (value == 2) {
                $scope.isProductNoClick = false;
            }
        }

        $scope.isStartDateClick = false;
        $scope.startDateClick = function (value) {
            if (value == 1) {
                $scope.isStartDateClick = true;
            } else if (value == 2) {
                $scope.isStartDateClick = false;
            }
        }

        $scope.isAbandonDateClick = false;
        $scope.abandonDateClick = function (value) {
            if (value == 1) {
                $scope.isAbandonDateClick = true;
            } else if (value == 2) {
                $scope.isAbandonDateClick = false;
            }
        }

        /*
         * validate branTypeProductNo 
         */
        $scope.isProdcutNoLegal = true;
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

        $scope.isPhoneBrandNotEmpty = true;
        $scope.isPhoneTypeNotEmpty = true;
        $scope.isProductNoNotEmpty = true;
        $scope.phoneBrandNotEmpty = function () {
            var phone = $scope.phone;
            $scope.isPhoneBrandNotEmpty = true;
            if (phone.brand == '' || phone.brand == null) {
                $scope.isPhoneBrandNotEmpty = false;
            }
        }

        $scope.phoneTypeNotEmpty = function () {
            var phone = $scope.phone;
            $scope.isPhoneTypeNotEmpty = true;
            if (phone.type == '' || phone.type == null) {
                $scope.isPhoneTypeNotEmpty = false;
            }
        }

        $scope.productNoNotEmpty = function () {
            var phone = $scope.phone;
            $scope.isProductNoNotEmpty = true;
            if (phone.productNo == '' || phone.productNo == null) {
                $scope.isProductNoNotEmpty = false;
            }
        }

        /*
         * not empty
         */
        $scope.parameterNotEmpty = function () {
            $scope.phoneBrandNotEmpty();
            $scope.phoneTypeNotEmpty();
            $scope.productNoNotEmpty();
            $scope.isPrameterNotEmpty = $scope.isPhoneBrandNotEmpty && $scope.isPhoneTypeNotEmpty && $scope.isProductNoNotEmpty;
        }

        $scope.validate = function () {
            $scope.validateDateLegal();
            $scope.validateBrandTypeProductNo();
            $scope.parameterNotEmpty();
            $scope.validateAbandonDateLegal();
            $scope.isOK = $scope.isPrameterNotEmpty && $scope.isStartDateLegal && $scope.isProdcutNoLegal && $scope.isAbandonDateLegal;
        }

        /*
         * submit
         */
        $scope.isOK = true;
        $scope.isPrameterNotEmpty = true;
        $scope.submitMsg = function () {
            $scope.validateDateLegal();
            $scope.validateAbandonDateLegal();
            if ($scope.isPrameterNotEmpty && $scope.isStartDateLegal && $scope.isAbandonDateLegal) {
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