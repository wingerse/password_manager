using System;
using System.Windows.Media.Imaging;
using PasswordManagerV3.MVVM;
using Microsoft.Win32;
using PasswordManagerV3.Models;
using PasswordManagerV3.Other;
using PasswordManagerV3.ViewModels;
using PasswordManagerV3.Views;

namespace PasswordManagerV3.Controllers
{
    internal class AddCategoryWindowViewModelController : Controller<AddEditCategoryWindow, AddEditCategoryWindowViewModel>
    {
        public AddCategoryWindowViewModelController(AddEditCategoryWindow view, AddEditCategoryWindowViewModel viewModel)
            : base(view, viewModel)
        {
        }

        public BitmapImage GetIconFromUser()
        {
            var fileDialog = new OpenFileDialog {
                                                    Multiselect = false,
                                                    Title = "Select an Icon",
                                                    CheckFileExists = true,
                                                    CheckPathExists = true,
                                                    Filter = "Png files (*.png)|*.png"
                                                };
            bool? result = fileDialog.ShowDialog(View);
            return result == true ? new BitmapImage(new Uri(fileDialog.FileName)) : null;
        }

        public UrlModel GetUrlFromUser()
        {
            var urlWindow = new UrlWindow {Owner = View};
            var urlWindowViewModel = new UrlWindowViewModel();
            urlWindowViewModel.Controller = new UrlWindowViewModelController(urlWindow, urlWindowViewModel);
            urlWindow.DataContext = urlWindowViewModel;

            return urlWindow.ShowDialogReturn(urlWindowViewModel);
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

        public void ShowError(string message)
        {
            ErrorMessages.ShowErrorMessage(message, View);
        }
    }
}