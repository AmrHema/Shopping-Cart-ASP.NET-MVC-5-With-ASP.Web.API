using API_Project.Repository;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApiContrib.Formatting.Jsonp;

namespace API_Project
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            // Web API configuration and services
            config.DependencyResolver = new NinjectDependencyResolver();
            // Web API routes
            config.MapHttpAttributeRoutes();

            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Change default Web API 2 from XML to JSON formatter 
            // First Way
            //config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));


            //Second Way
            //config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            // third Way
            config.Formatters.Remove(config.Formatters.XmlFormatter);




        }
    }
}
