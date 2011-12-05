using System.Collections.Generic;

namespace Core.RepositoryInterfaces
{
    public interface IRepositoryBase<TEntity, TId>
    {
        bool BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        TEntity GetById(TId id, bool shouldLock);
        TEntity GetById(TId id);
        List<TEntity> GetAll();
        List<TEntity> GetAll(string orderby);
        TEntity Save(TEntity entity);
        TEntity SaveOrUpdate(TEntity entity);
        void Delete(TEntity entity);
        TEntity MakePersistent(TEntity entity);
    }
}