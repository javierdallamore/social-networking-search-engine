using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialMonitor.Controllers
{
    using System.Collections.ObjectModel;
    using System.Web.Helpers;

    using BusinessRules;

    using Core.Domain;

    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Welcome to ASP.NET MVC!";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ProcesarPosts(ProcesarPostModel procesarPostModel)
        {
            return Json(new { success = 0 });
        }

        public JsonResult ProcesarPostsModelSample1()
        {
            return
                Json(
                    new ProcesarPostModel
                        {
                            PostIds = new int[] { 1, 2, 3 },
                            Tags =
                                new Collection<TagViewModel>()
                                    {
                                        new TagViewModel { Id = 1, Name = "Insulto" },
                                        new TagViewModel { Name = "New tag" }
                                    },
                            Sentiment = SentimentEnum.Positivo
                        },
                    JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProcesarPostsReturnedJsonOnSuccess()
        {
            return Json(new { success = 0 }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ProcesarPostsReturnedJsonOnFailure()
        {
            return Json(new { success = 1, message = "Algo esta mal" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetTags()
        {
            return
                Json(
                    new Collection<TagViewModel>
                        {
                            new TagViewModel() { Id = 1, Name = "Tag 1" },
                            new TagViewModel() { Id = 2, Name = "Tag 2" },
                            new TagViewModel() { Id = 3, Name = "Tag 3" },
                            new TagViewModel() { Id = 4, Name = "Tag 4" }
                        },
                    JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPostsModelSample1()
        {
            return
                Json(
                    new PostsFilterModel
                        {
                            Desde = DateTime.Today.AddDays(-10).ToString("dd/MM/yyyy"),
                            Hasta = DateTime.Today.ToString("dd/MM/yyyy"),
                            Action = "Process",
                            Engines = new int[] { 1, 2, 3 },
                            NumPage = 1,
                            PageSize = 10
                        },
                    JsonRequestBehavior.AllowGet);
        }
    }

    public class PostsFilterModel
    {
        public string Desde { get; set; }

        public string Hasta { get; set; }

        public IEnumerable<int> Tags { get; set; }

        public IEnumerable<int> Engines { get; set; }

        public int? Folder { get; set; }

        public bool FolderRecursive { get; set; }

        public IEnumerable<int> Sentiments { get; set; }

        public int? State { get; set; }

        public int NumPage { get; set; }

        public int PageSize { get; set; }

        public string Action { get; set; }

        public int? Query { get; set; }
    }

    public class TagViewModel
    {
        public int? Id { get; set; }
        
        public string Name { get; set; }
    } 

    public class ProcesarPostModel
    {
        public IEnumerable<TagViewModel> Tags { get; set; }
        
        public IEnumerable<int> PostIds { get; set; }
        
        public SentimentEnum? Sentiment { get; set; }
    }

    public enum SentimentEnum
    {
        Positivo = 1,

        Negativo = 2,

        Neutro = 0
    }
}
