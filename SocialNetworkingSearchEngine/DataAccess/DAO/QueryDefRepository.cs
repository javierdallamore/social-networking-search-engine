using System;
using System.Collections.Generic;
using Core.Domain;
using Core.RepositoryInterfaces;
using NHibernate.Criterion;

namespace DataAccess.DAO
{
    public class QueryDefRepository : RepositoryBase<QueryDef, int>, IQueryDefRepository
    {
        #region Implementation of IQueryDefRepository

        public IEnumerable<QueryDef> GetActiveQuerys()
        {
            return Session.QueryOver<QueryDef>().Where(x=>x.Enabled).List();
        }

        public IEnumerable<QueryDef> GetActiveQuerysWithMinQuequeLenghtViolated()
        {
            const string query = "SELECT q.[Id] " +
                                 ",q.[Query] " +
                                 ",q.[Enabled] " +
                                 ",q.[DaysOldestPost] " +
                                 ",q.[MinQueueLength] " +
                                 ",q.[SearchEnginesNames] " +
                                 "FROM QueryDef q LEFT OUTER JOIN Post p ON p.QueryDef_id = q.Id " +
                                 "WHERE q.Enabled = 'True' " +
                                 "GROUP BY q.Id, q.Query, q.Enabled, q.DaysOldestPost, q.MinQueueLength, q.SearchEnginesNames " +
                                 "HAVING COUNT (p.Id) < q.MinQueueLength";

            var resutl = Session.CreateSQLQuery(query).AddEntity(typeof(QueryDef)).List<QueryDef>();
            return resutl;
        }

        #endregion
    }
}
