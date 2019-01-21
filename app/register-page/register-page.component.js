angular.
module('registerPage').
component('registerPage', {
    templateUrl: 'register-page/register-page.template.html',
    controller: ['PhoneMsg', function RegisterPageCtrl(PhoneMsg) {
        this.phone = new PhoneMsg();
        this.brandRegex = '\\d+';
        this.brandList = ['SAMSUNG', 'IPHONE', 'OPPO', 'HUAWEI'];
        this.ps = PhoneMsg.query();

       
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