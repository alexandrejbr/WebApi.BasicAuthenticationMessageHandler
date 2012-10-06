using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using MessageHandlers;
using Ninject;

namespace SampleUsagePerRoute
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            IKernel kernel = new StandardKernel();
            kernel.Bind<ICredentialsValidator>().To<AlwaysTrueValidator>();
            kernel.Bind<BasicAuthenticationHandler>().ToSelf();

            GlobalConfiguration.Configuration.MessageHandlers.Add(kernel.Get<BasicAuthenticationHandler>());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints:null,
                handler: kernel.Get<BasicAuthenticationHandler>()
            );
        }
    }
}
