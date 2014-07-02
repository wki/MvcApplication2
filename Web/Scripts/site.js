/**
 * simple site.js initializing angular.js
 *
*/

var app = angular.module("WkiApp", ["ngResource"]);

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

app.factory("infoResource", function ($resource) {
    return $resource("/api/Info/:id", { id: "@id" });
});

app.service("info", function (infoResource) {
    this.id = 5;
    this.data = function () {
        console.log(infoResource);
        // return infoResource.get({ id: this.id });
        return "xxx" + this.id;
    };
});

app.controller("InfoController", ["$scope", "info", function ($scope, info) {
    $scope.info = info;
}]);
