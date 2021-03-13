using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CASTOR2.Core.Base
{
    public static class OperationHelperExtensions
    {
        /*
        * From https://stackoverflow.com/a/967098
        */
        public static int BinaryStringSearch<T>(this IList<T> list, T item) where T : MathObject
        {
            int lower = 0;
            int upper = list.Count - 1;

            while (lower <= upper)
            {
                int middle = lower + (upper - lower) / 2;
                int comparisonResult = MathObject.MathObjectComparer.Compare(item, list[middle]);
                if (comparisonResult == 0)
                    return middle;
                else if (comparisonResult < 0)
                    upper = middle - 1;
                else
                    lower = middle + 1;
            }

            return ~lower;
        }
        public static void InsertSorted<T>(this IList<T> list, T item) where T : MathObject
        {
            int index = BinaryStringSearch(list, item);
            index = index < 0 ? ~index : index;
            list.Insert(index, item);
        }
        public static IEnumerable<T> SortMathObjects<T>(this IEnumerable<T> mobjs) where T : MathObject
        {
            return mobjs.OrderBy(x => x, MathObject.MathObjectComparer);
        }
    }
}
