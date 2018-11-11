using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace PredicateCacheFramework
{
    public class PredicateCacheBuilder<T>
    {
        #region Fields

        private IList<Func<T, bool>> funcs;
        public Dictionary<string, Func<T, bool>> CompiledExpressions;
        private readonly Cachealgorithm _algoritm;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="PredicateCacheBuilder{T}"/> class.
        /// </summary>
        public PredicateCacheBuilder()
        {
            funcs = new List<Func<T, bool>>();
            CompiledExpressions = new Dictionary<string, Func<T, bool>>();
            _algoritm = new Cachealgorithm();
        }

        /// <summary>
        /// Adds the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="v">The v.</param>
        public PredicateCacheBuilder<T> Add(Func<T, bool> input)
        {
            funcs.Add(input);
            return this;
        }

        /// <summary>
        /// Gets the func based on the given key
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <returns></returns>
        public Func<T, bool> Get(params bool[] keys)
        {
            try
            {
                return CompiledExpressions[string.Join("", keys.Select(item => new BoolConversion { boolValue = item }.byteValue))];
            }
            catch (KeyNotFoundException)
            {
                return msg => false;
            }
        }

        /// <summary>
        /// Generates all delegates.
        /// </summary>
        public void Generate()
        {
            var size = funcs.Count();

            for (int k = 1; k <= size; k++)
            {
                var sequence = _algoritm.CombinationsOfK(Enumerable.Range(1, size).ToArray(), k).ToList();
                foreach (var item in sequence)
                {
                    var predicate = PredicateBuilder.True<T>();

                    var keys = new bool[size];

                    foreach (var subItem in item)
                    {
                        var index = subItem - 1;

                        keys[index] = true;

                        var target = funcs[index].Clone() as Func<T, bool>;
                        predicate = predicate.And(i => target(i));
                    }

                    CompiledExpressions.Add(GenerateKey(keys), predicate.Compile());
                }
            }
        }

        /// <summary>
        /// Generates the key.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <returns></returns>
        public string GenerateKey(bool[] keys)
        {
            return string.Join("", keys.Select(item => new BoolConversion { boolValue = item }.byteValue));
        }
    }
}