angular.
module('phone').
config(['$routeProvider',
    function config($routeProvider) {
        $routeProvider.
        when('/phone', {
            template: '<choose-page></choose-page>'
        }).
        when('/phone/registerPage', {
            template: '<register-page></register-page>'
        }).
        when('/phone/updatePage', {
            template: '<update-page></update-page>'
        }).
        when('/phone/deletePage', {
            template: '<delete-page></delete-page>'
            }).
        when('/phone/doubleCheck', {
            template: '<double-check></double-check>'
        }).
        otherwise('/phone');
    }
]);