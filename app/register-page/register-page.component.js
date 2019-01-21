angular.
module('registerPage').
component('registerPage', {
    templateUrl: 'register-page/register-page.template.html',
    controller: ['PhoneMsg', '$http', function RegisterPageCtrl(PhoneMsg, $http) {
        this.phone = new PhoneMsg();
        this.brandRegex = '\\d+';
        this.brandList = ['SAMSUNG', 'IPHONE', 'OPPO', 'HUAWEI'];
        this.ps = PhoneMsg.query();
				
				this.save = function() {
					alert('send to java');
					var phone = this.phone;
					$http({
						method: 'POST',
						params: ({
							brand: phone.brand,
							type: phone.type,
							productNo: phone.productNo
						}),
						url: 'http://localhost:8120/phone/save',
						headers: { 'Content-Type': 'application/json' } 
					}).then(function success(response) {
						alert(response.data.result);
					}, function error(response) {
						alert("error");
					});
				}

       
        this.submitMsg = function () {
            console.log(this.phone);
            var json = JSON.stringify(this.phone);
                console.log(json);
            PhoneMsg.save(json, function (data) {
                json = data;
            },function(resp){
                console.log('error');
            });
        }


    }]
})