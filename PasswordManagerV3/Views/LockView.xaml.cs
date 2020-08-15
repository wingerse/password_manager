using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PasswordManagerV3.ViewModels;

namespace PasswordManagerV3.Views
{
    /// <summary>
    ///     Interaction logic for LockedView.xaml
    /// </summary>
    public partial class LockView : UserControl
    {
        public LockView()
        {
            InitializeComponent();
        }

        private void LockView_OnLoaded(object sender, RoutedEventArgs e)
        {
            passwordBox.Clear();
            passwordBox.Focus();
            Keyboard.Focus(passwordBox);
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ((dynamic)DataContext).Password = passwordBox.Password;
        }
    }
}