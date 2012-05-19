using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Domain;
using Core.RepositoryInterfaces;
using DataAccess.DAO;
using SocialNetWorkingSearchEngine.Helpers;
using SocialNetWorkingSearchEngine.Models;

namespace SocialNetWorkingSearchEngine.Controllers
{
    [Authorize]
    public class WordsController : Controller
    {
        private readonly IWordRepository _wordRepository;


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
            if (!UserHelper.GetCurrent().IsAdmin) return View("AccesoDenegdo");
            IList<Word> list = _wordRepository.GetAll().Where(x => x.Name.StartsWith(like)).ToList();
            var model = new CrudViewModel<Word>(list, page, 20);

            model.Filter = like;

            return View(model);
        }

        //
        // GET: /Words/Details/5

        public ViewResult Details(Guid id)
        {
            if (!UserHelper.GetCurrent().IsAdmin) return View("AccesoDenegdo");
            return View(_wordRepository.GetById(id));
        }

        //
        // GET: /Words/Create

        public ActionResult Create()
        {
            if (!UserHelper.GetCurrent().IsAdmin) return View("AccesoDenegdo");
            return View();
        }

        //
        // POST: /Words/Create

        [HttpPost]
        public ActionResult Create(Word word)
        {
            if (!UserHelper.GetCurrent().IsAdmin) return View("AccesoDenegdo");
            if (ModelState.IsValid)
            {
                _wordRepository.Save(word);
                return RedirectToAction("Index");
            }

            return View();
        }

        //
        // GET: /Words/Edit/5

        public ActionResult Edit(Guid id)
        {
            if (!UserHelper.GetCurrent().IsAdmin) return View("AccesoDenegdo");
            return View(_wordRepository.GetById(id));
        }

        //
        // POST: /Words/Edit/5

        [HttpPost]
        public ActionResult Edit(Word word)
        {
            if (!UserHelper.GetCurrent().IsAdmin) return View("AccesoDenegdo");
            if (ModelState.IsValid)
            {
                _wordRepository.Save(word);
                return RedirectToAction("Index");
            }

            return View();
        }

        //
        // GET: /Words/Delete/5

        public ActionResult Delete(Guid id)
        {
            if (!UserHelper.GetCurrent().IsAdmin) return View("AccesoDenegdo");
            return View(_wordRepository.GetById(id));
        }

        //
        // POST: /Words/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(Guid id)
        {
            if (!UserHelper.GetCurrent().IsAdmin) return View("AccesoDenegdo");
            _wordRepository.Delete(_wordRepository.GetById(id));

            return RedirectToAction("Index");
            
        }
    }
}

