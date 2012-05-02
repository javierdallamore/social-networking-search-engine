using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Domain;
using Core.RepositoryInterfaces;
using DataAccess.DAO;
using SocialNetWorkingSearchEngine.Models;

namespace SocialNetWorkingSearchEngine.Controllers
{   
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
            IList<Word> list = _wordRepository.GetAll().Where(x => x.Name.StartsWith(like)).ToList();
            var model = new CrudViewModel<Word>(list, page, 20);
            model.Filter = like;
            return View(model);
        }

        //
        // GET: /Words/Details/5

        public ViewResult Details(System.Guid id)
        {
            return View(_wordRepository.GetById(id));
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
                _wordRepository.Save(word);
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /Words/Edit/5
 
        public ActionResult Edit(System.Guid id)
        {
             return View(_wordRepository.GetById(id));
        }

        //
        // POST: /Words/Edit/5

        [HttpPost]
        public ActionResult Edit(Word word)
        {
            if (ModelState.IsValid) {
                _wordRepository.Save(word);
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /Words/Delete/5
 
        public ActionResult Delete(System.Guid id)
        {
            return View(_wordRepository.GetById(id));
        }

        //
        // POST: /Words/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(System.Guid id)
        {
            _wordRepository.Delete(_wordRepository.GetById(id));

            return RedirectToAction("Index");
        }
    }
}

