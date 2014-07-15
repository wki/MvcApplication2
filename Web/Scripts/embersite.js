/* Ember Website JavaScript */
// all in one file for testing.

/// switch on intellisense
/// <reference path="ember.js" />

var App = Ember.Application.create({
    rootElement: "#ember-app"
});

App.Router.map(function () {
    this.resource("card", { path: "card/:id" }, function () {
        this.route("content");
        this.route("options");
        this.route("order");
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

App.CardController = Ember.ObjectController.extend({
    area: "foo"
});

App.CardRoute = Ember.Route.extend({
    model: function () {
        return { id: 42, foo: "bar" };
    }
});

App.CardRoute = Ember.Route.extend({
    setupController: function (controller, model) {
        controller.set('area', 'content');
    }
});

App.CardContentRoute = Ember.Route.extend({
    setupController: function (controller, model) {
        this.controllerFor('CardController').set('area', 'content');
    }
});

App.CardOptionsRoute = Ember.Route.extend({
    setupController: function (controller, model) {
        this.controllerFor('CardController').set('area', 'options');
    }
});

App.CardOrderRoute = Ember.Route.extend({
    setupController: function (controller, model) {
        this.controllerFor('CardController').set('area', 'order');
    }
});
