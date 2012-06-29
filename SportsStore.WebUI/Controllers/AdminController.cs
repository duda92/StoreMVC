using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Domain.Abstract;
using Store.Domain.Entities;
using Store.WebUI.Models;
using Store.Resourses.Views.Admin;

namespace Store.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private IStoreRepository repository;

        #region Constructors

        public AdminController(IStoreRepository repo)
        {
            repository = repo;
        }

        #endregion

        #region Products

        public ViewResult Products()
        {
            return View(repository.Products);
        }

        [HttpPost]
        public ActionResult DeleteProduct(int productId)
        {
            Product prod = repository.Products.FirstOrDefault(p => p.ProductID == productId);
            if (prod != null)
                repository.DeleteProduct(prod);
            return RedirectToAction("Products", new { app_culture = Request.RequestContext.RouteData.Values["app_culture"] });
        }

        public ActionResult Home()
        {
            return View();
        }
        
        public ActionResult AddNewProduct()
        {
            ViewBag.Categories = repository.Categories;
            ViewBag.Suppliers = repository.Suppliers;
            return View("EditProduct", new Product());
        }

        [HttpPost]
        public ActionResult AddNewProduct(Product model)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(model);
                return RedirectToAction("Products", new { app_culture = Request.RequestContext.RouteData.Values["app_culture"] });
            }
            else
            {
                ViewBag.Categories = repository.Categories;
                ViewBag.Suppliers = repository.Suppliers;
                return View("EditProduct", model); 
            }
        }
        
        public ActionResult BeginEditProduct(int? productID)
        {
            if (!productID.HasValue)
                return View("Products", repository.Products);

            Product product = repository.GetProductById(productID.Value);
            if (product == null)
                return View("Products", repository.Products);
            return RedirectToAction("EditProduct", new { productID = productID, app_culture = Request.RequestContext.RouteData.Values["app_culture"] });
        }

        public ActionResult EditProduct(int? productID)
        {
            if (!productID.HasValue)
                return RedirectToAction("Products");
            Product model = repository.GetProductById(productID.Value);
            if (model == null)
                return RedirectToAction("Products");
            ViewBag.Categories = repository.Categories;
            ViewBag.Suppliers = repository.Suppliers;
            return View(model);
        }

        [HttpPost]
        public ActionResult EditProduct(Product model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HttpPostedFileBase file = Request.Files[0];
                    byte[] imageSize = new byte[file.ContentLength];
                    file.InputStream.Read(imageSize, 0, (int)file.ContentLength);
                    model.Image = imageSize;
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Image", AdminStrings.image_error);
                }
                repository.SaveProduct(model);
                return RedirectToAction("Products", new { app_culture = Request.RequestContext.RouteData.Values["app_culture"] });
            }
            else
            {
                
                ViewBag.Categories = repository.Categories;
                ViewBag.Suppliers = repository.Suppliers;
                return View(model);
            }
        }
        #endregion

        #region Suppliers

        public ActionResult Suppliers()
        {
            IEnumerable<Supplier> suppliers = repository.Suppliers;
            return View(suppliers);
        }

        public ActionResult AddSupplier()
        {
            return View("EditSupplier", new Supplier());
        }

        [HttpPost]
        public ActionResult AddSupplier(Supplier model)
        {
            if (ModelState.IsValid)
            {
                Supplier supplier = model;
                repository.SaveSupplier(supplier);
                return RedirectToAction("Suppliers", new { app_culture = Request.RequestContext.RouteData.Values["app_culture"] });
            }
            else
                return View("EditSupplier", model);
        }

        public ActionResult BeginEditSupplier(int? supplierID)
        {
            if (!supplierID.HasValue)
                return View("Suppliers", repository.Suppliers);

            Supplier supplier = repository.GetSupplierById(supplierID.Value);
            if (supplier == null)
                return View("Suppliers", repository.Suppliers);
            return RedirectToAction("EditSupplier", new { supplierID = supplierID , app_culture = Request.RequestContext.RouteData.Values["app_culture"] });
        }

        public ActionResult EditSupplier(int? supplierID)
        {
            Supplier model = repository.GetSupplierById(supplierID.Value);
            if (model == null)
                return RedirectToAction("Suppliers");
            return View(model);
        }

        [HttpPost]
        public ActionResult EditSupplier(Supplier model)
        {
            if (ModelState.IsValid)
            {
                Supplier supplier = model;
                repository.SaveSupplier(supplier);
                return RedirectToAction("Suppliers", new { app_culture = Request.RequestContext.RouteData.Values["app_culture"] });
            }
            else
                return View(model);
        }

        [HttpPost]
        public ActionResult DeleteSupplier(int supplierId)
        {
            Supplier supplier = repository.Suppliers.FirstOrDefault(s => s.SupplierID == supplierId);
            if (supplier != null)
            {
                repository.DeleteSupplier(supplier);
            }
            return RedirectToAction("Suppliers", new { app_culture = Request.RequestContext.RouteData.Values["app_culture"] });
        }

        #endregion

        #region Shippers

        public ActionResult Shippers()
        {
            IEnumerable<Shipper> shippers = repository.Shippers;
            return View(shippers);
        }

        public ActionResult AddShipper()
        {
            return View("EditShipper", new Shipper());
        }

        [HttpPost]
        public ActionResult AddShipper(Shipper model)
        {
            if (ModelState.IsValid)
            {
                repository.SaveShipper(model);
                return RedirectToAction("Home", new { app_culture = Request.RequestContext.RouteData.Values["app_culture"] });
            }
            return View("EditShipper", model);
        }

        public ActionResult BeginEditShipper(int? shipperID)
        {
            if (!shipperID.HasValue)
                return View("Shippers", repository.Shippers);

            Shipper Shipper = repository.GetShipperById(shipperID.Value);
            if (Shipper == null)
                return View("Shippers", repository.Shippers);
            return RedirectToAction("EditShipper", new { ShipperID = shipperID, app_culture = Request.RequestContext.RouteData.Values["app_culture"] });
        }

        public ActionResult EditShipper(int? shipperID)
        {
            Shipper model = repository.GetShipperById(shipperID.Value);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditShipper(Shipper model)
        {
            if (ModelState.IsValid)
            {
                repository.SaveShipper(model);
                return RedirectToAction("Shippers", new { app_culture = Request.RequestContext.RouteData.Values["app_culture"] });
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteShipper(int shipperId)
        {
            Shipper shipper = repository.Shippers.FirstOrDefault(s => s.ShipperID == shipperId);
            if (shipper != null)
                repository.DeleteShipper(shipper);
            return RedirectToAction("Shippers", new { app_culture = Request.RequestContext.RouteData.Values["app_culture"] });
        }

        #endregion

        #region Categories

        public ActionResult Categories()
        {
            IEnumerable<Category> categories = repository.Categories;
            return View(categories);
        }

        public ActionResult AddCategory()
        {
            return View("EditCategory", new Category());
        }

        [HttpPost]
        public ActionResult AddCategory(Category model)
        {
            if (ModelState.IsValid)
            { 
                repository.SaveCategory(model);
                return RedirectToAction("Home", new { app_culture = Request.RequestContext.RouteData.Values["app_culture"] });
            }
            return View("EditCategory", model);
        }

        public ActionResult BeginEditCategory(int? categoryID)
        {
            if (!categoryID.HasValue)
                return View("Categories", repository.Categories);

            Category category = repository.GetCategoryById(categoryID.Value);
            if (category == null)
                return View("Categories", repository.Categories);
            return RedirectToAction("EditCategory", new { CategoryID = categoryID, app_culture = Request.RequestContext.RouteData.Values["app_culture"] });
        }

        public ActionResult EditCategory(int? categoryID)
        {
            Category model = repository.GetCategoryById(categoryID.Value);
            if (model == null)
                return RedirectToAction("Categories");
            return View(model);
        }

        [HttpPost]
        public ActionResult EditCategory(Category model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    HttpPostedFileBase file = Request.Files[0];
                    byte[] imageSize = new byte[file.ContentLength];
                    file.InputStream.Read(imageSize, 0, (int)file.ContentLength);
                    model.Picture = imageSize;
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Picture", AdminStrings.image_error);
                }
                repository.SaveCategory(model);
                return RedirectToAction("Categories", new { app_culture = Request.RequestContext.RouteData.Values["app_culture"] });
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteCategory(int categoryId)
        {
            Category category = repository.Categories.FirstOrDefault(s => s.CategoryID == categoryId);
            if (category != null)
                repository.DeleteCategory(category);
            return RedirectToAction("Categories", new { app_culture = Request.RequestContext.RouteData.Values["app_culture"] });
        }

        #endregion

        #region Users

        public ActionResult Users()
        {
            IEnumerable<User> users = repository.Users;
            return View(users);
        }

        public ActionResult EditUser(int? userID)
        {
            if (!userID.HasValue)
                return RedirectToAction("Users", new { app_culture = Request.RequestContext.RouteData.Values["app_culture"] });
            User user = repository.Users.SingleOrDefault(u => u.UserID == userID);
            if (user == null)
                return RedirectToAction("Users", new { app_culture = Request.RequestContext.RouteData.Values["app_culture"] });
            ViewBag.Roles = repository.Roles.ToArray();
            List<bool> isInRoles = new List<bool>();
            foreach (var role in repository.Roles)
                isInRoles.Add(user.Roles.Contains(role));
            return View(new EditRolesViewModel() { IsInRoles = isInRoles});
        }

        [HttpPost]
        public ActionResult EditUser(EditRolesViewModel model)
        {
            User user = repository.Users.SingleOrDefault(u => u.UserID == model.UserId);
            user.Roles.Clear();
            for (int i = 0; i < model.IsInRoles.Count; i++)
                if (model.IsInRoles[i])
                    user.Roles.Add(repository.Roles.ToList()[i]);

            repository.UpdateUser(user);
            return RedirectToAction("Users", new { app_culture = Request.RequestContext.RouteData.Values["app_culture"] });
        }

        public ActionResult RemoveUser(int? userID)
        {
            if (!userID.HasValue)
                return RedirectToAction("Users", new { app_culture = Request.RequestContext.RouteData.Values["app_culture"] });
            User user = repository.Users.FirstOrDefault(u => u.UserID == userID.Value);
            if (user != null)
                repository.DeleteUser(user);
            return RedirectToAction("Users", new { app_culture = Request.RequestContext.RouteData.Values["app_culture"] });
        }

        #endregion
    }
}
