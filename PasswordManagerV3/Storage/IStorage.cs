using System.Collections.Generic;
using System.Linq.Expressions;
using PasswordManagerV3.Models;
using PasswordManagerV3.Other;

namespace PasswordManagerV3.Storage
{
    internal interface IStorage
    {
        /// <summary>
        /// Check if storage exists
        /// </summary>
        bool StorageExists();

        /// <summary>
        /// Creates an empty storage with single root element named "Storage"
        /// </summary>
        void CreateEmpty();

        void CreateBackup();

        /// <summary>
        ///     Adds a category to Storage
        /// </summary>
        void AddCategory(Category category);

        /// <summary>
        ///     Deletes a category from storage
        /// </summary>
        void DeleteCategory(Category category);

        void AddEntry(Category category, Entry entry);
        void EditEntry(Category category, Entry oldEntry, Entry newEntry);
        void DeleteEntry(Category category, Entry entry);

        /// <summary>
        ///     Gets all the categories saved
        /// </summary>
        IEnumerable<Category> GetCategories();
    }
}