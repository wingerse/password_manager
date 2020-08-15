using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace PasswordManagerV3.Views.EntryValuePairControl
{
    internal class ProtectedToProtectedTextBoxVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isProtected = (bool)value;
            return isProtected ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}