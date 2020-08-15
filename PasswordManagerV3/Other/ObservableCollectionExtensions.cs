using System;
using System.Collections.ObjectModel;

namespace PasswordManagerV3.Other
{
    internal static class ObservableCollectionExtensions
    {
        public static void AddSorted<T>(this ObservableCollection<T> list, T item)
            where T : IComparable<T>
        {
            list.Insert(list.FindSortedIndex(item), item);
        }

        public static int FindSortedIndex<T>(this ObservableCollection<T> list, T item)
            where T : IComparable<T>
        {
            if (item == null)
                return -1;

            var i = 0;
            while (i < list.Count && item.CompareTo(list[i]) > 0)
                i++;
            return i;
        } 
    }
}