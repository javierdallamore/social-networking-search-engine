using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessRules;
using SocialNetWorkingSearchEngine.Helpers;
using SocialNetWorkingSearchEngine.Models;

namespace SocialNetWorkingSearchEngine.Controllers
{
    [Authorize]
    public class PostManagerController : Controller
    {
        public ActionResult Index()
        {
            var servicesManager = new ServicesManager();
            var userHomeModel = new PostManagerModel();
            
            userHomeModel.NegativeWords = servicesManager.GetAllWords().Where(x=>x.Sentiment.ToLower() == "negativo");
            userHomeModel.PositiveWords = servicesManager.GetAllWords().Where(x => x.Sentiment.ToLower() == "positivo");

            var userAssignedPostNotProcessed =
                servicesManager.GetNotProcessedUserAssignedPost(UserHelper.GetCurrent()).ToList();
            var diferenceInPostToProcess = Params.MaxPostToProcessPerUser - userAssignedPostNotProcessed.Count();
            
            if (diferenceInPostToProcess > 0)
            {
                var newAssignedPost = servicesManager.AssignPostToUser(diferenceInPostToProcess, UserHelper.GetCurrent());
                userAssignedPostNotProcessed = userAssignedPostNotProcessed.Concat(newAssignedPost).ToList();
            }
            userHomeModel.Posts = userAssignedPostNotProcessed;


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
