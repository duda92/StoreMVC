using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Domain.Entities;
using Store.Domain.Abstract;
using Store.Resourses.Registration;
using Store.WebUI.Models;

namespace Store.WebUI.Controllers
{
    public class RegistrationController : Controller
    {
        public IStoreRepository repository { get; set; }

        public RegistrationController(IStoreRepository repo)
        {
            repository = repo;
        }
        
        public ActionResult UserRegistration()
        {
            if (Session["UserRegistrationViewModel"] != null)
                return View((UserRegistrationViewModel)Session["UserRegistrationViewModel"]);
            return View();
        }

        [HttpPost]
        public ActionResult UserRegistration(UserRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (repository.Users.SingleOrDefault(u => u.Email == model.Email) == null)
                {
                    Session["UserRegistrationViewModel"] = model;
                    return RedirectToAction("CustomerMain", new { app_culture = RouteData.Values["app_culture"] });
                }
                else
                    ModelState.AddModelError("Email", RegistrationStrings.user_emai_already_exists);
            }
            return View(model);
        }

        public ActionResult CustomerMain()
        {
            if (Session["CustomerMainViewModel"] != null)
                return View(Session["CustomerMainViewModel"]);
            return View(new CustomerMainViewModel());
        }

        [HttpPost]
        public ActionResult CustomerMain(CustomerMainViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (repository.Customers.SingleOrDefault(c => c.CustomerID == model.CustomerID) == null)
                {
                    Session["CustomerMainViewModel"] = model;
                    return RedirectToAction("CustomerDetail", new { app_culture = RouteData.Values["app_culture"] });
                }
                else
                    ModelState.AddModelError("CustomerID", RegistrationStrings.customer_id_already_exists);
            }
            return View(model);
        }

        public ActionResult CustomerDetail()
        {
            if (Session["UserRegistrationViewModel"] == null)
                return RedirectToAction("UserRegistration", new { app_culture = RouteData.Values["app_culture"] });
            if (Session["CustomerMainViewModel"] == null)
                return RedirectToAction("UserRegistration", new { app_culture = RouteData.Values["app_culture"] });
            if (Session["CustomerDetailViewModel"] != null)
                return View(Session["CustomerDetailViewModel"]);
            return View();
        }

        [HttpPost]
        public ActionResult CustomerDetail(CustomerDetailViewModel model)
        {
            if (ModelState.IsValid)
            {
                Session["CustomerDetailViewModel"] = model;
                return RedirectToAction("CompleteRegistration", new { app_culture = RouteData.Values["app_culture"] });
            }
            else
                return View(model);
        }

        public ActionResult CompleteRegistration()
        {
            //check user registration
            if (Session["UserRegistrationViewModel"] == null)
                return RedirectToAction("UserRegistration", new { app_culture = RouteData.Values["app_culture"] });
            UserRegistrationViewModel userRegistrationModel = (UserRegistrationViewModel)Session["UserRegistrationViewModel"];
            if (!userRegistrationModel.Password.Equals(userRegistrationModel.ConfirmPassword))
                return RedirectToAction("UserRegistration", new { app_culture = RouteData.Values["app_culture"] });
            if (repository.Users.SingleOrDefault(u => u.Email == userRegistrationModel.Email) != null)
                return RedirectToAction("UserRegistration", new { app_culture = RouteData.Values["app_culture"] });

            //check customerMainRegistration
            if (Session["CustomerMainViewModel"] == null)
                return RedirectToAction("CustomerMain", new { app_culture = RouteData.Values["app_culture"] });
            CustomerMainViewModel customerMainModel = (CustomerMainViewModel)Session["CustomerMainViewModel"];
            if (repository.Customers.SingleOrDefault(c => c.CustomerID == customerMainModel.CustomerID) != null)
                return RedirectToAction("CustomerMain", new { app_culture = RouteData.Values["app_culture"] });

            //check customerDetailRegistration
            if (Session["CustomerDetailViewModel"] == null)
                return RedirectToAction("CustomerDetail", new { app_culture = RouteData.Values["app_culture"] });
            CustomerDetailViewModel customerDetailModel = (CustomerDetailViewModel)Session["CustomerDetailViewModel"];
            
            ViewBag.Email = userRegistrationModel.Email;
            ViewBag.CustomerID = customerMainModel.CustomerID;
            ViewBag.CompanyName = customerMainModel.CompanyName;
            ViewBag.ContactTitle = customerMainModel.ContactTitle;
            ViewBag.Address = customerDetailModel.Address;
            ViewBag.City = customerDetailModel.City;
            ViewBag.Region = customerDetailModel.Region;
            ViewBag.PostalCode = customerDetailModel.PostalCode;
            ViewBag.Country = customerDetailModel.Country;
            ViewBag.Phone = customerDetailModel.Phone;
            ViewBag.Fax = customerDetailModel.Fax;
            return View();
        }

        [HttpPost]
        public ActionResult CompleteRegistration(bool accepted = true)
        {
            //check user registration
            if (Session["UserRegistrationViewModel"] == null)
                return RedirectToAction("UserRegistration", new { app_culture = RouteData.Values["app_culture"] });
            UserRegistrationViewModel userRegistrationModel = (UserRegistrationViewModel)Session["UserRegistrationViewModel"];
            if (!userRegistrationModel.Password.Equals(userRegistrationModel.ConfirmPassword))
                return RedirectToAction("UserRegistration", new { app_culture = RouteData.Values["app_culture"] });
            if (repository.Users.SingleOrDefault(u => u.Email == userRegistrationModel.Email) != null)
                return RedirectToAction("UserRegistration", new { app_culture = RouteData.Values["app_culture"] });
            
            //check customerMainRegistration
            if (Session["CustomerMainViewModel"] == null)
                return RedirectToAction("CustomerMain", new { app_culture = RouteData.Values["app_culture"] });
            CustomerMainViewModel customerMainModel = (CustomerMainViewModel)Session["CustomerMainViewModel"];
            if (repository.Customers.SingleOrDefault(c => c.CustomerID == customerMainModel.CustomerID) != null)
                return RedirectToAction("CustomerMain", new { app_culture = RouteData.Values["app_culture"] });

            //check customerDetailRegistration
            if (Session["CustomerDetailViewModel"] == null)
                return RedirectToAction("CustomerDetail", new { app_culture = RouteData.Values["app_culture"] });
            CustomerDetailViewModel customerDetailModel = (CustomerDetailViewModel)Session["CustomerDetailViewModel"];
            
            if (accepted)
            {
                User user = new User { Email = userRegistrationModel.Email, Password = userRegistrationModel.Password };
                Customer customer = new Customer { CustomerID = customerMainModel.CustomerID, CompanyName = customerMainModel.CompanyName, ContactName = customerMainModel.ContactName, ContactTitle = customerMainModel.ContactTitle, Address = customerDetailModel.Address, City = customerDetailModel.City, Country = customerDetailModel.Country, Fax = customerDetailModel.Fax, Phone = customerDetailModel.Phone, PostalCode = customerDetailModel.PostalCode, Region = customerDetailModel.Region };
                user.Customer = customer;
                try
                {
                    repository.RegisterUser(user);
                }
                catch (Exception e)
                {
                    return RedirectToAction("UserRegistration", new { app_culture = RouteData.Values["app_culture"] });
                }
                return RedirectToAction("RegistrationCompleted", new { app_culture = RouteData.Values["app_culture"] });
            }
            else
            {
                return RedirectToAction("UserRegistration", new { app_culture = RouteData.Values["app_culture"] });
            }
        }

        public ActionResult RegistrationCompleted()
        {
            Session["UserRegistrationViewModel"] = null;
            Session["CustomerMainViewModel"] = null;
            Session["CustomerDetailViewModel"] = null;
            return View();
        }

    }
}
