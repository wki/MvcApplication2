/* Ember Website JavaScript */
// all in one file for testing.

/// switch on intellisense
/// <reference path="ember.js" />

var App = Ember.Application.create({
    rootElement: "#ember-app"
});

App.Router.map(function () {
    this.resource("card", { path: "card/:id" }, function () {
        this.resource("content");
        this.resource("options");
        this.resource("order");
    });
});

// ----------------- INDEX
App.IndexRoute = Ember.Route.extend({
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

// ------------------ CARD
App.CardRoute = Ember.Route.extend({
    model: function () {
        return { id: 42, foo: "bar" };
    }
});
