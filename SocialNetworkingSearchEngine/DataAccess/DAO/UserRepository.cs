using System;
using Core.Domain;
using Core.RepositoryInterfaces;

namespace DataAccess.DAO
{
    public class UserRepository : RepositoryBase<User, int>, IUserRepository
    {
        public User GetByLoginPass(string userName, string password)
        {
            return
                Session.QueryOver<User>().Where(x => x.Login == userName && x.HashedPass == password).SingleOrDefault();
        }
    }
}