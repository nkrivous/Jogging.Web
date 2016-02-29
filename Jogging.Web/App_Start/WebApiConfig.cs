using System.Web.Http;
using AutoMapper;
using Jogging.Web.DAL;
using Jogging.Web.Infrastructure;
using Jogging.Web.Interfaces;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity;
using Newtonsoft.Json.Serialization;

namespace Jogging.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var container = new UnityContainer();

            var conf = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapperProfileConfiguration()); });
            container.RegisterInstance(conf.CreateMapper(), new ContainerControlledLifetimeManager());

            container.RegisterType<IUserRepository, UserRepository>(new PerRequestLifetimeManager());
            container.RegisterType<IJoggingRepository, JoggingRepository>(new PerRequestLifetimeManager());

            config.DependencyResolver = new UnityResolver(container);

            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "JoggingManageApi",
                routeTemplate: "api/{controller}/{userId}/jogging/{joggingId}",
                defaults: new { joggingId = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "UserManageApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
        }
    }
}
