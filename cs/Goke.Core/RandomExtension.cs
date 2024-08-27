using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goke.Core
{
    public static class RandomExtension
    {
        public static T Choice<T>(this Random random, IList<T> list)
        {
            if (list is null || list.Count == 0)
            {
                throw new ArgumentException("List cannot be null or empty");
            }
            var i = random.Next(list.Count);
            return list[i];
        }

        public static T Choice<T>(this Random random, params T[] values)
        {
            if (values is null || values.Length == 0)
            {
                throw new ArgumentException("Argument cannot be null or empty");
            }
            var i = random.Next(values.Length);
            return values[i];
        }
    }
}
