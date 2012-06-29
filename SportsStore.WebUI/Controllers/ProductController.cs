using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Domain.Abstract;
using Store.Domain.Entities;
using Store.WebUI.Models;
using Store.WebUI.Infrastructure;

namespace Store.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IStoreRepository repository;

        public int PageSize = 4;
        public string currentCategory;
        
        public IEnumerable<Product> productsForDisplay;
        public PagingInfo pagingInfo;
        public IEnumerable<Product> productsOfCategory;
        public ILogger logger;

        public ProductController(IStoreRepository repo, ILogger log)
        {
            repository = repo;
            logger = log;
        }

        public ViewResult List(string category, int page = 1)
        {
            productsOfCategory = repository.Products.Where(p => category == null || p.Category.CategoryName == category);
            productsForDisplay = productsOfCategory.OrderBy(x => x.ProductID).Skip((page - 1) * PageSize).Take(PageSize);

            pagingInfo = new PagingInfo { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = productsOfCategory.Count() };

            logger.Info("List");

            ProductListViewModel viewModel = new ProductListViewModel
            {
                Products = productsForDisplay,
                PagingInfo = pagingInfo
            };
            currentCategory = category;
            viewModel.CurrentCategory = category;
            return View(viewModel);
        }

        public ActionResult SearchList(string category,  int page = 1)
        {
            IEnumerable<Product> products = (IEnumerable<Product>)TempData["Products"];
            if (products == null)
                return RedirectToAction("List");
            pagingInfo = new PagingInfo { CurrentPage = page, ItemsPerPage = PageSize, TotalItems = products.Count() };

            logger.Info("List");

            ProductListViewModel viewModel = new ProductListViewModel
            {
                Products = products,
                PagingInfo = pagingInfo
            };
            currentCategory = category;
            viewModel.CurrentCategory = category;
            return View("List", viewModel);
        }

    }
}
