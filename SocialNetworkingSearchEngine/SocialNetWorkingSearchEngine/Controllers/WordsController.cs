using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Domain;
using Core.RepositoryInterfaces;
using DataAccess.DAO;
using SocialNetWorkingSearchEngine.Models;

namespace SocialNetWorkingSearchEngine.Controllers
{
    [Authorize]
    public class WordsController : Controller
    {
        private readonly IWordRepository _wordRepository;

        public User Usuario
        {
            get { return (User)Session["Usuario"]; }
            set { Session["Usuario"] = value; }
        }

        public WordsController()
        {
            _wordRepository = new WordRepository();
        }

        public WordsController(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        //
        // GET: /Words/

        public ViewResult Index(int page = 1, string like = "")
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                IList<Word> list = _wordRepository.GetAll().Where(x => x.Name.StartsWith(like)).ToList();
                var model = new CrudViewModel<Word>(list, page, 20);

                model.Filter = like;

                return View(model); 
            }

            return View("LogOnUserControl");
        }

        //
        // GET: /Words/Details/5

        public ViewResult Details(Guid id)
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                return View(_wordRepository.GetById(id)); 
            }

            return View("LogOnUserControl");
        }

        //
        // GET: /Words/Create

        public ActionResult Create()
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                return View();
            }

            return RedirectToAction("LogOn", "Account");
        }

        //
        // POST: /Words/Create

        [HttpPost]
        public ActionResult Create(Word word)
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                if (ModelState.IsValid)
                {
                    _wordRepository.Save(word);
                    return RedirectToAction("Index");
                }

                return View();
            }

            return RedirectToAction("LogOn", "Account");
        }

        //
        // GET: /Words/Edit/5

        public ActionResult Edit(Guid id)
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                return View(_wordRepository.GetById(id));
            }

            return RedirectToAction("LogOn", "Account");
        }

        //
        // POST: /Words/Edit/5

        [HttpPost]
        public ActionResult Edit(Word word)
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                if (ModelState.IsValid)
                {
                    _wordRepository.Save(word);
                    return RedirectToAction("Index");
                }

                return View(); 
            }

            return RedirectToAction("LogOn", "Account");
        }

        //
        // GET: /Words/Delete/5

        public ActionResult Delete(Guid id)
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                return View(_wordRepository.GetById(id)); 
            }

            return RedirectToAction("LogOn", "Account");
        }

        //
        // POST: /Words/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                _wordRepository.Delete(_wordRepository.GetById(id));

                return RedirectToAction("Index"); 
            }

            return RedirectToAction("LogOn", "Account");
        }
    }
}

