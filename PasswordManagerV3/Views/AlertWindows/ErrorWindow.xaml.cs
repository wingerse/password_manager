using System.ComponentModel;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using MahApps.Metro.Controls;

namespace PasswordManagerV3.Views.AlertWindows
{
    /// <summary>
    ///     Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : MetroWindow, INotifyPropertyChanged
    {
        private TextBlock _text;

        public ErrorWindow()
        {
            InitializeComponent();
            okButton.Focus();
            SystemSounds.Asterisk.Play();
        }

        public TextBlock Text {
            get { return _text; }
            set {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Ok_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}