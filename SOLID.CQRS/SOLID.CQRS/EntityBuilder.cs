using System.Collections.Generic;
using System.Linq;

namespace SOLID.CQRS
{
    public class EntityBuilder<TEntity> : IEntityBuilder<TEntity>
    {
        private readonly IEntitySeed<TEntity> _seed;
        private readonly IEnumerable<IEntityComponent<TEntity>> _components;

        public EntityBuilder(
            IEntitySeed<TEntity> seed,
            IEnumerable<IEntityComponent<TEntity>> components)
        {
            _seed = seed;
            _components = components;
        }

        public IEnumerable<TEntity> Build()
        {
            if (_seed == null)
            {
                return Enumerable.Empty<TEntity>();
            }

            var seedResult = _seed.Execute();

            if (_components == null || !_components.Any())
            {
                return seedResult;
            }

            return this._components
                .Aggregate(seedResult, (seed, curr) => curr.Execute(seed).ToList());
        }
    }
}