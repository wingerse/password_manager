using System.Collections.Generic;
using System.Windows;
using PasswordManagerV3.MVVM;
using PasswordManagerV3.Models;
using PasswordManagerV3.Other;
using PasswordManagerV3.ViewModels;
using PasswordManagerV3.Views;
using PasswordManagerV3.Views.AddEntryWindow;
using PasswordManagerV3.Views.AlertWindows;
using PasswordManagerV3.Views.MainView;

namespace PasswordManagerV3.Controllers
{
    internal class MainViewModelController : Controller<MainView, MainViewModel>
    {
        public MainViewModelController(MainView mainView, MainViewModel mainViewModel)
            : base(mainView, mainViewModel)
        {
        }

        public AddEntryModel EditEntryByUser(Entry entry)
        {
            var addEntryWindow = new AddEditEntryWindow {Owner = Window.GetWindow(View), ShowInTaskbar = false, Title = "Edit Entry"};
            var addEntryWindowViewModel = new AddEditEntryWindowViewModel(entry);
            addEntryWindowViewModel.Controller = new AddEntryViewModelController(addEntryWindow, addEntryWindowViewModel);

            addEntryWindow.DataContext = addEntryWindowViewModel;

            return addEntryWindow.ShowDialogReturn(addEntryWindowViewModel);
        }

        public AddCategoryModel GetNewCategoryFromUser(IEnumerable<Category> categories)
        {
            var addCategoryWindow = new AddEditCategoryWindow {Owner = Window.GetWindow(View), ShowInTaskbar = false, Title = "Add Category"};
            var addCategoryWindowViewModel = new AddEditCategoryWindowViewModel(categories);
            addCategoryWindowViewModel.Controller = new AddCategoryWindowViewModelController(addCategoryWindow, addCategoryWindowViewModel);

            addCategoryWindow.DataContext = addCategoryWindowViewModel;

            return addCategoryWindow.ShowDialogReturn(addCategoryWindowViewModel);
        }

        public AddEntryModel GetNewEntryFromUser(Category category)
        {
            var addEntryWindow = new AddEditEntryWindow {Owner = Window.GetWindow(View), ShowInTaskbar = false, Title = "Add Entry"};
            var addEntryWindowViewModel = new AddEditEntryWindowViewModel(category);
            addEntryWindowViewModel.Controller = new AddEntryViewModelController(addEntryWindow, addEntryWindowViewModel);

            addEntryWindow.DataContext = addEntryWindowViewModel;

            return addEntryWindow.ShowDialogReturn(addEntryWindowViewModel);
        }

        public bool? Prompt(string message)
        {
            var promptWindow = new PromptWindow {Owner = Window.GetWindow(View), Text = message};
            return promptWindow.ShowDialog();
        }

        public void ShowAbout()
        {
            new AboutWindow {Owner = Window.GetWindow(View)}.ShowDialog();
        }

        public void ShowErrorMessage(string errorMessage)
        {
            ErrorMessages.ShowErrorMessage(errorMessage, Window.GetWindow(View));
        }
    }
}