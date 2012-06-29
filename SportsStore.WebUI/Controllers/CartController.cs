using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Domain.Abstract;
using Store.Domain.Entities;
using Store.WebUI.Models;
using Store.Resourses.Views.Cart;

namespace Store.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IStoreRepository repository { get; set; }
        private IOrderProcessor orderProcessor { get; set; }

        public CartController(IStoreRepository repo, IOrderProcessor orderProcessor_)
        {
            repository = repo;
            orderProcessor = orderProcessor_;
        }
        
        public RedirectToRouteResult AddToCart(Cart cart, int productId)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
                cart.AddItem(product, 1);

            return RedirectToAction("Index", new { app_culture = ControllerContext.RequestContext.RouteData.Values["app_culture"] });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
                cart.RemoveLine(product);
            return RedirectToAction("Index", new { app_culture = ControllerContext.RequestContext.RouteData.Values["app_culture"] });
        }

        public ViewResult Index(Cart cart)
        {
            return View(new CartIndexViewModel { Cart = cart });
        }

        public ViewResult Summary(Cart cart)
        {
            return View(cart);
        }

        [Authorize(Roles="Customer")]
        public ActionResult Checkout()
        {
            ViewBag.Shippers = repository.Shippers;
            Customer customer = repository.GetCustomerByUserEmail(User.Identity.Name);
            return View(new Order { Customer = customer, CustomerID = customer.CustomerID}); 
        }

        [Authorize]
        [HttpPost]
        public ViewResult Checkout(Cart cart, Order order)
        {
            if (cart.Lines.Count() == 0)
                ModelState.AddModelError("", CartStrings.Your_cart_is_empty);
            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, order);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                ViewBag.Shippers = repository.Shippers;
                return View(order);
            }
        }
    }
}
