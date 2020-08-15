using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using PasswordManagerV3.Models;

namespace PasswordManagerV3.Views.MainView
{
    internal class CategoryToGridViewConverter : IValueConverter
    {
        public DataTemplate IconCellTemplate { get; set; }
        public Style HeaderContainerStyle { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var category = (Category)value;
            if (category == null) return null;

            var gridView = new GridView();
            //Add the Icon column
            var iconColumn = new GridViewColumn {CellTemplate = IconCellTemplate, Width = 30};
            gridView.Columns.Add(iconColumn);

            //Add other columns (where IsVisibleInTable == true) 
            foreach (Column column in category.Columns.Where(c => c.IsVisibleInTable)) {
                var gridViewColumn = new GridViewColumn {
                                                            Header = column.Name,
                                                            HeaderContainerStyle = HeaderContainerStyle,
                                                            //I am using CellTemplateSelector because protected EntryKeyValuePairs need to be hidden.
                                                            CellTemplateSelector = new GridViewCellTemplateSelector(column)
                                                            //DisplayMemberBinding = new Binding($"[{column.Name}].Value")
                                                        };
                gridView.Columns.Add(gridViewColumn);
            }

            return gridView;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}