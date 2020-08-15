using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using PasswordManagerV3.Models;

namespace PasswordManagerV3.Views.MainView
{
    internal class GridViewCellTemplateSelector : DataTemplateSelector
    {
        private readonly Column _column;

        public GridViewCellTemplateSelector(Column column)
        {
            _column = column;
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var dataTemplate = new DataTemplate(typeof (Entry));

            var stackPanelFactory = new FrameworkElementFactory(typeof (Grid));

            var text = new FrameworkElementFactory(typeof (TextBlock));

            text.SetValue(TextBlock.TextTrimmingProperty, TextTrimming.CharacterEllipsis);
            text.SetValue(FrameworkElement.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            text.SetValue(FrameworkElement.VerticalAlignmentProperty, VerticalAlignment.Center);

            text.SetBinding(TextBlock.TextProperty,
                            new Binding {Path = new PropertyPath($"[{_column.Name}]"), Converter = new EntryKeyValuePairToValueConverter()});

            stackPanelFactory.AppendChild(text);

            dataTemplate.VisualTree = stackPanelFactory;

            return dataTemplate;
        }
    }
}