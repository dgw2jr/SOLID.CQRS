using System.Collections.Generic;

namespace SOLID.CQRS
{
    public interface IEntityComponent<TEntity>
    {
        IEnumerable<TEntity> Execute(IEnumerable<TEntity> seed);
    }
}