/**
 * simple site.js initializing angular.js
 *
*/

var app = angular.module("WkiApp", []);

app.service("greeter", function () {
    this.name = "foo"
    this.greeting = function () {
        return this.name
            ? ("Hello, " + this.name + "!")
            : "";
    };
});

app.controller("StartupController", function ($scope, greeter) {
    $scope.greeter = greeter;
});
