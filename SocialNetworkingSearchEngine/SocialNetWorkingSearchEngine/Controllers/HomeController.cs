using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using BusinessRules;
using Core.Domain;
using SocialNetWorkingSearchEngine.Models;

namespace SocialNetWorkingSearchEngine.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "Welcome to Pichers & Pichers Social Networking Search Engine";

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

        public JsonResult SearchResults(string parameters, string searchEngines, string sentiment)
        {
            var result = new List<Post>();

            if (ModelState.IsValid)
            {                
                var searchEngineManager = new SearchEngineManager();
                result = searchEngineManager.Search(parameters, searchEngines.Split(',').ToList());
                
                var sentimentValuator = new SentimentValuator
                                            {
                                                NegativeWords = negativeWords,
                                                PositiveWords = positiveWords,
                                                IgnoreChars = ignoreList
                                            };

                var model = new SearchResultModel();

                foreach (var item in result)
                {
                    sentimentValuator.ProcessItem(item);

                    if (string.IsNullOrWhiteSpace(sentiment) || item.Sentiment.ToLower() == sentiment.ToLower())
                        model.Items.Add(item);
                }
                
                BuildSentimentBox(model, sentimentValuator);
                BuildEnginesBox(model);
            } 
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private void BuildEnginesBox(SearchResultModel model)
        {
            var builder = new SearchEngineBoxBuilder();
            builder.BuildBox(model);
        }

        public void BuildSentimentBox(SearchResultModel model, SentimentValuator sentimentValuator)
        {
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

        [HttpPost]
        public JsonResult SavePost(Post post)
        {
            var servicesManager = new ServicesManager();
            return Json(servicesManager.SavePost(post), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveProfile(Profile profile)
        {
            var servicesManager = new ServicesManager();
            return Json(servicesManager.SaveProfile(profile), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveTag(Tag tag)
        {
            var servicesManager = new ServicesManager();
            return Json(servicesManager.SaveTag(tag), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TagPost(Post post, string tagName)
        {
            var servicesManager = new ServicesManager();
            return Json(servicesManager.TagPost(post, tagName), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public void SendMail(string to, string subject, string body)
        {
            var address = ConfigurationManager.AppSettings["addressFrom"];
            var displayName = ConfigurationManager.AppSettings["displayName"]; 
            var userName = ConfigurationManager.AppSettings["userName"];
            var password = ConfigurationManager.AppSettings["password"];
            var port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            var host = ConfigurationManager.AppSettings["host"];

            var servicesManager = new ServicesManager();
            servicesManager.SendMail(to, address, displayName, subject, body, userName, password, port, host);
        }
    }
}
