using System;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace PasswordManagerV3.Converters
{
    internal class PasswordToDotConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var password = (string)value;
            var sb = new StringBuilder();
            foreach (char c in password) {
                if (c == '\r' || c == '\n')
                    sb.Append(c);
                else
                    sb.Append('\u25CF');
            }
            return sb.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}