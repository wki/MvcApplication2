﻿using System.Web;
using System.Web.Optimization;

namespace Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(
                new ScriptBundle("~/bundles/angular").Include(
                    "~/Scripts/angular.js",
                    "~/Scripts/angular-animate.js",
                    "~/Scripts/angular-resource.js",
                    "~/Scripts/angular-route.js"
                )
            );

            bundles.Add(
                new Bundle("~/bundles/ember").Include(
                    "~/Scripts/jquery-{version}.js",
                    "~/Scripts/handlebars.js",
                    "~/Scripts/ember.js"
                )
            );

            bundles.Add(
                new Bundle("~/bundles/embersite").Include(
                    "~/Scripts/embersite.js"
                )
            );


            // Hint: a Bundle is not minified
            bundles.Add(new Bundle("~/bundles/site").Include(
                      "~/Scripts/jquery-{version}.js",
                      "~/Scripts/toastr.js",
                      "~/Scripts/site.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/toastr.css",
                      "~/Content/site.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
