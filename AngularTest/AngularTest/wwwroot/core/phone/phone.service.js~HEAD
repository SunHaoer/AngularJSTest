angular.
module('core.phone').
factory('PhoneMsg', ['$resource', function($resource) {
    return $resource('phones/phones.json', {}, {
        save: {
            method: 'POST',
        },
        query: {
            method: 'GET',
            isArray: true
        }

    })
}])