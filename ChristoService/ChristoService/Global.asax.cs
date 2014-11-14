using Castle.Facilities.WcfIntegration;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ChristoDomain.Interfaces;
using ChristoDomain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace ChristoService
{
    public class Global : System.Web.HttpApplication
    {
        public static WindsorContainer Container { get; set; }

        protected void Application_Start(object sender, EventArgs e)
        {
            BuildContainer();

        }

        private void BuildContainer()
        {
            Container = new WindsorContainer();
            Container.Kernel.AddFacility<WcfFacility>();
            Container.Kernel.Register(Component.For<IMinimalSpanningTree>()
                                               .ImplementedBy<KruskalMSPService>()
                                               .Named("KruskalMSPService"));
            Container.Kernel.Register(Component.For<IChristoService>()
                                               .ImplementedBy<ChristoService>()
                                               .Named("ChristoService"));
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}