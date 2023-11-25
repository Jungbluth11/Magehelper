using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Magehelper.WPF
{
    internal static class ObservableCollectionExtensions
    {
        public static void Sort<TSource, TKey>(this ObservableCollection<TSource> source, Func<TSource, TKey> keySelector)
        {
            TSource[] sortedList;
            sortedList = source.OrderBy(keySelector).ToArray();
            source.Clear();
            foreach (TSource item in sortedList)
            {
                source.Add(item);
            }
        }
    }
}