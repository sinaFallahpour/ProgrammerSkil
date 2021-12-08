using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProgrammerSkil.Models.ExtentionMethods.HandlerException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;

namespace ProgrammerSkil
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            config.Services.Replace(typeof(IExceptionHandler), new OopsExceptionHandler());

            // Web API configuration and services
            config.Filters.Add(new AuthorizeAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.Filters.Add(new AuthorizeAttribute());
          
            //cros  origin
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            //for Showing CamelCase propery in response of   api
            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.ContractResolver =
                                new CamelCasePropertyNamesContractResolver();

            //if a object have another obj an this obj   have that object is will bug but this code resolve that bug 
            //json.SerializerSettings.PreserveReferencesHandling =
            //                    PreserveReferencesHandling.All;

            //in Moshel moghe Include Zadan ra hal mikone
            json.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);
        }
    }
}
