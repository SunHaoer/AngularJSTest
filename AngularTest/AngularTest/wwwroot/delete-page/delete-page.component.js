angular.
    module("deletePage").
    component("deletePage", {
        templateUrl: "delete-page/delete-page.template.html",
        controller: ["$scope", "$http", "$location", function deletePageCtrl($scope, $http, $location) {
            $scope.myDate = new Date();
            $scope.myDate.toLocaleDateString();//获取当前日期

            /*
             * get 'ReplacePhoneModel'
             */
            $scope.getDeletePhonePageViewModel = function () {
                $http({
                    method: 'GET',
                    params: ({
                    }),
                    url: '/api/DeletePhone/GetDeletePhonePageViewModel',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $scope.deletePhonePageViewModel = response.data;
                    var model = $scope.deletePhonePageViewModel;
                    if (model.isLogin) {
                        $scope.deleteReasonList = model.deleteReasonList;
                        $scope.phone = model.tempNewPhone;
                        $scope.phone.startDate = new Date(model.tempNewPhone.startDate);
                        $scope.phone.abandonDate = new Date(model.tempNewPhone.abandonDate);
                        $scope.phone.deleteDate = new Date(model.tempNewPhone.deleteDate);
                        $scope.phone.deleteReason = model.tempNewPhone.deleteReason;
                    } else {
                        alert('not login');
                        $location.url('/phone/errorPage');
                    }
                }, function error(response) {
                });
            }
            $scope.getDeletePhonePageViewModel();

            /*
             * submit
             */
            $scope.submitMsg = function () {
                if (true) {
                    var phone = $scope.phone;
                    $http({
                        method: 'GET',
                        params: ({
                            deleteReason: phone.deleteReason,
                            deleteDate: phone.deleteDate
                        }),
                        url: '/api/DeletePhone/SubmitMsg',
                        headers: { 'Content-Type': 'application/json' }
                    }).then(function success(response) {
                        if (response.data.isSuccess) {
                            alert('success');
                            $location.url('phone/doubleCheck');
                        } else {
                            alert('not legal');
                        }
                    }, function error(response) {
                    });
                }
            }



            //$scope.otherReason = function () {
            //    if ($scope.TempPhone.deleteReason == 'other') {
            //        $scope.isOther = true;
            //    } else {
            //        $scope.isOther = false;
            //    }
            //}


            ////日期格式化
            //$scope.formatDate = function () {
            //    var deleteDate = $scope.TempPhone.deleteDate;
            //    var year = deleteDate.getFullYear();
            //    var month = ("0" + (deleteDate.getMonth() + 1)).slice(-2);
            //    var date = ("0" + deleteDate.getDate()).slice(-2);
            //    return year + "-" + month + "-" + date;
            //}

            ////时间大小比较
            //$scope.daysBetween = function (DateOne, DateTwo) {
            //    var oneYear = DateOne.getFullYear();
            //    var twoYear = DateTwo.getFullYear();
            //    var oneMonth = ("0" + (DateOne.getMonth() + 1)).slice(-2);
            //    var twoMonth = ("0" + (DateTwo.getMonth() + 1)).slice(-2);
            //    var oneDate = ("0" + DateOne.getDate()).slice(-2);
            //    var twoDate = ("0" + DateTwo.getDate()).slice(-2);
            //    //alert(oneMonth + "\n" + twoMonth);
            //    if (oneYear != twoYear) {
            //        return oneYear >= twoYear;
            //    } else if (oneMonth != twoMonth) {
            //        return oneMonth >= twoMonth;
            //    } else {
            //        return oneDate >= twoDate;
            //    }
            //}

            //$scope.deleteReasonNotEmpty = true;
            //$scope.checkDeleteReason = function () {
            //    //alert('1');
            //    //alert($scope.TempPhone.deleteReason);
            //    if ($scope.TempPhone.deleteReason == '' || $scope.TempPhone.deleteReason == null || ($scope.TempPhone.deleteReason == 'other') && (($scope.TempPhone.otherReason == '') || ($scope.TempPhone.otherReason == null))) {
            //        $scope.deleteReasonNotEmpty = false;
            //        //alert('+');
            //    } else {
            //        $scope.deleteReasonNotEmpty = true;
            //        //alert('-');
            //    }
            //}

            ////将修改值传入newTemp
            //$scope.setNewTempPhone = function () {
            //    $scope.checkDeleteReason();
            //    //alert($scope.deleteReasonNotEmpty);
            //    if ($scope.deleteReasonNotEmpty) {
            //        $http({
            //            method: 'Post',
            //            params: ({
            //                id: $scope.TempPhone.id,
            //                phoneUser: $scope.TempPhone.phoneUser,
            //                brand: $scope.TempPhone.brand,
            //                type: $scope.TempPhone.type,
            //                productNo: $scope.TempPhone.productNo,
            //                startDate: $scope.TempPhone.startDate,
            //                endDate: $scope.TempPhone.endDate,
            //                deleteDate: $scope.formatDate(),
            //                deleteReason: $scope.TempPhone.deleteReason == 'other' ? $scope.TempPhone.otherReason : $scope.TempPhone.deleteReason,
            //                state: $scope.TempPhone.state
            //            }),
            //            url: '/api/TempPhone/SetNewTempPhone',
            //            headers: { 'Content-Type': 'application/json' }
            //        }).then(function success(response) {
            //            if ($scope.daysBetween($scope.TempPhone.deleteDate, $scope.myDate) == true) {
            //                $location.url("/phone/doubleCheck");
            //            }
            //            else {
            //                alert('DeleteDate is too early!');
            //            }
            //        }, function error(response) {
            //            //alert("error");
            //        });
            //    }

            //}

            //$scope.backToIndex = function () {
            //    //alert(1);
            //    if (confirm('Back to index?')) {
            //        $location.path('/phone/choosePage');     // ??????
            //    }
            //}


        }]

    });
