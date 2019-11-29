using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jrpgsource.SpriteManagement
{
    public static partial class ExtentionMethods
    {
        /// <summary>
        /// T value Removeall
        /// </summary>
        public static void RemoveAll<TKey, TValue>(this SortedDictionary<TKey,TValue> dict,
            Func<KeyValuePair<TKey, TValue>, bool> condition)
        {
            foreach (var cur in dict.Where(condition).ToList())
            {
                dict.Remove(cur.Key);
            }
        }
    }
}
