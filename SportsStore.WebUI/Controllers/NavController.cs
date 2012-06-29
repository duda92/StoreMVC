using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Domain.Abstract;

namespace Store.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IStoreRepository repository;

        public NavController(IStoreRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string category = null)
        {
            ViewBag.SelectedCategory = category;
            IEnumerable<string> categories = repository.Categories.Select(x => x.CategoryName).Distinct().OrderBy(x => x);
            return PartialView(categories);
        }

    }
}
