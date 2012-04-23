using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BusinessRules;
using Core.Domain;
using SocialNetWorkingSearchEngine.Models;

namespace SocialNetWorkingSearchEngine.Controllers
{
    public class HomeUserController : Controller
    {
        public ActionResult Index()
        {
            var searchEngineManager = new SearchEngineManager();
            var userHomeModel = new UserHomeModel();
            userHomeModel.Posts = searchEngineManager.GetUserAssignedPost(new User { Id = 1, Login = "diego" });

            return View(userHomeModel);
        }

        [HttpPost]
        public JsonResult UpdatePost(string idPost, int rating, string sentiment, List<string> tags)
        {
            var servicesManager = new ServicesManager();
            servicesManager.UpdatePost(idPost, rating, sentiment, tags);

            return Json("ok", JsonRequestBehavior.AllowGet);
        }
    }
}
