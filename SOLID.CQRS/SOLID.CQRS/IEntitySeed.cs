using System.Collections.Generic;

namespace SOLID.CQRS
{
    public interface IEntitySeed<out TEntity>
    {
        IEnumerable<TEntity> Execute();
    }
}