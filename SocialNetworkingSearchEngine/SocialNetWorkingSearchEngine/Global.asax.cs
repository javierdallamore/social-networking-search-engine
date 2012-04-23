using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BusinessRules;
using DataAccess;
using log4net.Config;

namespace SocialNetWorkingSearchEngine
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );


        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
            XmlConfigurator.Configure();
            SearchEngineManager.ConfigureAddins();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            if (!RequestMayNeedIterationWithPersistence(sender as HttpApplication)) return;
            NHSessionManager.Instance.BeginTransaction();
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            if (!RequestMayNeedIterationWithPersistence(sender as HttpApplication)) return;
            NHSessionManager.Instance.CommitTransaction();
        }

        private static readonly string[] NoPersistenceFileExtensions = new string[] { ".jpg", ".gif", ".png", ".css", ".js", ".swf", ".xap", ".doc", ".ico", ".htm" };
        
        private static bool RequestMayNeedIterationWithPersistence(HttpApplication application)
        {
            if (application == null)
            {
                return false;
            }
            HttpContext context = application.Context;
            if (context == null)
            {
                return false;
            }
            if (context.Request.FilePath == "/") return false;

            string extension = Path.GetExtension(context.Request.PhysicalPath);
            return ((extension != null) && (Array.IndexOf<string>(NoPersistenceFileExtensions, extension.ToLower()) < 0));
        }

    }
}