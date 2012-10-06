using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MessageHandlers;
using Ninject;

namespace SampleUsageGlobal
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //GlobalConfiguration.Configuration.MessageHandlers.Add(new BasicAuthenticationHandler(new AlwaysTrueValidator()));

            //With Ninject..
            IKernel kernel = new StandardKernel();
            kernel.Bind<ICredentialsValidator>().To<AlwaysTrueValidator>();
            kernel.Bind<BasicAuthenticationHandler>().ToSelf();

            GlobalConfiguration.Configuration.MessageHandlers.Add(kernel.Get<BasicAuthenticationHandler>());
        }
    }
}