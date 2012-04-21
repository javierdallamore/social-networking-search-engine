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
    public class WordsController : Controller
    {
		private readonly IWordRepository wordRepository;

        public WordsController()
        {
			this.wordRepository = new WordRepository();
        }

        public WordsController(IWordRepository wordRepository)
        {
			this.wordRepository = wordRepository;
        }

        //
        // GET: /Words/

        public ViewResult Index(string like = "")
        {
            return View(wordRepository.GetAll().Where(x=>x.Name.StartsWith(like)));
        }

        //
        // GET: /Words/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(wordRepository.GetById(id));
        }

        //
        // GET: /Words/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Words/Create

        [HttpPost]
        public ActionResult Create(Word word)
        {
            if (ModelState.IsValid) {
                wordRepository.Save(word);
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /Words/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
             return View(wordRepository.GetById(id));
        }

        //
        // POST: /Words/Edit/5

        [HttpPost]
        public ActionResult Edit(Word word)
        {
            if (ModelState.IsValid) {
                wordRepository.Save(word);
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /Words/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(wordRepository.GetById(id));
        }

        //
        // POST: /Words/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            wordRepository.Delete(wordRepository.GetById(id));

            return RedirectToAction("Index");
        }
    }
}

