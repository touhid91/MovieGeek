using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MovieGeek.a1.Helpers
{
    internal static class CollectionHelper
    {
        internal static ObservableCollection<TSource> ToObservableCollection<TSource>(this IEnumerable<TSource> source)
        {
            return new ObservableCollection<TSource>(source);
        }
    }
}
