using System.Windows;
using PasswordManagerV3.MVVM;
using PasswordManagerV3.Controllers;
using PasswordManagerV3.Other;
using PasswordManagerV3.ViewModels;
using PasswordManagerV3.Views;

namespace PasswordManagerV3
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            Sounds.Load();
            var mainWindow = new MainWindow();

            var mainWindowViewModel = new MainWindowViewModel();

            mainWindowViewModel.Controller = new MainWindowViewModelController(mainWindow, mainWindowViewModel);

            mainWindow.DataContext = mainWindowViewModel;

            mainWindow.Show();
        }
    }
}