using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessRules;
using SearchEnginesBase.Entities;
using SocialNetWorkingSearchEngine.Models;

namespace SocialNetWorkingSearchEngine.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Search()
        {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";

            return View();
        }

        public JsonResult Searcht(string parameters, string searchEngines)
        {
            var result = new List<SocialNetworkingSearchResult>();
            if (ModelState.IsValid)
            {
                var searchEngineManager = new SearchEngineManager();
                searchEngines = "SearchEngineMock";
                result = searchEngineManager.Search(parameters, searchEngines.Split(',').ToList());
            } 
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}
