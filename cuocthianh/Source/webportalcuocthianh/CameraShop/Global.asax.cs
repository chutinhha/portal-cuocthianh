using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BaseEntity;
using System.Configuration;
using RegisterService;
using CoreData;
using System.Threading;
using Helper;
using ExternalDatabaseHelper;
namespace CameraShop
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        static string cnn = ConfigurationManager.ConnectionStrings["Camera"].ConnectionString;
        public static IList<EmailList> EmailQueue = new List<EmailList>();
        static Thread ThreadSendMail;
       
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public void Session_Start(object sender, EventArgs e)
        {
            //Session["ShoppingCart"] = new List<CoreData.ShoppingCart>();
        }
        public void Session_End(object sender, EventArgs e)
        {
            Session.Abandon();
            Session.Clear();
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteUrl.Route(routes);

        }


        void SendMail()
        {
           
            while (ThreadSendMail.IsAlive)
            {
                //var configmail = ExternalDatabaseHelper.ExtendModel.ExtendDBModel.Configurations.Where(c => c.Code.Equals("Email")).First();
                //var configpassword = ExternalDatabaseHelper.ExtendModel.ExtendDBModel.Configurations.Where(c => c.Code.Equals("EmailPassword")).First();
                try
                {
                  
                    foreach (var item in EmailQueue)
                    {
                        //EmailHelper.Send(item.Email, configmail.Value, configpassword.Value, "Camera Shop", item.TitleExt, item.BodyExt);
                        //EmailQueue.Remove(item);
                    }
                }
                catch { }
                Thread.Sleep(5000);
            }
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            BaseEntity.BaseEntity.StartDB(cnn);
            RegisterServices.RegisterDependencyResolver();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            ThreadSendMail = new Thread(new ThreadStart(() => SendMail()));
            ThreadSendMail.Start();

        }
    }
}