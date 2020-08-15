using System;
using PasswordManagerV3.MVVM;
using PasswordManagerV3.Other;
using PasswordManagerV3.Other.Security;
using PasswordManagerV3.Other.Serialization;
using PasswordManagerV3.Other.Serialization.Serializers;
using PasswordManagerV3.ViewModels;
using PasswordManagerV3.Views;
using PasswordManagerV3.Views.MainView;

namespace PasswordManagerV3.Controllers
{
    internal class MainWindowViewModelController : Controller<MainWindow, MainWindowViewModel>
    {
        private LockView _lockView;
        private LockViewModel _lockViewModel;

        private MainView _mainView;
        private MainViewModel _mainViewModel;

        public MainWindowViewModelController(MainWindow view, MainWindowViewModel vm)
            : base(view, vm)
        {
            ChangeToLockView();
        }

        public void ChangeToLockView()
        {
            if (_lockViewModel == null)
                CreateLockView();

            //Change MainWindow `View`
            ViewModel.View = _lockView;
        }

        public void ChangeToMainView()
        {
            if (_mainViewModel == null)
                CreateMainView();

            //Change MainWindow `View`
            ViewModel.View = _mainView;
        }

        private void CreateLockView()
        {
            _lockView = new LockView();

            _lockViewModel = new LockViewModel();
            _lockViewModel.NavigateToMainView += LockViewModelNavigateToMainView;

            _lockViewModel.Controller = new LockViewModelController(_lockView, _lockViewModel);

            _lockView.DataContext = _lockViewModel;
        }

        private void CreateMainView()
        {
            _mainView = new MainView();

            const string salt = "s4XLnd.tg}2?'Cy:";

            string key = Sha256.Encode(_lockViewModel.Password, salt);
            _mainViewModel = new MainViewModel(new Storage.XmlStorage(new SimpleAes(key), new Base64BitmapSourceSerializer()));
            _mainViewModel.NavigateToLockView += MainViewModelNavigateToLockView;

            _mainViewModel.Controller = new MainViewModelController(_mainView, _mainViewModel);

            _mainView.DataContext = _mainViewModel;
        }

        private void LockViewModelNavigateToMainView(object sender, EventArgs e)
        {
            ChangeToMainView();
        }

        private void MainViewModelNavigateToLockView(object sender, EventArgs e)
        {
            ChangeToLockView();
        }
    }
}