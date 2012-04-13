using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Domain;
using Core.RepositoryInterfaces;
using DataAccess.DAO;

namespace SocialNetWorkingSearchEngine.Controllers
{   
    public class TagsController : Controller
    {
		private readonly ITagRepository tagRepository;

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
            return View(tagRepository.GetAll());
        }

        //
        // GET: /Tags/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(tagRepository.GetById(id));
        }

        //
        // GET: /Tags/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Tags/Create

        [HttpPost]
        public ActionResult Create(Tag tag)
        {
            if (ModelState.IsValid) {
                tagRepository.Save(tag);
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Tags/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
             return View(tagRepository.GetById(id));
        }

        //
        // POST: /Tags/Edit/5

        [HttpPost]
        public ActionResult Edit(Tag tag)
        {
            if (ModelState.IsValid) {
				tagRepository.SaveOrUpdate(tag);
                
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Tags/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(tagRepository.GetById(id));
        }

        //
        // POST: /Tags/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            tagRepository.Delete(tagRepository.GetById(id));

            return RedirectToAction("Index");
        }
    }
}

