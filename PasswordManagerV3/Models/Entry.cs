using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media.Imaging;

namespace PasswordManagerV3.Models
{
    public class Entry : INotifyPropertyChanged, IComparable<Entry>, IComparable
    {
        private BitmapSource _icon;

        public Entry(BitmapSource icon = null, List<EntryKeyValuePair> entryKeyValuePairs = null)
        {
            Icon = icon;
            EntryKeyValuePairs = new ObservableCollection<EntryKeyValuePair>(entryKeyValuePairs ?? new List<EntryKeyValuePair>());
        }

        public BitmapSource Icon {
            get { return _icon; }
            set {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        public ObservableCollection<EntryKeyValuePair> EntryKeyValuePairs { get; }

        /// <summary>
        ///     Gets the EntryKeyValuePair by name. If no such columns exist, first column's EntryKeyValuePair is returned. Null is
        ///     returned if EntryKeyValuePairs collection is empty.
        /// </summary>
        /// <param name="name">Column name</param>
        public EntryKeyValuePair this[string name] {
            get {
                EntryKeyValuePair keyValuePair = EntryKeyValuePairs.FirstOrDefault(pair => pair.Key == name);
                return keyValuePair ?? EntryKeyValuePairs.FirstOrDefault();
            }
        }

        public int CompareTo(Entry other)
        {
            if (this == other)
                return 0;
            if (other == null)
                return 1;

            EntryKeyValuePair firstEntryKeyValuePair = EntryKeyValuePairs.FirstOrDefault();
            EntryKeyValuePair otherFirstEntryKeyValuePair = other.EntryKeyValuePairs.FirstOrDefault();
            if (firstEntryKeyValuePair == null || otherFirstEntryKeyValuePair == null)
                return 0;
            return string.Compare(firstEntryKeyValuePair.Value, otherFirstEntryKeyValuePair.Value, StringComparison.OrdinalIgnoreCase);
        }

        public int CompareTo(object obj)
        {
            var other = (Entry)obj;
            return CompareTo(other);
        }

        /// <summary>
        ///     Performs a deep clone
        /// </summary>
        public Entry Clone()
        {
            return new Entry(Icon?.Clone(), EntryKeyValuePairs.Select(e => e.Clone()).ToList());
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}