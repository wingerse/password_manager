using System.ComponentModel;
using System.Xml.Linq;
using PasswordManagerV3.Other;

namespace PasswordManagerV3.Models
{
    public class EntryKeyValuePair : INotifyPropertyChanged
    {
        private bool _isMandatory;
        private bool _isMultiline;
        private bool _isProtected;

        private string _key;
        private string _value;

        public EntryKeyValuePair(string key, string value, bool isMultiline = false, bool isMandatory = true, bool isProtected = false)
        {
            Key = key;
            Value = value;
            IsMultiline = isMultiline;
            IsMandatory = isMandatory;
            IsProtected = isProtected;
        }

        public string Key {
            get { return _key; }
            set {
                _key = value;
                OnPropertyChanged(nameof(Key));
            }
        }

        public string Value {
            get { return _value; }
            set {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public bool IsMultiline {
            get { return _isMultiline; }
            set {
                _isMultiline = value;
                OnPropertyChanged(nameof(IsMultiline));
            }
        }

        public bool IsMandatory {
            get { return _isMandatory; }
            set {
                _isMandatory = value;
                OnPropertyChanged(nameof(IsMandatory));
            }
        }

        public bool IsProtected {
            get { return _isProtected; }
            set {
                _isProtected = value;
                OnPropertyChanged(nameof(IsProtected));
            }
        }

        /// <summary>
        ///     Performs a deep clone
        /// </summary>
        public EntryKeyValuePair Clone()
        {
            return new EntryKeyValuePair(Key, Value, IsMultiline, IsMandatory, IsProtected);
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}