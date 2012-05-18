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
    public class QueryDefsController : Controller
    {
        private readonly IQueryDefRepository _querydefRepository;

        public User Usuario
        {
            get { return (User)Session["Usuario"]; }
            set { Session["Usuario"] = value; }
        }

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
            if (Usuario != null && Usuario.IsAdmin)
            {
                IList<QueryDef> list = _querydefRepository.GetAll().Where(x => x.Query.StartsWith(like)).ToList();
                var model = new CrudViewModel<QueryDef>(list, page, 10);
                model.Filter = like;

                return View(model); 
            }

            return View("LogOnUserControl");
        }

        //
        // GET: /QueryDefs/Details/5

        public ViewResult Details(int id)
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                return View(_querydefRepository.GetById(id)); 
            }

            return View("LogOnUserControl");
        }

        //
        // GET: /QueryDefs/Create

        public ActionResult Create()
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                return View(); 
            }

            return RedirectToAction("LogOn", "Account");
        }

        //
        // POST: /QueryDefs/Create

        [HttpPost]
        public ActionResult Create(QueryDef querydef)
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                if (ModelState.IsValid)
                {
                    _querydefRepository.Save(querydef);
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
        // GET: /QueryDefs/Edit/5

        public ActionResult Edit(int id)
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                return View(_querydefRepository.GetById(id)); 
            }

            return RedirectToAction("LogOn", "Account");
        }

        //
        // POST: /QueryDefs/Edit/5

        [HttpPost]
        public ActionResult Edit(QueryDef querydef)
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                if (ModelState.IsValid)
                {
                    var entity = _querydefRepository.GetById(querydef.Id);
                    //_mapper.Map<QueryDef>(entity, querydef);
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
        // GET: /QueryDefs/Delete/5

        public ActionResult Delete(int id)
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                return View(_querydefRepository.GetById(id)); 
            }

            return RedirectToAction("LogOn", "Account");
        }

        //
        // POST: /QueryDefs/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                _querydefRepository.Delete(_querydefRepository.GetById(id));
                return RedirectToAction("Index"); 
            }

            return RedirectToAction("LogOn", "Account");
        }
    }
}

