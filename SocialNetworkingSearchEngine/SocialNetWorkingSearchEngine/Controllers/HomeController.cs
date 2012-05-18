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
    [Authorize]
    public class HomeController : Controller
    {
        public User Usuario
        {
            get { return (User)Session["Usuario"]; }
            set { Session["Usuario"] = value; }
        }

        public ActionResult Index()
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                ViewData["Message"] = "Bienvenido al Motor de Busqueda en Redes Sociales";
                return View(); 
            }

            return RedirectToAction("LogOn", "Account");
        }

        public ActionResult About()
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                return View(); 
            }

            return RedirectToAction("LogOn", "Account");
        }

        private List<string> negativeWords;
        private List<string> positiveWords;
        private List<string> ignoreList;

        public JsonResult SearchResults(string parameters, string searchEngines, string sentiment, string socialNetworking, string userName)
        {
            var result = new List<Post>();
            var model = new SearchResultModel();

            if (ModelState.IsValid)
            {
                var searchEngineManager = new SearchEngineManager();
                result = searchEngineManager.Search(parameters, searchEngines.Split(',').ToList());
                result = result.OrderByDescending(o => o.CreatedAt).ToList();
                if (result.Count > 0)
                {
                    GetAllWords();

                    var sentimentValuator = new SentimentValuator
                    {
                        NegativeWords = negativeWords,
                        PositiveWords = positiveWords,
                        IgnoreChars = ignoreList
                    };

                    foreach (var item in result)
                    {
                        sentimentValuator.ProcessItem(item);

                        if (string.IsNullOrEmpty(sentiment) && string.IsNullOrEmpty(socialNetworking) && string.IsNullOrEmpty(userName))
                            model.Items.Add(item);
                        
                        else if (ValidateFilters(sentiment, socialNetworking, userName, item))
                            model.Items.Add(item);
                    }

                    sentimentValuator.ResetCounters();
                    
                    foreach (var item in model.Items)
                    {
                        sentimentValuator.ProcessItem(item);
                    }

                    BuildSentimentBox(model, sentimentValuator);
                    BuildEnginesBox(model);
                    BuildTopUsersBox(model);
                }
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        private bool ValidateFilters(string sentiment, string socialNetworking, string userName, Post item)
        {
            bool functionValueReturn = (!string.IsNullOrEmpty(sentiment) && item.Sentiment.ToLower() == sentiment.ToLower()) ||
                                       (!string.IsNullOrEmpty(socialNetworking) && item.SocialNetworkName.ToLower() == socialNetworking.ToLower()) ||
                                       (!string.IsNullOrEmpty(userName) && item.UserName.ToLower() == userName.ToLower());

            return functionValueReturn;
        }

        private void GetAllWords()
        {
            ignoreList = new List<string>() { ".", "," };
            negativeWords = new List<string>();
            positiveWords = new List<string>();

            var serviceManager = new ServicesManager();

            negativeWords = serviceManager.GetAllWords().Where(x => x.Sentiment == "Negativo").Select(x => x.Name).ToList();
            positiveWords = serviceManager.GetAllWords().Where(x => x.Sentiment == "Positivo").Select(x => x.Name).ToList();
        }

        private void BuildTopUsersBox(SearchResultModel model)
        {
            var builder = new TopUsersBoxBuilder();
            builder.BuildBox(model);
        }

        private void BuildEnginesBox(SearchResultModel model)
        {
            var builder = new SearchEngineBoxBuilder();
            builder.BuildBox(model);
        }

        public void BuildSentimentBox(SearchResultModel model, SentimentValuator sentimentValuator)
        {
            var sentimentBox = new StatBox() { Title = "Sentimiento" };
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
            if (model.Items.Count > 0)
            {
                sentimentBox.StatItems[0].ValueText =
                    ((decimal)sentimentValuator.PositiveCount / model.Items.Count * 100).ToString("f") + "%";
                sentimentBox.StatItems[1].ValueText =
                    ((decimal)sentimentValuator.NegativeCount / model.Items.Count * 100).ToString("f") + "%";
                sentimentBox.StatItems[2].ValueText =
                    ((decimal)sentimentValuator.NeutralCount / model.Items.Count * 100).ToString("f") + "%";
                sentimentBox.StatItems[0].ValuePercent =
                    ((decimal)sentimentValuator.PositiveCount / model.Items.Count * 100);
                sentimentBox.StatItems[1].ValuePercent =
                    ((decimal)sentimentValuator.NegativeCount / model.Items.Count * 100);
                sentimentBox.StatItems[2].ValuePercent =
                    ((decimal)sentimentValuator.NeutralCount / model.Items.Count * 100);

            }
            else
            {
                sentimentBox.StatItems[0].ValueText = "0%";
                sentimentBox.StatItems[1].ValueText = "0%";
                sentimentBox.StatItems[2].ValueText = "0%";
                sentimentBox.StatItems[0].ValuePercent = 0;
                sentimentBox.StatItems[1].ValuePercent = 0;
                sentimentBox.StatItems[2].ValuePercent = 0;
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
            return Json(servicesManager.GetAllTags(), "application/json", JsonRequestBehavior.AllowGet);
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

        [HttpPost]
        public void SendPostToMail(string to, string subject, string content, string urlPost, string createdAt, string usrName,
            string urlUser, string urlImgNetwork, string urlImgProfile, string urlImgSentiment)
        {
            var address = ConfigurationManager.AppSettings["addressFrom"];
            var displayName = ConfigurationManager.AppSettings["displayName"];
            var userName = ConfigurationManager.AppSettings["userName"];
            var password = ConfigurationManager.AppSettings["password"];
            var port = Convert.ToInt32(ConfigurationManager.AppSettings["port"]);
            var host = ConfigurationManager.AppSettings["host"];

            var post = new Post
                           {
                               Content = content,
                               UrlPost = urlPost,
                               UserName = usrName,
                               UrlProfile = urlUser,
                               ProfileImage = urlImgProfile,
                               UrlImgNetwork = urlImgNetwork,
                               UrlImgSentiment = urlImgSentiment
                               
                           };
            var servicesManager = new ServicesManager();
            servicesManager.SendPostToMail(to, address, displayName, subject, userName, password, port, host, post);
        }
    }
}
