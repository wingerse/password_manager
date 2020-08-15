using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using PasswordManagerV3.MVVM;
using PasswordManagerV3.Controllers;
using PasswordManagerV3.Models;
using PasswordManagerV3.Other;
using PasswordManagerV3.Storage;

namespace PasswordManagerV3.ViewModels
{
    internal class MainViewModel : ViewModel, IControllable<MainViewModelController>
    {
        private readonly IStorage _storage;
        private Category _selectedCategory;
        private Entry _selectedEntry;

        public MainViewModel(IStorage storage)
        {
            _storage = storage;
            LockCommand = new RelayCommand<object>(Lock);
            BackupCommand = new RelayCommand<object>(Backup);
            NewEntryCommand = new RelayCommand<object>(MakeNewEntry, param => SelectedCategory != null);
            EditEntryCommand = new RelayCommand<object>(EditEntry);
            AboutCommand = new RelayCommand<object>(o => Controller.ShowAbout());
            AddCategoryCommand = new RelayCommand<object>(AddCategory);
            DeleteCategoryCommand = new RelayCommand<object>(DeleteCategory, param => SelectedCategory != null);
            DeleteEntryCommand = new RelayCommand<object>(DeleteEntry);
            PopulateData();
            //Adds the sort descriptor for the categories so that they will be sorted based on name
            CollectionViewSource.GetDefaultView(Categories).SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));
        }

        public ICommand LockCommand { get; private set; }
        public ICommand BackupCommand { get; private set; }
        public ICommand NewEntryCommand { get; private set; }
        public ICommand AboutCommand { get; private set; }
        public ICommand AddCategoryCommand { get; private set; }
        public ICommand DeleteCategoryCommand { get; private set; }

        public ICommand EditEntryCommand { get; private set; }
        public ICommand DeleteEntryCommand { get; private set; }

        public Category SelectedCategory {
            get { return _selectedCategory; }
            set {
                _selectedCategory = value;
                /*Adds the sort descriptor for the selected category's entries list so that they will be sorted based on the CompareTo method, this works because 
                  Entry implements IComparable<T> and IComparable (IComparable is necessary for SortDescription to work) but I implemented generic version anyway
                  Just for the sake of it.  
                */
                if(_selectedCategory != null)
                    CollectionViewSource.GetDefaultView(_selectedCategory.Entries).SortDescriptions.Add(new SortDescription("", ListSortDirection.Ascending));
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }

        public Entry SelectedEntry {
            get { return _selectedEntry; }
            set {
                _selectedEntry = value;
                OnPropertyChanged(nameof(SelectedEntry));
            }
        }

        public ObservableCollection<Category> Categories { get; private set; }

        public MainViewModelController Controller { get; set; }

        public event EventHandler NavigateToLockView;

        private void Backup(object param)
        {
            _storage.CreateBackup();
        }

        private void AddCategory(object param)
        {
            AddCategoryModel categoryModel = Controller.GetNewCategoryFromUser(Categories);
            if (categoryModel == null || categoryModel.DialogResult == false) return;

            Category category = categoryModel.Category;

            Categories.Add(category);

            SelectedCategory = category;

            //Add category to storage
            _storage.AddCategory(category);
        }

        private void DeleteCategory(object param)
        {
            if (!Controller.Prompt("Are you sure you want to delete this Category?") == true)
                return;
            if (SelectedCategory == null) return;
            //Remove from storage first because selected category will return null when it is removed from categories
            _storage.DeleteCategory(SelectedCategory);
            Categories.Remove(SelectedCategory);
        }

        private void DeleteEntry(object param)
        {
            if (!Controller.Prompt("Are you sure you want to delete this entry?") == true)
                return;

            //Remove from storage first because if it is removed from Entries, SelectedEntry will be null
            _storage.DeleteEntry(SelectedCategory, SelectedEntry);
            SelectedCategory.Entries.Remove(SelectedEntry);
        }

        private void EditEntry(object param)
        {
            AddEntryModel entryModel = Controller.EditEntryByUser(SelectedEntry.Clone());
            if (entryModel == null || entryModel.DialogResult != true) return;

            Entry entry = entryModel.Entry;

            //Replace selectedEntry with new entry
            _storage.EditEntry(SelectedCategory, SelectedEntry, entry);

            //Replace old entry with new entry in the entries
            SelectedCategory.Entries[SelectedCategory.Entries.IndexOf(SelectedEntry)] = entry;
            SelectedEntry = entry;
        }

        private void Lock(object param)
        {
            Sounds.PlayLockSound();
            OnNavigateToLockView();
        }

        private void MakeNewEntry(object param)
        {
            AddEntryModel entryModel = Controller.GetNewEntryFromUser(SelectedCategory);
            if (entryModel == null || entryModel.DialogResult != true) return;

            Entry entry = entryModel.Entry;
            SelectedCategory.Entries.Add(entry);
            SelectedEntry = entry;

            _storage.AddEntry(SelectedCategory, entry);
        }

        #region Events Firing methods

        private void OnNavigateToLockView()
        {
            EventHandler handler = NavigateToLockView;
            handler?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        private void PopulateData()
        {
            Categories = new ObservableCollection<Category>(_storage.GetCategories());
        }
    }
}