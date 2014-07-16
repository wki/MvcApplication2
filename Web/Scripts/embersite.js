/* Ember Website JavaScript */
// all in one file for testing.

/// switch on intellisense
/// <reference path="ember.js" />

var App = Ember.Application.create({
    rootElement: "#ember-app",
    LOG_TRANSITIONS: true,
    LOG_TRANSITIONS_INTERLAL: true

});

App.Router.map(function () {
    this.resource("card", { path: "/card/:card_id" }, function () {
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
    area: "foo",
});

App.CardRoute = Ember.Route.extend({
    model: function (params) {
        //console.log(params);
        return { id: params.card_id, foo: "bar-" + params.card_id };
    }
    //,
    //setupController: function (controller, model) {
    //    controller.set('area', 'content');
    //}
});

App.CardIndexRoute = Ember.Route.extend({
    afterModel: function (card) {
        //console.log("redirecting. card =");
        //console.log(card);
        this.transitionTo('card.content', card.id);
    }
});

App.CardContentController = Ember.ObjectController.extend({
    name: 'foo',
    department: 'IT'
});

App.CardContentRoute = Ember.Route.extend({
    //model: function() {
    //    return this.modelFor('card');
    //},
    setupController: function (controller, model) {
        console.log('model for card.content');
        console.log(model);
        this.controllerFor('Card').set('area', 'content');
    }
});

App.CardOptionsRoute = Ember.Route.extend({
    setupController: function (controller, model) {
        this.controllerFor('Card').set('area', 'options');
    }
});

App.CardOrderRoute = Ember.Route.extend({
    setupController: function (controller, model) {
        this.controllerFor('Card').set('area', 'order');
    }
});
