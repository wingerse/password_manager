using System;
using System.Collections.Generic;
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
    internal class AddEditCategoryWindowViewModel : ViewModel, IDialogReturnViewModel<AddCategoryModel>, IControllable<AddCategoryWindowViewModelController>
    {
        private readonly IEnumerable<Category> _categories;
        private ObservableCollection<EditableTextItem<Column>> _columns;
        private BitmapSource _icon;
        private EditableTextItem<Column> _selectedItem;
        private string _title;

        public AddEditCategoryWindowViewModel(IEnumerable<Category> categories)
        {
            _categories = categories;
            OkCommand = new RelayCommand<object>(OkClick);
            CancelCommand = new RelayCommand<object>(CancelClick);
            IconFromFileCommand = new RelayCommand<object>(IconFromFile);
            IconFromUrlCommand = new RelayCommand<object>(IconFromUrl);
            AddColumnCommand = new RelayCommand<object>(AddColumn);
            DeleteColumnCommand = new RelayCommand<object>(DeleteColumn);
            Title = "New Category";
            Columns = new ObservableCollection<EditableTextItem<Column>> {
                                                                             new EditableTextItem<Column>(new Column("Title")),
                                                                             new EditableTextItem<Column>(new Column("Location")),
                                                                             new EditableTextItem<Column>(new Column("Password", isProtected: true))
                                                                         };
        }

        public string Title {
            get { return _title; }
            set {
                _title = value;
                if (_title == "")
                    SetErrors(nameof(Title), new List<string> {"Title cannot be empty"});
                else if (_categories.Any(category => category.Name == _title))
                    SetErrors(nameof(Title), new List<string> {"There already exists a category by that name. Use another name"});
                else
                    ClearErrors(nameof(Title));
                OnPropertyChanged(nameof(Title));
            }
        }

        public BitmapSource Icon {
            get { return _icon; }
            set {
                _icon = value;
                OnPropertyChanged(nameof(Icon));
            }
        }

        public EditableTextItem<Column> SelectedItem {
            get { return _selectedItem; }
            set {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public ObservableCollection<EditableTextItem<Column>> Columns {
            get { return _columns; }
            set {
                _columns = value;
                OnPropertyChanged(nameof(Columns));
            }
        }

        public ICommand IconFromFileCommand { get; set; }
        public ICommand IconFromUrlCommand { get; set; }
        public ICommand AddColumnCommand { get; set; }
        public ICommand DeleteColumnCommand { get; set; }
        public ICommand OkCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public AddCategoryWindowViewModelController Controller { get; set; }
        public AddCategoryModel ReturnInformation { get; set; }

        private void AddColumn(object param)
        {
            var column = new EditableTextItem<Column>(new Column("New Column", false), true);
            Columns.Add(column);
            SelectedItem = column;
        }

        private void CancelClick(object param)
        {
            Controller.ReturnCancel();
        }

        private void DeleteColumn(object param)
        {
            if (SelectedItem == null) return;
            Columns.Remove(SelectedItem);
            SelectedItem = Columns.LastOrDefault();
        }

        private void IconFromFile(object param)
        {
            Icon = Controller.GetIconFromUser();
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

        private void OkClick(object param)
        {
            if (HasErrors) return;
            ReturnInformation = new AddCategoryModel(new Category(Title, Columns.Select(item => item.WrappedItem).ToList(), Icon));
            Controller.ReturnOk();
        }
    }
}