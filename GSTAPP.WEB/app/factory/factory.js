'use strict';

angular.module('GSTApp')

.factory('authFac',['$state','$window','$http','AppConstant','ApiFactory',function (state,window,http,AppConstant,ApiFactory ) {
        return {
            setLogin: function (data) {
                window.localStorage["userInfo"] = data.Tocken;
                window.localStorage["Name"] = data.UserName;
            },
            setLogout: function () {
                window.localStorage["userInfo"] = "";
                window.localStorage["Name"] = "";
            },
            checkLogin:function(){
                return ApiFactory.setData("Login/TestLogin",{});
             },
            getTocken:function(){
                return window.localStorage["userInfo"];
            }
        };
    }])
.factory('ApiFactory',['$http','AppConstant',function(http,AppConstant){
    return{
        setData : function(urlName,data)
        {
             return http.post((AppConstant.BaseUrl+urlName),data,{
                 headers:{
                     'Authorization': "userInfo " + window.localStorage["userInfo"]
                 }
             });
        },
        getData : function(urlName,data)
        {
            return http.post((AppConstant.BaseUrl+urlName),data,{
                 headers:{
                     'Authorization': "userInfo " + window.localStorage["userInfo"]
                 }
             });
        }
    }
}
])
;