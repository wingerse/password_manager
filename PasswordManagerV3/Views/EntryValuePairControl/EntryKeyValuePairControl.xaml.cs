using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PasswordManagerV3.Views.EntryValuePairControl
{
    /// <summary>
    ///     Interaction logic for EntryKeyValuePairControl.xaml
    /// </summary>
    public partial class EntryKeyValuePairControl : UserControl
    {
        public static readonly DependencyProperty KeyProperty = DependencyProperty.Register("Key", typeof (string), typeof (EntryKeyValuePairControl));
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof (string), typeof (EntryKeyValuePairControl));
        public static readonly DependencyProperty IsProtectedProperty = DependencyProperty.Register("IsProtected", typeof (bool), typeof (EntryKeyValuePairControl));

        private readonly BitmapImage _lockWhite = new BitmapImage(new Uri("../../Icons/lockwhite.png", UriKind.Relative));
        private readonly BitmapImage _unlockWhite = new BitmapImage(new Uri("../../Icons/unlockwhite.png", UriKind.Relative));

        //Internal isProtected. This only affects the textblock rendering.
        private bool _isProtected = true;

        public EntryKeyValuePairControl()
        {
            InitializeComponent();
        }

        public bool IsProtected {
            get { return (bool)GetValue(IsProtectedProperty); }
            set { SetValue(IsProtectedProperty, value); }
        }

        public string Key {
            get { return (string)GetValue(KeyProperty); }
            set { SetValue(KeyProperty, value); }
        }

        public string Value {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private void Copy_OnClick(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(unprotectedTextBlock.Text);
        }

        private void ToConcealMode()
        {
            image.Source = _unlockWhite;
            lockButton.ToolTip = "Reveal";

            protectedTextBlock.Visibility = Visibility.Visible;
            unprotectedTextBlock.Visibility = Visibility.Hidden;
        }

        private void ToNormalMode()
        {
            image.Source = _lockWhite;
            lockButton.ToolTip = "Conceal";

            unprotectedTextBlock.Visibility = Visibility.Visible;
            protectedTextBlock.Visibility = Visibility.Hidden;
        }

        private void UnlockOrLock_OnClick(object sender, RoutedEventArgs e)
        {
            if (_isProtected) {
                ToNormalMode();
                _isProtected = false;
            }
            else {
                ToConcealMode();
                _isProtected = true;
            }
        }
    }
}