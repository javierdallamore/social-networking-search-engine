using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Domain;
using Core.RepositoryInterfaces;

namespace DataAccess.DAO
{
    public class DaoEntity: DaoBase<Entity,Guid>, IEntityRepository
    {
    }
}
