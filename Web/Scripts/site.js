/**
 * simple site.js initializing angular.js
 *
*/

(function () {
    var app = angular.module("WkiApp", []);

    /////////////////// Simple Binding Demo

    app.service("greeter", function () {
        this.name = "foo"
        this.greeting = function () {
            return this.name
                ? ("Hello, " + this.name + "!")
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
        ["$scope", "$http", "$interval", function ($scope, $http, $interval)
    {
        $scope.id = 43;
        $scope.data = "hello";
        $scope.counter = 10;

        $interval(decrementCounter, 500, $scope.counter);

        function decrementCounter() {
            if (--$scope.counter <= 0) {
                $scope.counter = 10;
            }
        }

        function onDataReceived(response) {
            $scope.data = response.data;
        }

        $scope.update = function () {
            $http.get("/api/Info/" + $scope.id)
                .then(onDataReceived);
        };
    }]);

}());