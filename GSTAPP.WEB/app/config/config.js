'use strict';

angular.module('GSTApp')
.constant('AppConstant', {
    //BaseUrl: 'http://192.168.0.122/GSTAPP.WEB/api/',
    BaseUrl: 'http://localhost/GSTAPP.WEB/api/',
    //ViewUrl: 'http://192.168.0.122/GSTAPP.WEB/'
    ViewUrl: 'http://localhost/GSTAPP.WEB/'

})
.run(function ($http) {
    $http.defaults.headers.common.Authorization = "userInfo " + window.localStorage["userInfo"];
})
.config(['$stateProvider', '$urlRouterProvider', '$httpProvider', 'ChartJsProvider', function (stateProvider, urlRouterProvider, httpProvider, ChartJsProvider) {
    httpProvider.interceptors.push(function ($q, $state, toasterService) {

        return {

            'responseError': function (rejection) {

                var defer = $q.defer();

                if (rejection.data.InnerException != null && rejection.data.InnerException.ExceptionMessage == "401") {
                    console.clear();
                    $state.go('login');
                }
                else if (rejection.status == 500) {
                    toasterService.error("Request error !");
                    //console.log(rejection);
                }

                defer.reject(rejection);

                return defer.promise;

            }
        };
    });
    //ChartJsProvider.setOptions({ colors: ['#803690', '#00ADF9', '#DCDCDC', '#46BFBD', '#FDB45C', '#949FB1', '#4D5360'] });
    stateProvider.state('app', {
        url: '/app',
        views: {
            '': {
                templateUrl: 'views/layout.html',
                controller: 'layoutCtrl'
            }
        }
    })
    .state('controles', {
        url: '/controles',
        views: {
            '': {
                templateUrl: 'views/controles.html',
                controller: 'controlesCtrl'
            }
        }
    })
    .state('login', {
        url: '/login',
        views: {
            '': {
                templateUrl: 'views/loginPage.html',
                controller: 'loginCtrl'
            }
        }
    })
    .state('registration', {
        url: '/registration',
        views: {
            '': {
                templateUrl: 'views/registrationPage.html',
                controller: 'registrationCtrl'
            }
        }
    })
        .state('validation', {
            url: '/validation',
            views: {
                '': {
                    templateUrl: 'views/validationPage.html',
                    controller: 'validationCtrl'
                }
            }
        })
        .state('EmailVerification', {
            url: '/EmailVerification/:Email/:EmailVerificationCode',
            views: {
                '': {
                    templateUrl: 'views/EmailVerificationForm.html',
                    controller: 'EmailVerificationCtrl'
                }
            }
        })
.state('app.profilepage', {
    url: '/profilepage',
    views: {
        '': {
            templateUrl: 'views/profilepage/profilepage.html',
            controller: 'profilepageCtrl'
        }
    }
})
            .state('app.company', {
                url: '/company',
                views: {
                    '': {
                        templateUrl: 'views/company/company.html',
                        controller: 'companyCtrl'
                    }
                }
            })
.state('app.companyForm', {
    url: '/companyForm/:Code',
    views: {
        '': {
            templateUrl: 'views/company/companyForm.html',
            controller: 'companyFormCtrl'
        }
    }
})
.state('app.customer', {
    url: '/customer',
    views: {
        '': {
            templateUrl: 'views/customer/Customer.html',
            controller: 'customerCtrl'
        }
    }
})
        .state('app.customerForm', {
            url: '/customerForm/:Code',
            views: {
                '': {
                    templateUrl: 'views/customer/customerForm.html',
                    controller: 'customerFormCtrl'
                }
            }
        })
.state('app.supplier', {
    url: '/supplier',
    views: {
        '': {
            templateUrl: 'views/supplier/Supplier.html',
            controller: 'supplierCtrl'
        }
    }
})
.state('app.supplierForm', {
    url: '/supplierForm/:Code',
    views: {
        '': {
            templateUrl: 'views/supplier/SupplierForm.html',
            controller: 'supplierFormCtrl'
        }
    }
})

    .state('app.productgroup', {
        url: '/productgroup',
        views: {
            '': {
                templateUrl: 'views/productgroup/ProductGroup.html',
                controller: 'productgroupCtrl'
            }
        }
    })
    .state('app.productgroupForm', {
        url: '/productgroupForm/:Code',
        views: {
            '': {
                templateUrl: 'views/productgroup/ProductgroupForm.html',
                controller: 'productgroupFormCtrl'
            }
        }
    })


     .state('app.itemtype', {
         url: '/itemtype',
         views: {
             '': {
                 templateUrl: 'views/itemtype/itemtype.html',
                 controller: 'itemtypeCtrl'
             }
         }
     })
    .state('app.itemtypeForm', {
        url: '/itemtypeForm/:Code',
        views: {
            '': {
                templateUrl: 'views/itemtype/itemtypeForm.html',
                controller: 'itemtypeFormCtrl'
            }
        }
    })
    .state('app.itemcategory', {
        url: '/itemcategory',
        views: {
            '': {
                templateUrl: 'views/itemcategory/ItemCategory.html',
                controller: 'itemcategoryCtrl'
            }
        }
    })
    .state('app.itemcategoryForm', {
        url: '/itemcategoryForm/:Code',
        views: {
            '': {
                templateUrl: 'views/itemcategory/itemcategoryForm.html',
                controller: 'itemcategoryFormCtrl'
            }
        }
    })

    .state('app.terms', {
        url: '/terms',
        views: {
            '': {
                templateUrl: 'views/terms/Terms.html',
                controller: 'termsCtrl'
            }
        }
    })
    .state('app.termsForm', {
        url: '/termsForm/:Code',
        views: {
            '': {
                templateUrl: 'views/terms/termsForm.html',
                controller: 'termsFormCtrl'
            }
        }
    })
    .state('app.department', {
        url: '/department',
        views: {
            '': {
                templateUrl: 'views/department/Department.html',
                controller: 'departmentCtrl'
            }
        }
    })
    .state('app.departmentForm', {
        url: '/departmentForm/:Code',
        views: {
            '': {
                templateUrl: 'views/department/DepartmentForm.html',
                controller: 'departmentFormCtrl'
            }
        }
    })
    .state('app.product', {
        url: '/product',
        views: {
            '': {
                templateUrl: 'views/product/product.html',
                controller: 'productCtrl'
            }
        }
    })
     .state('app.productForm', {
         url: '/productForm/:Code',
         views: {
             '': {
                 templateUrl: 'views/product/productForm.html',
                 controller: 'productFormCtrl'
             }
         }
     })
    .state('app.purchaseorder', {
        url: '/purchaseorder',
        views: {
            '': {
                templateUrl: 'views/purchaseorder/PurchaseOrder.html',
                controller: 'purchaseorderCtrl'
            }
        }
    })
    .state('app.purchaseorderForm', {
        url: '/purchaseorderForm/:Code',
        views: {
            '': {
                templateUrl: 'views/purchaseorder/purchaseorderForm.html',
                controller: 'purchaseorderFormCtrl'
            }
        }
    })
    .state('app.purchaseinvoice', {
        url: '/purchaseinvoice',
        views: {
            '': {
                templateUrl: 'views/purchaseinvoice/purchaseinvoice.html',
                controller: 'purchaseinvoiceCtrl'
            }
        }
    })
     .state('app.purchaseinvoiceForm', {
         url: '/purchaseinvoiceForm/:Code',
         views: {
             '': {
                 templateUrl: 'views/purchaseinvoice/purchaseinvoiceForm.html',
                 controller: 'purchaseinvoiceFormCtrl'
             }
         }
     })

    .state('app.salesinvoice', {
        url: '/salesinvoice',
        views: {
            '': {
                templateUrl: 'views/salesinvoice/salesinvoice.html',
                controller: 'salesinvoiceCtrl'
            }
        }
    })
     .state('app.salesinvoiceForm', {
         url: '/salesinvoiceForm/:Code',
         views: {
             '': {
                 templateUrl: 'views/salesinvoice/salesinvoiceForm.html',
                 controller: 'salesinvoiceFormCtrl'
             }
         }
     })
    .state('app.salesorder', {
        url: '/salesorder',
        views: {
            '': {
                templateUrl: 'views/salesorder/salesorder.html',
                controller: 'salesorderCtrl'
            }
        }
    })
     .state('app.salesorderForm', {
         url: '/salesorderForm/:Code',
         views: {
             '': {
                 templateUrl: 'views/salesorder/salesorderForm.html',
                 controller: 'salesorderFormCtrl'
             }
         }
     })
    .state('registrationSuccess', {
        url: '/registrationsuccess',
        views: {
            '': {
                templateUrl: 'views/RegistrationSuccess.html',
                controller: 'registrationsuccessCtrl'
            }
        }
    })
     .state('EmailVerificationSuccess', {
         url: '/EmailVerificationSuccess',
         views: {
             '': {
                 templateUrl: 'views/EmailVerificationSuccess.html',
                 controller: 'EmailVerificationSuccessCtrl'
             }
         }
     })
    .state('resendSuccess', {
        url: '/resendsuccess',
        views: {
            '': {
                templateUrl: 'views/ResendSuccess.html',
                controller: 'resendsuccessCtrl'
            }
        }
    })
     .state('resend', {
         url: '/resend/:Email',
         views: {
             '': {
                 templateUrl: 'views/Resend.html',
                 controller: 'resendCtrl'
             }
         }
     })

    ;
    urlRouterProvider.otherwise('/login');
}])
;