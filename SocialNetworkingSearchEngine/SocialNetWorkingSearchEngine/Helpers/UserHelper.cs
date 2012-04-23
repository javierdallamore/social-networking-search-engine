using System.Web;
using Core.Domain;
using DataAccess.DAO;

namespace SocialNetWorkingSearchEngine.Helpers
{
    public struct UserHelper
    {
        public static User GetCurrent()
        {
            var repo = new UserRepository();
            var user = repo.GetByLogin(HttpContext.Current.User.Identity.Name);
            return user;
        }
    }
}