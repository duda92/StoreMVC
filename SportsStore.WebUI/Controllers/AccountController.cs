using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.WebUI.Infrastructure;
using Store.WebUI.Models;
using System.Web.Routing;
using System.Web.Security;
using Store.Resourses.Views.Account;
using Store.Domain.Abstract;
using Store.Domain.Entities;

namespace Store.WebUI.Controllers
{
    public class AccountController : Controller
    {
        public IFormsAuthenticationService FormsService { get; set; }
        public IMembershipService MembershipService { get; set; }
        
        protected override void Initialize(RequestContext requestContext)
        {
            if (FormsService == null) { FormsService = new FormsAuthenticationService(); }
            if (MembershipService == null) { MembershipService = new AccountMembershipService(); }

            base.Initialize(requestContext);
        }

        public ActionResult LoginPanel()
        {
            return View();
        }

        public ActionResult LogOn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model)
        {  
            if (ModelState.IsValid)
            {
                if (MembershipService.ValidateUser(model.UserName, model.Password))
                {
                    FormsService.SignIn(model.UserName, model.RememberMe);
                    return Redirect(Url.Action("List", "Product"));
                }
                else
                    ModelState.AddModelError("", AccountStrings.The_user_name_or_password_provided_is_incorrect);
            }
            return View(model);
        }

        public ActionResult LogOut(string returnUrl)
        {
            FormsService.SignOut();
            return Redirect(returnUrl ?? Url.Action("List", "Product"));    
        }
    }
}
