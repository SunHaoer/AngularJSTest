angular.
module('common').
component('successPage', {

    templateUrl: 'common/success-page.template.html',
    controller: ['$scope', '$timeout', '$location', function ChoosePageCtrl($scope, $timeout, $location) {

        $timeout(function() {
            $location.url('/phone');
        }, 3000);

    }]
})