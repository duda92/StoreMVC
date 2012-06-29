using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;

namespace Store.WebUI.Infrastructure
{
    public class MultiCultureMvcRouteHandler : MvcRouteHandler
    {
        protected override IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            string culture = null;
            if (((Route)requestContext.RouteData.Route).Url == @"Account/LogOn/{app_culture}")
            {
                string requestUrl = ((requestContext.HttpContext).Request).Url.ToString();
                Regex r1 = new Regex(@"/[a-z]{2}$");
                Regex r2 = new Regex(@"/[a-z]{2}/$");
                Regex r3 = new Regex(@"/[a-z]{2}\?");

                if (r1.IsMatch(requestUrl))
                    culture = r1.Match(requestUrl).Value.Substring(1, 2);
                else if (r2.IsMatch(requestUrl))
                    culture = r2.Match(requestUrl).Value.Substring(1, 2);
                else if (r3.IsMatch(requestUrl))
                    culture = r3.Match(requestUrl).Value.Substring(1, 2);

                if (culture != null)
                    requestContext.RouteData.Values["app_culture"] = culture;
                else
                    culture = "en";
            }
            else if (requestContext.RouteData.Values["app_culture"] != null)
                culture = requestContext.RouteData.Values["app_culture"].ToString();
            else
                culture = "en";

            CultureInfo ci = null;
            try
            {
                ci = new CultureInfo(culture);
            }
            catch (CultureNotFoundException)
            {
                ci = new CultureInfo("en");
            }
            Thread.CurrentThread.CurrentUICulture = ci;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
            return base.GetHttpHandler(requestContext);
        }
    }
}