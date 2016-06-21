using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace MercadoCinotam.EntityFramework.Repositories
{
    public abstract class MercadoCinotamRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<MercadoCinotamDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected MercadoCinotamRepositoryBase(IDbContextProvider<MercadoCinotamDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class MercadoCinotamRepositoryBase<TEntity> : MercadoCinotamRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected MercadoCinotamRepositoryBase(IDbContextProvider<MercadoCinotamDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
