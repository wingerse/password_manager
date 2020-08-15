using PasswordManagerV3.MVVM;
using Microsoft.Win32;
using PasswordManagerV3.Models;
using PasswordManagerV3.Other;
using PasswordManagerV3.ViewModels;
using PasswordManagerV3.Views;
using PasswordManagerV3.Views.AddEntryWindow;
using PasswordManagerV3.Views.AlertWindows;

namespace PasswordManagerV3.Controllers
{
    internal class AddEntryViewModelController : Controller<AddEditEntryWindow, AddEditEntryWindowViewModel>
    {
        public AddEntryViewModelController(AddEditEntryWindow view, AddEditEntryWindowViewModel viewModel)
            : base(view, viewModel)
        {
        }

        public string GetIcon()
        {
            var fileDialog = new OpenFileDialog {
                                                    Multiselect = false,
                                                    Title = "Select an Icon",
                                                    CheckFileExists = true,
                                                    CheckPathExists = true,
                                                    Filter = "Png files (*.png)|*.png"
                                                };
            bool? result = fileDialog.ShowDialog(View);
            return result == true ? fileDialog.FileName : null;
        }

        public UrlModel GetUrlFromUser()
        {
            var urlWindow = new UrlWindow {Owner = View};
            var urlWindowViewModel = new UrlWindowViewModel();
            urlWindowViewModel.Controller = new UrlWindowViewModelController(urlWindow, urlWindowViewModel);
            urlWindow.DataContext = urlWindowViewModel;

            return urlWindow.ShowDialogReturn(urlWindowViewModel);
        }

        public void ShowError(string message)
        {
            ErrorMessages.ShowErrorMessage(message, View);
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