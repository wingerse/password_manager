using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PasswordManagerV3.Views
{
    /// <summary>
    ///     Interaction logic for EditableTextBlock.xaml
    /// </summary>
    public partial class EditableTextBlock : UserControl
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof (string), typeof (EditableTextBlock),
                                                                                             new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty IsInEditModeProperty = DependencyProperty.Register("IsInEditMode", typeof (bool), typeof (EditableTextBlock),
                                                                                                     new PropertyMetadata(false));

        public static readonly DependencyProperty CanInteractProperty =
            DependencyProperty.Register("CanInteract", typeof (bool), typeof (EditableTextBlock), new PropertyMetadata(true));

        public EditableTextBlock()
        {
            InitializeComponent();
        }

        public bool CanInteract {
            get { return (bool)GetValue(CanInteractProperty); }
            set { SetValue(CanInteractProperty, value); }
        }

        public string Text {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public bool IsInEditMode {
            get { return (bool)GetValue(IsInEditModeProperty); }
            set { SetValue(IsInEditModeProperty, value); }
        }

        private void EditableTextBlock_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //IsInEditMode can change only if CanInteract is true. Else any interactions will leave this control as it is.
            if (!CanInteract) return;
            if (IsInEditMode || e.ClickCount != 2) return;
            IsInEditMode = true;
        }

        private void TextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!CanInteract) return;
            if (e.Key != Key.Enter && e.Key != Key.Escape) return;
            //Nope. There won't be any NullReferenceExceptions here because I know that there exists a Binding for TextBox.Text
            ((TextBox)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
            IsInEditMode = false;
        }

        private void TextBox_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!CanInteract) return;
            var textBox = sender as TextBox;
            if (textBox == null) return;
            textBox.Focus();
            textBox.SelectAll();
        }

        private void TextBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (!CanInteract) return;
            IsInEditMode = false;
        }
    }
}