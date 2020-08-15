using PasswordManagerV3.MVVM;
using PasswordManagerV3.ViewModels;
using PasswordManagerV3.Views;

namespace PasswordManagerV3.Controllers
{
    internal class UrlWindowViewModelController : Controller<UrlWindow, UrlWindowViewModel>
    {
        public UrlWindowViewModelController(UrlWindow view, UrlWindowViewModel viewModel)
            : base(view, viewModel)
        {
        }

        public void ReturnCancel()
        {
            View.DialogResult = false;
            View.Close();
        }

        public void ReturnOk()
        {
            View.DialogResult = true;
            View.Close();
        }
    }
}