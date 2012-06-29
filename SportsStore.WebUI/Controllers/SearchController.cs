using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lucene.Net.Analysis;
using Lucene.Net.Store;
using Lucene.Net.Index;
using Lucene.Net.Search;
using System.Configuration;
using Lucene.Net.QueryParsers;
using Store.Domain.Entities;
using Store.Domain.Abstract;
using Ninject;

namespace Store.WebUI.Controllers
{
    public class SearchController : Controller
    {
        //public Directory directory { get; set; }
        [Inject]
        public Analyzer analyzer { get; set; }
        public IStoreRepository repository { get; set; }

        public SearchController(Analyzer _analyzer, IStoreRepository repo)
        {
            repository = repo;
            analyzer = _analyzer; 
        }

        public ActionResult SearchProduct(string searchText)
        {
            if(string.IsNullOrEmpty(searchText))
                return RedirectToAction("List", "Product", new { app_culture = RouteData.Values["app_culture"] });


            Directory directory = FSDirectory.Open(new System.IO.DirectoryInfo(ConfigurationManager.AppSettings["SearchDataPath"]));

            IndexReader indexReader = IndexReader.Open(directory, true);
            Searcher indexSearch = new IndexSearcher(indexReader);
            
            MultiFieldQueryParser parser = new MultiFieldQueryParser(Lucene.Net.Util.Version.LUCENE_29, 
                                                                new string[]
                                                                {
                                                                    "ProductName",
                                                                    "Description"
                                                                },
                                                                analyzer);
            Query query = null;
            try
            {
                query = parser.Parse(searchText);
            }
            catch (ParseException)
            {
                return RedirectToAction("List", "Product", new { app_culture = RouteData.Values["app_culture"] });
            }

            TopDocs resultDocs = indexSearch.Search(query, indexReader.MaxDoc()); 
            var hits = resultDocs.scoreDocs;

            List<Product> results = new List<Product>();
            foreach (var hit in hits)
            {
                var documentFromSearcher = indexSearch.Doc(hit.doc);
                int id = int.Parse(documentFromSearcher.GetField("ProductID").StringValue());
                results.Add(repository.GetProductById(id));
            }
            indexSearch.Close();
            directory.Close();
            RouteData.Values["Products"] = results;
            TempData["Products"] = results;
            return RedirectToAction("SearchList", "Product", new { app_culture = RouteData.Values["app_culture"] });
        }
    }
}
