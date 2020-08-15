using System.Windows;
using PasswordManagerV3.MVVM;
using PasswordManagerV3.Other;
using PasswordManagerV3.ViewModels;
using PasswordManagerV3.Views;

namespace PasswordManagerV3.Controllers
{
    internal class LockViewModelController : Controller<LockView, LockViewModel>
    {
        public LockViewModelController(LockView view, LockViewModel viewModel)
            : base(view, viewModel)
        {
        }

        public void ShowErrorMessage(string errorMessage)
        {
            ErrorMessages.ShowErrorMessage(errorMessage, Window.GetWindow(View));
        }
    }
}