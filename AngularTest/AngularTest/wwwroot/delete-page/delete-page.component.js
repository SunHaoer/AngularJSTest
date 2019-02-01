angular.
    module("deletePage").
    component("deletePage", {
        templateUrl: "delete-page/delete-page.template.html",
        controller: ["$scope", "$http", "$location", function deletePageCtrl($scope, $http, $location) {
            $scope.myDate = new Date();
            $scope.myDate.toLocaleDateString();//获取当前日期

            $scope.checkLogin = function () {   // 需提取
                $http({
                    method: 'GET',
                    params: ({
                    }),
                    url: '/api/Phone/CheckLogin',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    if (response.data['notLogin'] == 'true') {
                        $location.url('/phone/errorPage');
                    }
                }, function error(response) {
                    //alert("error");
                });
            }
            $scope.checkLogin();

            //导入数据
            $scope.getNewTempPhone = function () {
                $http({
                    method: 'GET',
                    params: ({

                    }),
                    url: '/api/TempPhone/GetNewTempPhone',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    $scope.TempPhone = response.data;
                    if ($scope.TempPhone.deleteDate == "0001-01-01T00:00:00") {
                        $scope.TempPhone.deleteDate = new Date($scope.myDate);
                    } else {
                        $scope.TempPhone.deleteDate = new Date($scope.TempPhone.deleteDate);
                    }
                    if ($scope.TempPhone.deleteReason != null && $scope.TempPhone.deleteReason != '' && $scope.TempPhone.deleteReason != 'lost' && $scope.TempPhone.deleteReason != 'buy new phone' && $scope.TempPhone.deleteReason != 'time end') {
                        $scope.TempPhone.otherReason = $scope.TempPhone.deleteReason;
                        $scope.TempPhone.deleteReason = 'other';
                    }
                }, function error(response) {
                    //alert("error");
                });
            }
            $scope.getNewTempPhone();



            $scope.otherReason = function () {
                if ($scope.TempPhone.deleteReason == 'other') {
                    $scope.isOther = true;
                } else {
                    $scope.isOther = false;
                }
            }


            //日期格式化
            $scope.formatDate = function () {
                var deleteDate = $scope.TempPhone.deleteDate;
                var year = deleteDate.getFullYear();
                var month = ("0" + (deleteDate.getMonth() + 1)).slice(-2);
                var date = ("0" + deleteDate.getDate()).slice(-2);
                return year + "-" + month + "-" + date;
            }

            //时间大小比较
            $scope.daysBetween = function (DateOne, DateTwo) {
                var oneYear = DateOne.getFullYear();
                var twoYear = DateTwo.getFullYear();
                var oneMonth = ("0" + (DateOne.getMonth() + 1)).slice(-2);
                var twoMonth = ("0" + (DateTwo.getMonth() + 1)).slice(-2);
                var oneDate = ("0" + DateOne.getDate()).slice(-2);
                var twoDate = ("0" + DateTwo.getDate()).slice(-2);
                //alert(oneMonth + "\n" + twoMonth);
                if (oneYear != twoYear) {
                    return oneYear >= twoYear;
                } else if (oneMonth != twoMonth) {
                    return oneMonth >= twoMonth;
                } else {
                    return oneDate >= twoDate;
                }
            }

            $scope.deleteReasonNotEmpty = true;
            $scope.checkDeleteReason = function () {
                //alert('1');
                //alert($scope.TempPhone.deleteReason);
                if ($scope.TempPhone.deleteReason == '' || $scope.TempPhone.deleteReason == null || ($scope.TempPhone.deleteReason == 'other') && (($scope.TempPhone.otherReason == '') || ($scope.TempPhone.otherReason == null))) {
                    $scope.deleteReasonNotEmpty = false;
                    //alert('+');
                } else {
                    $scope.deleteReasonNotEmpty = true;
                    //alert('-');
                }
            }

            //将修改值传入newTemp
            $scope.setNewTempPhone = function () {
                $scope.checkDeleteReason();
                //alert($scope.deleteReasonNotEmpty);
                if ($scope.deleteReasonNotEmpty) {
                    $http({
                        method: 'Post',
                        params: ({
                            id: $scope.TempPhone.id,
                            phoneUser: $scope.TempPhone.phoneUser,
                            brand: $scope.TempPhone.brand,
                            type: $scope.TempPhone.type,
                            productNo: $scope.TempPhone.productNo,
                            startDate: $scope.TempPhone.startDate,
                            endDate: $scope.TempPhone.endDate,
                            deleteDate: $scope.formatDate(),
                            deleteReason: $scope.TempPhone.deleteReason == 'other' ? $scope.TempPhone.otherReason : $scope.TempPhone.deleteReason,
                            state: $scope.TempPhone.state
                        }),
                        url: '/api/TempPhone/SetNewTempPhone',
                        headers: { 'Content-Type': 'application/json' }
                    }).then(function success(response) {
                        if ($scope.daysBetween($scope.TempPhone.deleteDate, $scope.myDate) == true) {
                            $location.url("/phone/doubleCheck");
                        }
                        else {
                            alert('DeleteDate is too early!');
                        }
                    }, function error(response) {
                        //alert("error");
                    });
                }

            }

            $scope.backToIndex = function () {
                //alert(1);
                if (confirm('Back to index?')) {
                    $location.path('/phone/choosePage');     // ??????
                }
            }
            $scope.GetdeleteReason = function () {
                $http({
                    method: 'Get',
                    params: ({

                    }),
                    url: '/api/DeleteReasonModel/InitDeleteReasonDB',

                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {

                }, function error(response) {
                    alert('error');
                })
            }
            $scope.GetdeleteReason();
            $scope.getListAboutdeleteReason = function () {
                $http({
                    method: 'Get',
                    params: ({

                    }),
                    url: '/api/DeleteReasonModel/GetdeleteReasonAll',
                    headers: { 'Content-Type': 'application/json' }
                }).then(function success(response) {
                    var list = response.data;
                    $scope.deleteReasonList = [];
                    for (var i = 0; i < list.length; i++) {
                        $scope.deleteReasonList.push(list[i]["deleteReason"]);
                    }
                }, function error(response) {

                });
            }

        }]

    });
