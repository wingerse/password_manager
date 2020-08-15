using System;
using System.Globalization;
using System.Windows.Data;
using PasswordManagerV3.Models;

namespace PasswordManagerV3.Views.MainView
{
    /// <summary>
    /// Converts an EntryKeyValuePair to its Value property based on its IsProtected property. If it is protected, the return value is protected with black dots.
    /// </summary>
    internal class EntryKeyValuePairToValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var entryKeyValuePair = (EntryKeyValuePair)value;
            return entryKeyValuePair.IsProtected ? new string('\u25CF', entryKeyValuePair.Value.Length) : entryKeyValuePair.Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}