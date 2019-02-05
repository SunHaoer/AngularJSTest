angular.
module('replacePage').
component('replacePage', {

    templateUrl: 'common/register-page.template.html',
    controller: ['$location', '$http', '$scope', function ReplacePage($location, $http, $scope) {

        $scope.isRegister = false;
        $scope.isReplace = true;
        $scope.myDate = new Date();
        $scope.myDate.toLocaleDateString();

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
                if (model.isLogin) {
                    $scope.brandList = model.brandList;
                    $scope.typeList = model.typeList;
                    $scope.phone = model.tempNewPhone;
                    $scope.phone.startDate = new Date(model.tempNewPhone.startDate);
                    $scope.oldPhone = model.tempOldPhone;
                    $scope.oldPhone.startDate = new Date(model.tempOldPhone.startDate);
                    $scope.oldPhone.abandonDate = new Date(model.tempOldPhone.abandonDate);
                } else {
                    alert('not login');
                    $location.url('phone/errorPage');
                }
            }, function error(response) {
            });
        }
        $scope.ReplacePhoneModel();

        /*
         * submit
         */
        $scope.submitMsg = function () {
            if (true) {
                var phone = $scope.phone;
                $http({
                    method: 'GET',
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
                        alert('success');
                        $location.path("/phone/replaceCheckPage");
                    } else {
                        alert('not legal');
                    }
                }, function error(response) {
                });
            }
        }


        $scope.cancle = function(phone) {
            $location.url('/phone');
        }

        $scope.backToIndex = function () {
            //alert(1);
            if (confirm('Back to index? Data will not be saved')) {
                $location.path('/phone/choosePage');     
            }
        }

        $scope.eqAbandonDate = function () {
            //alert($scope.phone.inputDate);
            $scope.oldPhone.abandonDate = $scope.phone.inputDate;
        }

        $scope.twoDateIsEqual = true;
        $scope.abandonDateIsLegal = function (date1, date2) {
            //alert(date1 + '\n' + date2);
            if (date1.getFullYear() != date2.getFullYear() || date1.getMonth() != date2.getMonth() || date1.getDate() != date2.getDate()) {
                $scope.twoDateIsEqual = false;
            } else {
                $scope.twoDateIsEqual = true;
            }
        }

        $scope.inputDateIsLegal = true;
        $scope.validateInputDate = function () {
            //alert(new Date(1900, 1, 1, 0, 0, 0, 0));
            if ($scope.phone.inputDate < new Date(1900, 1, 1, 0, 0, 0, 0) || $scope.phone.inputDate > new Date(2100, 1, 1, 0, 0, 0, 0)) {
                $scope.inputDateIsLegal = false;
            } else {
                $scope.inputDateIsLegal = true;
            }
        }
    }]

})