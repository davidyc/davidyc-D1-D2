using Cache.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleDemonstrationCache
{
    public class Fibonachi
    {
        private readonly ICache<int> _cache;

        public Fibonachi(ICache<int> cache)
        {
            _cache = cache;
        }

        public int ComputeFibonachi(int index)
        {
            if (index <= 0)
            {
                throw new ArgumentException($"{nameof(index)} must be positive number");
            }

            if (index == 1 || index == 2)
            {
                return 1;
            }

            int fromCache = _cache.Get(index.ToString());
            if (fromCache != default(int))
            {
                Console.WriteLine($"From cache: {fromCache}");
                return fromCache;
            }

            int result = ComputeFibonachi(index - 1) + ComputeFibonachi(index - 2);
            Console.WriteLine($"Computed: {result}");
            _cache.Set(index.ToString(), result, DateTimeOffset.Now.AddMilliseconds(300));
            return result;
        }
    }
}
