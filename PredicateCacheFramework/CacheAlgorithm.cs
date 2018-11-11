using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredicateCacheFramework
{
    public class Cachealgorithm
    {
        /// <summary>
        /// Combinationses the of k.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">The data.</param>
        /// <param name="k">The k.</param>
        /// <returns></returns>
        public IEnumerable<IEnumerable<T>> CombinationsOfK<T>(T[] data, int k)
        {
            int size = data.Length;

            IEnumerable<IEnumerable<T>> Runner(IEnumerable<T> list, int n)
            {
                int skip = 1;
                foreach (var headList in list.Take(size - k + 1).Select(h => new T[] { h }))
                {
                    if (n == 1)
                        yield return headList;
                    else
                    {
                        foreach (var tailList in Runner(list.Skip(skip), n - 1))
                        {                            
                               yield return headList.Concat(tailList);
                        }
                        skip++;
                    }
                }
            }
            return Runner(data, k);
        }
    }
}
