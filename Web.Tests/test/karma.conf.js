module.exports = function (config) {
    config.set({
        basePath: '../',
        frameworks: ['mocha'],
        reporters: ['progress', 'dots'],
        browsers: ['Chrome'],
        autoWatch: false,
        port: 9876,
        logLevel: config.LOG_INFO,

        // these are default values anyway
        singleRun: false,
        colors: false,

        files: [
          // 3rd Party Code
          '../Web/Scripts/angular.js',
          '../Web/Scripts/angular-route.js',

          // App-specific Code
          '../Web/Scripts/site.js',

          // Test-Specific Code
          'test/mocha.conf.js',
          'node_modules/chai/chai.js',
          'test/lib/chai-sugar.js',

          // unit tests
          'test/unit/**/*.js'
        ]
    });
};
