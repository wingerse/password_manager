using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PasswordManagerV3.Models;
using PasswordManagerV3.Views.Other;

namespace PasswordManagerV3.Views.AddEntryWindow
{
    class ItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate KeyEditableTemplate { get; set; }
        public DataTemplate KeyNotEditableTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var entryKeyValurPairEditableItem = (EditableTextItem<EntryKeyValuePair>)item;
            return entryKeyValurPairEditableItem.WrappedItem.IsMandatory ? KeyNotEditableTemplate : KeyEditableTemplate;
        }
    }
}
