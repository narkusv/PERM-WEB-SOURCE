using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PERMWebSolution
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "PERMApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //This package allows the ASP.NET Web API framework to trace to System.Diagnostics.Trace.
            // more at http://go.microsoft.com/fwlink/?LinkId=269874
            config.EnableSystemDiagnosticsTracing();
        }
    }
}
