using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Core.Domain;
using Core.RepositoryInterfaces;
using DataAccess.DAO;
using EfficientlyLazy.Crypto;
using SocialNetWorkingSearchEngine.Models;

namespace SocialNetWorkingSearchEngine.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public User Usuario
        {
            get { return (User)Session["Usuario"]; }
            set { Session["Usuario"] = value; }
        }

        public UsersController()
        {
            _userRepository = new UserRepository();
        }

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        //
        // GET: /Users/

        public ViewResult Index(int page = 1, string like = "")
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                IList<User> list = _userRepository.GetAll().Where(x => x.Name.StartsWith(like)).ToList();
                var model = new CrudViewModel<User>(list, page, 2);
                model.Filter = like;

                return View(model);  
            }

            return View("LogOnUserControl");
        }

        //
        // GET: /Users/Details/5

        public ViewResult Details(int id)
        {
            if (Usuario != null && Usuario.IsAdmin)
            {
                return View(_userRepository.GetById(id)); 
            }

            return View("LogOnUserControl");
        }

        //
        // GET: /Users/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Users/Create

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                user.HashedPass = DataHashing.Compute(Algorithm.SHA1, user.HashedPass);
                _userRepository.Save(user);

                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /Users/Edit/5

        public ActionResult Edit(int id)
        {
            return View(_userRepository.GetById(id));
        }

        //
        // POST: /Users/Edit/5

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var entity = _userRepository.GetById(user.Id);
                entity.IsAdmin = user.IsAdmin;
                entity.Name = user.Name;              
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /Users/Delete/5

        public ActionResult Delete(int id)
        {
            return View(_userRepository.GetById(id));
        }

        //
        // POST: /Users/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            _userRepository.Delete(_userRepository.GetById(id));
            
            return RedirectToAction("Index");
        }
    }
}

