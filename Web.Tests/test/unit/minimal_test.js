
describe("Minimal Test", function () {
    it("should pass", function () {
        var x = [];
        // expect(x).not.to.be.a('null');
        expect(x).not.toBe(null);
        expect(x).toBeTruthy();
    });

    it("should fail", function () {
        var y = false;

        expect(y).toBe(false);
    });
});

describe("Module", function () {
    var module;
    beforeEach(function () {
        module = angular.module("WkiApp");
    });

    it("should be registered", function () {
        expect(module).toBeDefined();
    });

    //it("should not resolve an unknown Controller", function () {
    //    var controller = module.controller("DummyNotExistingController");
    //    expect(controller).not.toBeDefined();
    //});
});

describe("Order Controller", function () {
    var module;
    beforeEach(function () {
        module = angular.module("WkiApp");
    });

    it("should be resolvable", function () {
        var controller = module.controller("OrderController");
        expect(controller).toBeDefined();
    });
});

describe("Generated Test", function () {
    var x;
    beforeEach(function () {
        x = true;
    });

    it("should succeed", function () {
        expect(x).toBeTruthy();
    });

    it("should fail", function () {
        expect(x).toBeFalsy();
    });
});