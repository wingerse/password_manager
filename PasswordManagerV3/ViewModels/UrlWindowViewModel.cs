using System.Windows.Input;
using PasswordManagerV3.MVVM;
using PasswordManagerV3.Controllers;
using PasswordManagerV3.Models;

namespace PasswordManagerV3.ViewModels
{
    internal class UrlWindowViewModel : ViewModel, IDialogReturnViewModel<UrlModel>, IControllable<UrlWindowViewModelController>
    {
        private string _url;

        public UrlWindowViewModel()
        {
            OkCommand = new RelayCommand<object>(OkClick);
            CancelCommand = new RelayCommand<object>(CancelClick);
        }

        public ICommand OkCommand { get; }
        public ICommand CancelCommand { get; }

        public string Url {
            get { return _url; }
            set {
                _url = value;
                OnPropertyChanged(nameof(Url));
            }
        }

        public UrlWindowViewModelController Controller { get; set; }
        public UrlModel ReturnInformation { get; set; }

        private void CancelClick(object param)
        {
            Controller.ReturnCancel();
        }

        private void OkClick(object param)
        {
            ReturnInformation = new UrlModel(Url);
            Controller.ReturnOk();
        }
    }
}