using System;
using System.Collections.Generic;
using System.Reflection;
using Core.RepositoryInterfaces;
using NHibernate;

namespace DataAccess.DAO
{
    public abstract class RepositoryBase<TEntity, TId> : IRepositoryBase<TEntity, TId>
    {
        private static bool _propiedadesLeidas = false;
        private static PropertyInfo _propertyBaja;
        protected const int CantidadMaximaReg = 5000;

        public RepositoryBase()
        {

        }

        protected ISession Session
        {
            get
            {
                return NHSessionManager.Instance.GetSession();// this.sessionManager.OpenSession();  
            }
        }

        #region IDao<TEntity,TId> Members

        public bool BeginTransaction()
        {
            return NHSessionManager.Instance.BeginTransaction();
        }

        public void CommitTransaction()
        {
            NHSessionManager.Instance.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            NHSessionManager.Instance.RollbackTransaction();
        }

        public TEntity GetById(TId id, bool shouldLock)
        {
            TEntity entity = GetById(id);
            if (shouldLock)
                Session.Lock(entity, LockMode.Read);
            return entity;
        }

        public TEntity GetById(TId id)
        {
            TEntity entity = Session.Get<TEntity>(id);
            return entity;
        }

        public List<TEntity> GetAll()
        {
            Type entity = typeof(TEntity);
            string hql = "from " + entity.Name;

            if (!_propiedadesLeidas)
            {
                _propertyBaja = entity.GetProperty("FEC_BAJA");
                _propiedadesLeidas = true;
            }

            if (_propertyBaja != null)
            {
                hql += " where FechaBaja is null";
            }
            IQuery query = Session.CreateQuery(hql);

            List<TEntity> result = (List<TEntity>)query.SetCacheable(true).List<TEntity>();

            return result;
        }

        public List<TEntity> GetAll(string orderby)
        {
            Type entity = typeof(TEntity);
            string hql = "from " + entity.Name;

            if (!_propiedadesLeidas)
            {
                _propertyBaja = entity.GetProperty("FEC_BAJA");
                _propiedadesLeidas = true;
            }

            if (_propertyBaja != null)
            {
                hql += " where FechaBaja is null";
            }
            hql += " orderby " + orderby;
            IQuery query = Session.CreateQuery(hql);

            List<TEntity> result = (List<TEntity>)query.SetCacheable(true).List<TEntity>();

            return result;
        }

        public virtual TEntity Save(TEntity entity)
        {
            bool init = NHSessionManager.Instance.BeginTransaction();
            Session.SaveOrUpdate(entity);
            if (init)
                NHSessionManager.Instance.CommitTransaction();
            return entity;
        }

        public virtual TEntity SaveOrUpdate(TEntity entity)
        {
            bool init = NHSessionManager.Instance.BeginTransaction();
            Session.SaveOrUpdate(entity);
            if (init)
                NHSessionManager.Instance.CommitTransaction();
            return entity;
        }

        public void Delete(TEntity entity)
        {
            Session.Delete(entity);
        }

        public TEntity MakePersistent(TEntity entity)
        {
            Session.Persist(entity);
            return entity;
        }

        #endregion
    }
}