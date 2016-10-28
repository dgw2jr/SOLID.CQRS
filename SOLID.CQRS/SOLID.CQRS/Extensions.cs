using System;
using System.Collections.Generic;
using System.Linq;

namespace SOLID.CQRS
{
    public static class Extensions
    {
        public static Func<IEnumerable<TRight>, IEnumerable<TResult>> Joiner<TLeft, TRight, TResult>(
            this IEnumerable<TLeft> left, 
            Func<TLeft, IEnumerable<TRight>, TResult> selector)
            where TLeft : IHasCustomerId
            where TRight : IHasCustomerId
        {
            return b => left.GroupJoin(b, l => l.CustomerId, r => r.CustomerId, selector);
        }
    }
}