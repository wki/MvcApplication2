/* Ember Website JavaScript */
// all in one file for testing.

/// switch on intellisense
/// <reference path="ember.js" />

var App = Ember.Application.create({
    rootElement: "#ember-app"
});

App.Router.map(function () {
    this.resource("user", { path: "/user/:login" });
});

// ----------------- INDEX
App.IndexRoute = Ember.Route.extend({
    model: function () {
        return [
            { name: "Wolfgang", login: "wki" },
            { name: "Domm",     login: "domm" },
            { name: "Bevacqua", login: "bevacqua" }
        ];
    }
 });

App.IndexController = Ember.ArrayController.extend({
    renderedOn: function () {
        return new Date();
    }.property(),
    actions: {
        clicked: function () {
            alert("clicked");
        }
    }
});

// ----------------- USER

App.UserRoute = Ember.Route.extend({
    model: function (params) {
        return Ember.$.getJSON("https://api.github.com/users/" + params.login);
    }
});

