module.exports = function (grunt) {
    // grunt.loadNpmTasks('grunt-shell');
    // grunt.loadNpmTasks('grunt-open');
    grunt.loadNpmTasks('grunt-contrib-connect');
    grunt.loadNpmTasks('grunt-karma');

    grunt.initConfig({
        shell: {
            options: {
                stdout: true
            }
        },
        connect: {
            options: {
                base: '../Web'
            },
            testserver: {
                options: {
                    port: 9999
                }
            }
        },

        karma: {
            unit: {
                configFile: './test/karma.conf.js',
                autoWatch: false,
                singleRun: true
            }
        }
    });

    // grunt.registerTask('test', ['connect:testserver', 'karma:unit']);
    grunt.registerTask('test', ['karma:unit']);
};
