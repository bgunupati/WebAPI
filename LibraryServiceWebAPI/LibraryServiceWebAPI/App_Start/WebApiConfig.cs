using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LibraryServiceWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

           // config.Routes.MapHttpRoute(
           //     name: "DefaultApi",
           //     routeTemplate: "api/{controller}/{id}",
           //     defaults: new { id = RouteParameter.Optional }
           // );

           // config.Routes.MapHttpRoute(
           //     name: "getByTitle",
           //     routeTemplate: "api/{controller}/{title}",
           //     defaults: new { title = RouteParameter.Optional }
           //);
        }
    }
}
