using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BusinessRules;
using Core.Domain;
using SocialNetWorkingSearchEngine.Helpers;
using SocialNetWorkingSearchEngine.Models;

namespace SocialNetWorkingSearchEngine.Controllers
{
    [Authorize]
    public class PostManagerController : Controller
    {
        public ActionResult Index()
        {
            var searchEngineManager = new SearchEngineManager();
            var userHomeModel = new UserHomeModel();
            userHomeModel.Posts = searchEngineManager.GetUserAssignedPost(UserHelper.GetCurrent());

            return View(userHomeModel);
        }

        [HttpPost]
        public JsonResult UpdatePost(string idPost, int rating, string sentiment, List<string> tags)
        {
            var servicesManager = new ServicesManager();
            var currentUser = UserHelper.GetCurrent();
            servicesManager.UpdatePost(idPost, rating, sentiment, tags, currentUser);

            return Json("ok", JsonRequestBehavior.AllowGet);
        }
    }
}
