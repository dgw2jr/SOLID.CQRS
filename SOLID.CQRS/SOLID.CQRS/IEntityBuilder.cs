using System.Collections.Generic;

namespace SOLID.CQRS
{
    internal interface IEntityBuilder<out TType>
    {
        IEnumerable<TType> Build();
    }
}