using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using PasswordManagerV3.MVVM;
using PasswordManagerV3.Controllers;
using PasswordManagerV3.Models;
using PasswordManagerV3.Views.Other;

namespace PasswordManagerV3.ViewModels
{
    internal class AddEditEntryWindowViewModel : ViewModel, IControllable<AddEntryViewModelController>, IDialogReturnViewModel<AddEntryModel>
    {
        private BitmapSource _icon;

        /// <summary>
        ///     Constructor for Adding new Entry
        /// </summary>
        public AddEditEntryWindowViewModel(Category category)
        {
            //Construct the EntryKeyValuePair list using the category columns. If column is multiline, EntryKeyValuePair will also be multiline.
            //If Column is protected, EntryKeyValuePair will also be protected
            EntryKeyValuePairs =
                new ObservableCollection<EditableTextItem<EntryKeyValuePair>>(
                    category.Columns.Select(
                        column =>
                        new EditableTextItem<EntryKeyValuePair>(
                            new EntryKeyValuePair(column.Name, string.Empty, column.IsMultiline, isProtected: column.IsProtected), canInteract: false)));

            Init();
        }

        /// <summary>
        ///     //Constructor for editing an existing entry
        /// </summary>
        /// <param name="entry">A deep copy of the entry to be edited</param>
        public AddEditEntryWindowViewModel(Entry entry)
        {
            /*Construct the EntryKeyValuePair list and Icon from the EntryKeyValuePair list and Icon of the supplied entry (Should be deep copied to prevent this window from 
             *updating the 
             *values and reflecting them back in the main window.)
             */
            EntryKeyValuePairs =
                new ObservableCollection<EditableTextItem<EntryKeyValuePair>>(
                    entry.EntryKeyValuePairs.Select(e => new EditableTextItem<EntryKeyValuePair>(e, canInteract: !e.IsMandatory)));
            Icon = entry.Icon;
            Init();
        }

        public ICommand IconFromFileCommand { get; private set; }
        public ICommand IconFromUrlCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand OkCommand { get; private set; }
        public ICommand CancelCommand { get; private set; }

        public ObservableCollection<EditableTextItem<EntryKeyValuePair>> EntryKeyValuePairs { get; }
        public bool AddMultiline { get; set; }
        public bool AddProtected { get; set; }

        public BitmapSource Icon {
            get { return _icon; }
            set {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        public AddEntryViewModelController Controller { get; set; }
        public AddEntryModel ReturnInformation { get; set; }

        private void Add(object param)
        {
            EntryKeyValuePairs.Add(
                new EditableTextItem<EntryKeyValuePair>(new EntryKeyValuePair("New Key", string.Empty, isMandatory: false, isMultiline: AddMultiline,
                                                                              isProtected: AddProtected), true));
        }

        private void CancelClick(object param)
        {
            Controller.ReturnCancel();
        }

        private void Delete(EditableTextItem<EntryKeyValuePair> param)
        {
            if (param == null) return;
            EntryKeyValuePairs.Remove(param);
        }

        private void IconFromFile(object param)
        {
            string iconPath = Controller.GetIcon();
            if (iconPath == null) return;
            Icon = new BitmapImage(new Uri(iconPath));
        }

        private async void IconFromUrl(object param)
        {
            UrlModel urlModel = Controller.GetUrlFromUser();
            if (urlModel == null || urlModel.DialogResult == false) return;
            string url = urlModel.Url;

            const string errorMessage = "There was an issue connecting to the internet. Please try again";
            try {
                using (var client = new HttpClient()) {
                    Stream stream = await client.GetStreamAsync(url);
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = stream;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    Icon = bitmap;
                }
            }
            catch (WebException) {
                Controller.ShowError(errorMessage);
            }
            catch (HttpRequestException) {
                Controller.ShowError(errorMessage);
            }
            
        }

        private void Init()
        {
            IconFromFileCommand = new RelayCommand<object>(IconFromFile);
            IconFromUrlCommand = new RelayCommand<object>(IconFromUrl);
            AddCommand = new RelayCommand<object>(Add);
            DeleteCommand = new RelayCommand<EditableTextItem<EntryKeyValuePair>>(Delete, param => param != null && !param.WrappedItem.IsMandatory);
            OkCommand = new RelayCommand<object>(OkClick);
            CancelCommand = new RelayCommand<object>(CancelClick);
        }

        private void OkClick(object param)
        {
            ReturnInformation = new AddEntryModel(new Entry(Icon, EntryKeyValuePairs.Select(item => item.WrappedItem).ToList()));
            Controller.ReturnOk();
        }
    }
}