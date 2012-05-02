using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Domain;
using Core.RepositoryInterfaces;
using DataAccess.DAO;
using SocialNetWorkingSearchEngine.Models;

namespace SocialNetWorkingSearchEngine.Controllers
{   
    public class QueryDefsController : Controller
    {
		private readonly IQueryDefRepository _querydefRepository;

        public QueryDefsController(IQueryDefRepository querydefRepository)
        {
			_querydefRepository = querydefRepository;
        }

        //
        // GET: /QueryDefs/

        public QueryDefsController()
        {
            _querydefRepository = new QueryDefRepository();
        }

        public ViewResult Index(int page = 1, string like = "")
        {
            IList<QueryDef> list = _querydefRepository.GetAll().Where(x => x.Query.StartsWith(like)).ToList();
            var model = new CrudViewModel<QueryDef>(list, page, 10);
            model.Filter = like;
            return View(model);
        }

        //
        // GET: /QueryDefs/Details/5

        public ViewResult Details(int id)
        {
            return View(_querydefRepository.GetById(id));
        }

        //
        // GET: /QueryDefs/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /QueryDefs/Create

        [HttpPost]
        public ActionResult Create(QueryDef querydef)
        {
            if (ModelState.IsValid) {
                _querydefRepository.Save(querydef);
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }
        
        //
        // GET: /QueryDefs/Edit/5
 
        public ActionResult Edit(int id)
        {
             return View(_querydefRepository.GetById(id));
        }

        //
        // POST: /QueryDefs/Edit/5

        [HttpPost]
        public ActionResult Edit(QueryDef querydef)
        {
            if (ModelState.IsValid) {
				var entity = _querydefRepository.GetById(querydef.Id);
				//_mapper.Map<QueryDef>(entity, querydef);
                return RedirectToAction("Index");
            } else {
				return View();
			}
        }

        //
        // GET: /QueryDefs/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(_querydefRepository.GetById(id));
        }

        //
        // POST: /QueryDefs/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _querydefRepository.Delete(_querydefRepository.GetById(id));

            return RedirectToAction("Index");
        }
    }
}

