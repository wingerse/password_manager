using System.ComponentModel;

namespace PasswordManagerV3.Views.Other
{
    internal class EditableTextItem<T> : INotifyPropertyChanged
    {
        private bool _canInteract;
        private bool _isInEditMode;

        public EditableTextItem(T wrappedItem, bool isInEditMode = false, bool canInteract = true)
        {
            WrappedItem = wrappedItem;
            IsInEditMode = isInEditMode;
            CanInteract = canInteract;
        }

        public T WrappedItem { get; set; }

        public bool CanInteract {
            get { return _canInteract; }
            set {
                _canInteract = value;
                OnPropertyChanged(nameof(CanInteract));
            }
        }

        public bool IsInEditMode {
            get { return _isInEditMode; }
            set {
                _isInEditMode = value;
                OnPropertyChanged(nameof(IsInEditMode));
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}