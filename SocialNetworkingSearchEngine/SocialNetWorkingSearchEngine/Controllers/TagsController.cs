using System.Web.Mvc;
using Core.Domain;
using Core.RepositoryInterfaces;
using DataAccess.DAO;

namespace SocialNetWorkingSearchEngine.Controllers
{
    [Authorize]
    public class TagsController : Controller
    {
		private readonly ITagRepository tagRepository;
        public User Usuario
        {
            get { return (User)Session["Usuario"]; }
            set { Session["Usuario"] = value; }
        }

        public TagsController()
        {
			this.tagRepository =new TagRepository();
        }

        public TagsController(ITagRepository tagRepository)
        {
			this.tagRepository = tagRepository;
        }

        //
        // GET: /Tags/

        public ViewResult Index()
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                return View(tagRepository.GetAll()); 
            }

            return View("LogOnUserControl");
        }

        //
        // GET: /Tags/Details/5

        public ViewResult Details(System.Guid id)
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                return View(tagRepository.GetById(id)); 
            }

            return View("LogOnUserControl");
        }

        //
        // GET: /Tags/Create

        public ActionResult Create()
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                return View();                
            }

            return RedirectToAction("LogOn", "Account");
        } 

        //
        // POST: /Tags/Create

        [HttpPost]
        public ActionResult Create(Tag tag)
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                if (ModelState.IsValid)
                {
                    tagRepository.Save(tag);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }

            return RedirectToAction("LogOn", "Account");
        }
        
        //
        // GET: /Tags/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                return View(tagRepository.GetById(id));   
            }

            return RedirectToAction("LogOn", "Account");
        }

        //
        // POST: /Tags/Edit/5

        [HttpPost]
        public ActionResult Edit(Tag tag)
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                if (ModelState.IsValid)
                {
                    tagRepository.SaveOrUpdate(tag);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }                
            }

            return RedirectToAction("LogOn", "Account");
        }

        //
        // GET: /Tags/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                return View(tagRepository.GetById(id));    
            }

            return RedirectToAction("LogOn", "Account");
        }

        //
        // POST: /Tags/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                tagRepository.Delete(tagRepository.GetById(id));
                return RedirectToAction("Index");                
            }

            return RedirectToAction("LogOn", "Account");
        }
    }
}

