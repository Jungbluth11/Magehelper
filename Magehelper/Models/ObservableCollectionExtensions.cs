namespace Magehelper.Models;

public static class ObservableCollectionExtensions
{
    public static void Sort<TSource, TKey>(this ObservableCollection<TSource> source, Func<TSource, TKey> keySelector)
    {
        TSource[] sortedList;
        sortedList = [.. source.OrderBy(keySelector)];
        source.Clear();
        foreach (TSource item in sortedList)
        {
            source.Add(item);
        }
    }

    public static void AddRange<TSource>(this ObservableCollection<TSource> source, IEnumerable<TSource> collection)
    {
        foreach (TSource item in collection)
        {
            source.Add(item);
        }
    }
}
