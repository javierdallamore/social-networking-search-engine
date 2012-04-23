using Core.Domain;

namespace Core.RepositoryInterfaces
{
    public interface IUserRepository : IRepositoryBase<User, int>
    {
        User GetByLogin(string login);
    }
}