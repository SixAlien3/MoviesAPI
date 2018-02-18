using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;
using MoviesAPI.Filters;
using MoviesAPI.Infrastructure;
using Newtonsoft.Json.Serialization;

namespace MoviesAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.EnableCors();
            config.Filters.Add(new MoviesApiException());

            //The handler, like the logger, must be registered in the Web API configuration. 
            //Note that we can only have one Exception Handler per application.
            config.Services.Replace(typeof(IExceptionHandler), new MoviesApiExceptionHandler());
            config.Services.Replace(typeof(IExceptionLogger), new MoviesApiExceptionLogger());

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // will return json format instead of XML (especially in chrome browser)
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));


            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.UseDataContractJsonSerializer = false;
            config.Formatters.JsonFormatter.SerializerSettings.DateFormatString = "d MMMM, yyyy";
        }
    }
}
