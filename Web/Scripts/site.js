/**
 * simple site.js initializing angular.js
 *
*/

var app = angular.module("WkiApp", ["ngRoute"]);

app.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when("/main", {
            templateUrl: "/templates/main.html",
            controller: "MainController"
        })
        .when("/about", {
            templateUrl: "/templates/about.html",
            controller: "AboutController"
        })
        .otherwise({ redirectTo: "/main" });
}]);

// global info storage for holding page-global things
app.value("info", {
    message: "Welcome here",   // Messagebox displayed when truthy
    area: "main"
});

/////////////////// Message directive
app.directive("wkMessage", function () {
    return {
        templateUrl: "/templates/wk-message.html",
        controller: "MessageController"
    };
});

app.controller("MessageController", ["$scope", "$timeout", "info", function ($scope, $timeout, info) {
    var timer = null;

    function start_timer() {
        stop_timer();
        timer = $timeout(close, 10000);
    }

    function stop_timer() {
        if (timer) $timeout.cancel(timer);
    }

    function close() {
        console.log("closing");
        info.message = "";
        stop_timer();
    }

    $scope.info = info;
    $scope.close = close

    if (info.message) start_timer();

    $scope.$watch(
        function () { return info.message },
        function (newValue, oldValue) {
            console.log("message changed from " + oldValue + " to " + newValue);
            if (newValue) {
                start_timer();
                toastr.info("message kept for 10 seconds");
            }
        }
    );
}]);

/////////////////// Global Page Controller
//app.controller("GlobalPageController", ["$scope", function ($scope) {
//    $scope.message = "inside global page controller";
//}]);

/////////////////// About Controller
app.controller("AboutController", ["$scope", "info", function ($scope, info) {
    console.log("running about controller");
    info.message = "Now in About Controller's realm.";
    info.area = "about";
}]);

/////////////////// Main Controller
app.controller("MainController", ["$scope", "info", function ($scope, info) {
    console.log("running main controller");
    info.message = "Now in Main Controller's realm.";
    info.area = "main";
}]);

/////////////// Top Navbar
app.directive("wkNavbar", function () {
    return {
        templateUrl: "/templates/wk-navbar.html"
    };
});


/////////////////// Simple Binding Demo

app.service("greeter", function () {
    this.name = "foo"
    this.greeting = function () {
        return this.name
            ? ("Hi, " + this.name + "!")
            : "";
    };
});

// WORKS (when *NOT* minified):
//app.controller("StartupController", function ($scope, greeter) {
//    $scope.greeter = greeter;
//});

// ALSO WORKS (even when minified):
app.controller("StartupController", ["$scope", "greeter", function ($scope, greeter) {
    $scope.greeter = greeter;
}]);


/////////////////// Info Loader

app.controller(
    "InfoController",
    ["$scope", "$http", "$interval", "info", function ($scope, $http, $interval, info) {
        $scope.id = 43;
        $scope.data = "hello";
        $scope.counter = 10;

        $interval(decrementCounter, 500);

        function decrementCounter() {
            if (--$scope.counter <= 0) {
                $scope.counter = 10;
            }
        }

        function onDataReceived(response) {
            $scope.data = response.data;
            info.message = "Received: " + response.data;
        }

        $scope.update = function () {
            $http.get("/api/Info/" + $scope.id)
                .then(onDataReceived);
        };
    }]);
