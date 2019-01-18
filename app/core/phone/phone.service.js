angular.
module("core.phone").
factory("PhoneMsg",["$resource",function($resource){
    return $resource("phones/:uname.json",{},{
        save:{
            method:"POST",
            params:{uname:"phones"}
        }
    })
}])