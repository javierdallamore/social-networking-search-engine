using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessRules;
using Core.Domain;
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
        
        public ActionResult Explore()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public JsonResult SearchResults(string parameters, string searchEngines)
        {
            var result = new List<SocialNetworkingSearchResult>();
            if (ModelState.IsValid)
            {
                var searchEngineManager = new SearchEngineManager();
                result = searchEngineManager.Search(parameters, searchEngines.Split(',').ToList());
            } 
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllEntities(string parameters, string searchEngines)
        {
            var servicesManager = new ServicesManager();
            return Json(servicesManager.GetAllEntities(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllEntitiesByProfile(string profileId)
        {
            var servicesManager = new ServicesManager();
            return Json(servicesManager.GetAllEntitiesByProfile(Guid.Parse(profileId)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllEntitiesByTag(string tagName)
        {
            var servicesManager = new ServicesManager();
            return Json(servicesManager.GetAllEntitiesByTag(tagName), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllProfiles()
        {
            var servicesManager = new ServicesManager();
            return Json(servicesManager.GetAllProfiles(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllTags()
        {
            var servicesManager = new ServicesManager();
            return Json(servicesManager.GetAllTags(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveEntity(Entity entity)
        {
            var servicesManager = new ServicesManager();
            return Json(servicesManager.SaveEntity(entity), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveEntity(Profile profile)
        {
            var servicesManager = new ServicesManager();
            return Json(servicesManager.SaveProfile(profile), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveEntity(Tag tag)
        {
            var servicesManager = new ServicesManager();
            return Json(servicesManager.SaveTag(tag), JsonRequestBehavior.AllowGet);
        }

        public JsonResult TagEntity(Entity entity, string tagName)
        {
            var servicesManager = new ServicesManager();
            return Json(servicesManager.TagEntity(entity, tagName), JsonRequestBehavior.AllowGet);
        }
    }
}
