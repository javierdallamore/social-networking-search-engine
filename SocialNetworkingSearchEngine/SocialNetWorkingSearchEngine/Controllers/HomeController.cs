using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using BusinessRules;
using Core.Domain;
using SearchEnginesBase.Entities;
using SearchEnginesBase.Utils;
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

        private List<string> negativeWords = new List<string>() { "Conchuda", "concha", "boludo", "puto" };
        private List<string> positiveWords = new List<string>() { "idolo", "exito", "amo", "genio" };
        private List<string> ignoreList = new List<string>() { ".", "," };

        public JsonResult SearchResults(string parameters, string searchEngines)
        {
            var result = new List<SocialNetworkingSearchResult>();
            if (ModelState.IsValid)
            {
                var searchEngineManager = new SearchEngineManager();
                result = searchEngineManager.Search(parameters, searchEngines.Split(',').ToList());
                var model = new SearchResultModel();
                foreach (var socialNetworkingSearchResult in result)
                {
                    model.Items.AddRange(socialNetworkingSearchResult.SocialNetworkingItems);
                }
                BuildSentimentBox(model);
                BuildEnginesBox(model);
            } 
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private void BuildEnginesBox(SearchResultModel model)
        {
            var builder = new SearchEngineBoxBuilder();
            builder.BuildBox(model);
        }

        public void BuildSentimentBox(SearchResultModel model)
        {
            var sentimentValuator = new SentimentValuator();
            sentimentValuator.NegativeWords = negativeWords;
            sentimentValuator.PositiveWords = positiveWords;
            sentimentValuator.IgnoreChars = ignoreList;
            sentimentValuator.ProcessItems(model.Items);
            var sentimentBox = new StatBox() {Title = "Sentimiento"};
            model.StatBoxs.Add(sentimentBox);
            sentimentBox.StatItems.Add(new StatItem()
                                           {
                                               Title = "Positivas",
                                               Link = "#",
                                               Value = sentimentValuator.PositiveCount
                                           });
            sentimentBox.StatItems.Add(new StatItem()
                                           {
                                               Title = "Negativas",
                                               Link = "#",
                                               Value = sentimentValuator.NegativeCount
                                           });
            sentimentBox.StatItems.Add(new StatItem()
                                           {
                                               Title = "Neutras",
                                               Link = "#",
                                               Value = sentimentValuator.NeutralCount
                                           });
            if (model.Items.Count >0)
            {
                sentimentBox.StatItems[0].ValueText =
                    ((decimal)sentimentValuator.PositiveCount / model.Items.Count * 100).ToString("f") + "%";
                sentimentBox.StatItems[1].ValueText =
                    ((decimal)sentimentValuator.NegativeCount / model.Items.Count * 100).ToString("f") + "%";
                sentimentBox.StatItems[2].ValueText =
                    ((decimal)sentimentValuator.NeutralCount / model.Items.Count * 100).ToString("f") + "%";
                
            }
            else
            {
                sentimentBox.StatItems[0].ValueText = "0%";
                sentimentBox.StatItems[1].ValueText = "0%";
                sentimentBox.StatItems[2].ValueText = "0%";
            }
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

        public void SendMail(string to, string subject, string body)
        {
            var address = ConfigurationManager.AppSettings["address"];
            var displayName = ConfigurationManager.AppSettings["displayName"]; 
            var userName = ConfigurationManager.AppSettings["userName"];
            var password = ConfigurationManager.AppSettings["password"];
            var port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            var host = ConfigurationManager.AppSettings["host"];

            Utils.SendMail(to, address, displayName, subject, body, userName, password, port, host);
        }
    }
}
