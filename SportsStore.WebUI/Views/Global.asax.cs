using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Store.WebUI.Infrastructure;
using Store.Domain.Entities;
using Store.WebUI.Binders;
using System.Web.Security;
using Ninject;
using System.Globalization;
using System.Threading;
using Store.Domain.Abstract;
using Store.Domain.Concrete;
using Lucene.Net.Store;
using Lucene.Net.Analysis;
using System.IO;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using System.Configuration;
using Store.WebUI.Controllers;

namespace Store.WebUI
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private IKernel _kernel = new StandardKernel();

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null, "{app_culture}", new { controller = "Product", action = "List", category = (string)null, app_culture = "en" }, new { app_culture = @"\w{2}" });//, page = 1 });

            routes.MapRoute(null, "Page{page}/{app_culture}", new { controller = "Product", action = "List", category = (string)null, app_culture = "en" }, new { app_culture = @"\w{2}" });

            routes.MapRoute(null, "{category}/{app_culture}", new { controller = "Product", action = "List", page = 1, app_culture = "en" }, new { app_culture = @"\w{2}" });

            routes.MapRoute(null, "{category}/Page{page}/{app_culture}", new { controller = "Product", action = "List", app_culture = "en", }, new { app_culture = @"\w{2}" });

            routes.MapRoute(null, "Admin/EditProduct/{productID}/{app_culture}", new { controller = "Admin", action = "EditProduct", app_culture = "en" }, new { app_culture = @"\w{2}" });

            routes.MapRoute(null, "Admin/EditSupplier/{supplierID}/{app_culture}", new { controller = "Admin", action = "EditSupplier", app_culture = "en" }, new { app_culture = @"\w{2}" });

            routes.MapRoute(null, "Admin/EditCategory/{categoryID}/{app_culture}", new { controller = "Admin", action = "EditCategory", app_culture = "en" }, new { app_culture = @"\w{2}" });

            routes.MapRoute(null, "Admin/EditShipper/{shipperID}/{app_culture}", new { controller = "Admin", action = "EditShipper", app_culture = "en" }, new { app_culture = @"\w{2}" });

            routes.MapRoute(null, "Admin/EditUser/{userID}/{app_culture}", new { controller = "Admin", action = "EditUser", app_culture = "en" }, new { app_culture = @"\w{2}" });

            routes.MapRoute("Account", "Account/LogOn/{app_culture}", new { controller = "Account", action = "LogOn", app_culture = "en" }, new { app_culture = @"\w{2}" });

            routes.MapRoute(null, "{controller}/{action}/{app_culture}", new { app_culture = "en" }, new { app_culture = @"\w{2}" });

            routes.MapRoute("NotFound", "{*url}", new { controller = "Error", action = "HttpError404" });

            foreach (Route r in routes)
                r.RouteHandler = new MultiCultureMvcRouteHandler();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(_kernel));

            _kernel.Bind<ILogger>().To<NLogLogger>().InSingletonScope();

            _kernel.Bind<Analyzer>().To<StandardAnalyzer>().WithConstructorArgument("matchVersion", Lucene.Net.Util.Version.LUCENE_29);
            Lucene.Net.Store.Directory directory = FSDirectory.Open(new DirectoryInfo(ConfigurationManager.AppSettings["SearchDataPath"]));

            _kernel.Bind<IStoreRepository>().To<EFStoreRepository>().InSingletonScope();
            _kernel.Inject(Membership.Provider);
            _kernel.Inject(Roles.Provider);
            //UpdateProductsDocument(directory);
        }
 
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            Response.Clear();
            HttpException httpException = exception as HttpException;

            RouteData routeData = new RouteData();
            routeData.Values.Add("controller", "Error");

            if (httpException == null)
                routeData.Values.Add("action", "Index");
            else
            {
                switch (httpException.GetHttpCode())
                {
                    case 404:
                        routeData.Values.Add("action", "HttpError404");
                        break;
                    case 500:
                        routeData.Values.Add("action", "HttpError500");
                        break;
                    default:
                        routeData.Values.Add("action", "General");
                        break;
                }
            }

            routeData.Values.Add("error", exception);
            Server.ClearError();
            Response.TrySkipIisCustomErrors = true;

            IController errorController = new ErrorController();
            errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        }

        private void UpdateProductsDocument(Lucene.Net.Store.Directory directory)
        {
            using (var writer = new IndexWriter(directory, (Analyzer)_kernel.Get(typeof(Analyzer)), true, IndexWriter.MaxFieldLength.LIMITED))
            {
                IQueryable<Product> products = ((IStoreRepository)_kernel.Get(typeof(IStoreRepository))).Products;

                foreach (var product in products)
                    writer.AddDocument(product.GetDocument());

                writer.Optimize();
                writer.Close();
            }
        }

    }
}