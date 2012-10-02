using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BusinessRules;
using SocialNetWorkingSearchEngine.Helpers;
using SocialNetWorkingSearchEngine.Models;

namespace SocialNetWorkingSearchEngine.Controllers
{
    using Core.Domain;

    [Authorize]
    public class PostManagerController : Controller
    {
        private readonly ServicesManager servicesManager = new ServicesManager();

        public ActionResult Index()
        {
            var userHomeModel = new PostManagerModel();

            userHomeModel.NegativeWords = servicesManager.GetAllWords().Where(x => x.Sentiment.ToLower() == "negativo");
            userHomeModel.PositiveWords = servicesManager.GetAllWords().Where(x => x.Sentiment.ToLower() == "positivo");

            var userAssignedPostNotProcessed = GetAndAssignPosts();
            userHomeModel.Posts = userAssignedPostNotProcessed;


            return View(userHomeModel);
        }

        public JsonResult GetPosts()
        {
            return Json(this.GetAndAssignPosts(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UpdatePost(string idPost, int rating, string sentiment, List<string> tags)
        {
            var currentUser = UserHelper.GetCurrent();
            servicesManager.UpdatePost(idPost, rating, sentiment, tags, currentUser);

            return Json("ok", JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<Post> GetAndAssignPosts()
        {
            var userAssignedPostNotProcessed = servicesManager.GetNotProcessedUserAssignedPost(UserHelper.GetCurrent()).ToList();
            var diferenceInPostToProcess = Params.MaxPostToProcessPerUser - userAssignedPostNotProcessed.Count();

            if (diferenceInPostToProcess > 0)
            {
                var newAssignedPost = servicesManager.AssignPostToUser(diferenceInPostToProcess, UserHelper.GetCurrent());
                userAssignedPostNotProcessed = userAssignedPostNotProcessed.Concat(newAssignedPost).ToList();
            }

            return userAssignedPostNotProcessed;
        }
    }
}
