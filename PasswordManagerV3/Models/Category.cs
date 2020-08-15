using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace PasswordManagerV3.Models
{
    public class Category : INotifyPropertyChanged
    {
        private ObservableCollection<Entry> _entries;

        public Category(string name, List<Column> columns, BitmapSource icon = null, List<Entry> entries = null)
        {
            Name = name;
            Icon = icon;
            Columns = columns;
            Entries = entries == null ? new ObservableCollection<Entry>() : new ObservableCollection<Entry>(entries);
        }

        public string Name { get; set; }

        public BitmapSource Icon { get; set; }

        public List<Column> Columns { get; set; }

        public ObservableCollection<Entry> Entries {
            get { return _entries; }
            set {
                _entries = value;
                OnPropertyChanged(nameof(Entries));
            }
        }

        public Category Clone()
        {
            return new Category(Name, new List<Column>(Columns), Icon.Clone(), new List<Entry>(Entries));
        }

        public override string ToString()
        {
            return $"{Name} ({Entries.Count})";
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