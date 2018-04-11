using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.General.WpfDocument
{
    public static class IEnumerableHelper
    {

        /// <summary> 返回指定区间值 </summary>
        public static IEnumerable<T> TakeWhileFromTo<T>(this IEnumerable<T> arr, Predicate<T> from = null, Predicate<T> to = null)
        {
            bool begion = false;

            foreach (var item in arr)
            {
                if (from == null || from(item))
                {
                    begion = true;
                }

                if (begion)
                {
                    yield return item;
                }

                if (to != null && to(item)) break;
            }
        }


        /// <summary> 查找指定区间集合 </summary>
        public static IEnumerable<T> TakeFromTo<T>(this IEnumerable<T> arr, int from, int to)
        {
            bool begion = false;

            for (int i = 0; i < arr.Count(); i++)
            {
                if (i >= from && i < to) yield return arr.ElementAt(i);

            }

        }
    }
}
