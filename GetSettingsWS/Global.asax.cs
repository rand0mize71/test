using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace GetSettingsWS
{
    public class WebApiApplication : System.Web.HttpApplication
    {

        public static void RegisterRoutes(RouteCollection routes)
        {
            //RouteTable.Routes.MapRoute(
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "settings", action = "getsetting" });

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}
