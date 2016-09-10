﻿using System.Web;
using System.Web.Optimization;

namespace HitechCraft.WebAdmin
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.*",
                        "~/Scripts/jquery.jgrowl*",
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.fileupload*",
                        "~/Scripts/jquery.ui*",
                        "~/Scripts/jquery-ui*",
                        "~/Scripts/jgrowl.alerts.js",
                        "~/Scripts/skin2DRender.js",
                        "~/Scripts/MSP.js",
                        "~/Scripts/tree.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/Style.css",
                      "~/Content/jquery-ui*",
                      "~/Content/jquery.jgrowl.css"));
        }
    }
}
