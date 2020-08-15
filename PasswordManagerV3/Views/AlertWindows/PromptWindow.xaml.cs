using System.Media;
using System.Windows;
using MahApps.Metro.Controls;

namespace PasswordManagerV3.Views.AlertWindows
{
    /// <summary>
    ///     Interaction logic for PromptWindow.xaml
    /// </summary>
    public partial class PromptWindow : MetroWindow
    {
        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof (string), typeof (PromptWindow));

        public PromptWindow()
        {
            InitializeComponent();
            SystemSounds.Asterisk.Play();
        }

        public string Text {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        private void Yes_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void No_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}