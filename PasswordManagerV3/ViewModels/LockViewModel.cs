using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using PasswordManagerV3.MVVM;
using PasswordManagerV3.Controllers;
using PasswordManagerV3.Other;

namespace PasswordManagerV3.ViewModels
{
    internal class LockViewModel : ViewModel, IControllable<LockViewModelController>
    {
        private readonly ImageSource _blueIcon;
        private readonly ImageSource _greenIcon;
        private Image _iconImage;
        private ImageSource _iconSource;

        private RotateTransform _transform;

        public LockViewModel()
        {
            UnlockCommand = new RelayCommand<object>(Unlock);

            _blueIcon = new BitmapImage(new Uri("../Icons/icon.png", UriKind.Relative));
            _greenIcon = new BitmapImage(new Uri("../Icons/greenicon.png", UriKind.Relative));
            IconSource = _blueIcon;
            Transform = new RotateTransform(0);
        }

        public ICommand UnlockCommand { get; set; }

        public Image IconImage {
            get { return _iconImage; }
            set {
                _iconImage = value;
                OnPropertyChanged(nameof(IconImage));
            }
        }

        public ImageSource IconSource {
            get { return _iconSource; }
            set {
                _iconSource = value;
                OnPropertyChanged(nameof(IconSource));
            }
        }

        public RotateTransform Transform {
            get { return _transform; }
            set {
                _transform = value;
                OnPropertyChanged(nameof(Transform));
            }
        }

        public string Password { get; set; }

        public LockViewModelController Controller { get; set; }

        public event EventHandler NavigateToMainView;

        private void OnNavigateToMainView()
        {
            EventHandler handler = NavigateToMainView;
            handler?.Invoke(this, EventArgs.Empty);
        }

        private void ResetViewModel()
        {
            Transform = new RotateTransform(0);
            IconSource = _blueIcon;
        }

        private void Unlock(object param)
        {
            // we hard code this since its only for my personal use
            const string salt = "FiKL4@C'y#K(=]zZ";

            if (Sha256.Encode(Password, salt) == "108332bcb31ad023158e82056613903d4b561440a9a69c7327f92a6fc9807fc4") {
                Sounds.PlayUnlockSound();

                IconSource = _greenIcon;

                var animation = new DoubleAnimation(0, 90, new Duration(TimeSpan.FromSeconds(0.5)));

                //Change view when completed
                animation.Completed += (sender, args) => {
                                           OnNavigateToMainView();
                                           ResetViewModel();
                                       };
                Transform.BeginAnimation(RotateTransform.AngleProperty, animation);
            }
            else
                Controller.ShowErrorMessage("The password is incorrect\r\nHint: password");
        }
    }
}
