using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExamples.LinqExtensionMethods
{
    public static class JobsLinqExtensions
    {
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            foreach(var j in source)
            {
                if (predicate(j))
                {
                    yield return j;
                }
            }
        }
    }
}
