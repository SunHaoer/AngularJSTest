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
        when('/phone/replacePage', {
            template: '<replace-page></replace-page>'
        }).
        when('/phone/deletePage', {
            template: '<delete-page></delete-page>'
        }).
        when('/phone/checkPage', {
            template: '<check-page></check-page>'
        }).
        when('/phone/registerCheckPage', {
            template: '<registerCheck-page></registerCheck-page>'
        }).
        when('/phone/replaceCheckPage', {
            template: '<replaceCheck-page></replaceCheck-page>'
        }).
        when('/phone/successPage', {
            template: '<success-page></success-page>'
        }).
        when('/phone/errorPage', {
            template: '<error-page></error-page>'
        }).
        otherwise('/phone');
    }
]);