using System.Windows.Controls;
using PasswordManagerV3.MVVM;
using PasswordManagerV3.Controllers;

namespace PasswordManagerV3.ViewModels
{
    internal class MainWindowViewModel : ViewModel, IControllable<MainWindowViewModelController>
    {
        private ContentControl _view;

        public MainWindowViewModelController Controller { get; set; }

        /// <summary>
        ///     Don't use this in Code. Use the ViewController instead. This is public only for use by Binding mechanism.
        /// </summary>
        public ContentControl View {
            get { return _view; }
            set {
                _view = value;
                OnPropertyChanged(nameof(View));
            }
        }
    }
}