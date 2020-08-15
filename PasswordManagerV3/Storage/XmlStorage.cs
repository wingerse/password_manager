using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using PasswordManagerV3.Models;
using PasswordManagerV3.Other.Security;
using PasswordManagerV3.Other.Serialization;
using PasswordManagerV3.Other.Serialization.Contracts;
using PasswordManagerV3.Other.Serialization.Serializers;

namespace PasswordManagerV3.Storage
{
    internal class XmlStorage : IStorage
    {
        private const string Path = "passwords.xml";
        private readonly IBitmapSourceSerializer _bitmapSourceSerializer;
        private readonly ICipher _cipher;
        //This does not use Dependency Inversion because Storage solely depends on Xml serialization
        private readonly IAnyTypeSerializer<XElement> _anyTypeToXmlSerializer = new DataContractXmlAnyTypeSerializer();

        public XmlStorage(ICipher cipher, IBitmapSourceSerializer bitmapSourceSerializer)
        {
            _cipher = cipher;
            _bitmapSourceSerializer = bitmapSourceSerializer;

            if (!StorageExists())
                CreateEmpty();
        }

        public bool StorageExists()
        {
            return File.Exists(Path);
        }

        public void CreateEmpty()
        {
            var document = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                                         new XElement(nameof(XmlStorage)));
            document.Save(Path);
        }

        public void CreateBackup()
        {
            File.Copy(Path, Path + ".backup", true);
        }

        public void AddCategory(Category category)
        {
            XDocument document = XDocument.Load(Path);

            //NullReferenceException will not occur because document is created with a root element already at start of application.
            //Adds a new CategorySerializationContract to the Root `Storage` element
            document.Root.Add(_anyTypeToXmlSerializer.Serialize(new CategorySerializationContract(category, _bitmapSourceSerializer, _cipher)));
            document.Save(Path);
        }

        public void DeleteCategory(Category category)
        {
            XDocument document = XDocument.Load(Path);

            //Get first CategorySerializationContract element whose Name matches the Name of the category parameter
            XElement categorySerializationContractElement = GetCategorySerializationContractElement(document, category);

            //Remove that element if exists
            categorySerializationContractElement?.Remove();

            document.Save(Path);
        }

        public void AddEntry(Category category, Entry entry)
        {
            XDocument document = XDocument.Load(Path);

            XElement categorySerializationContractElement = GetCategorySerializationContractElement(document, category);
            if(categorySerializationContractElement == null)
                throw new NullReferenceException("The category does not exist in the storage");

            categorySerializationContractElement.Element(nameof(Category.Entries)).
                                                 Add(_anyTypeToXmlSerializer.Serialize(new EntrySerializationContract(entry, _bitmapSourceSerializer, _cipher)));

            document.Save(Path);
        }

        public void EditEntry(Category category, Entry oldEntry, Entry newEntry)
        {
            XDocument document = XDocument.Load(Path);

            XElement categorySerializationContractElement = GetCategorySerializationContractElement(document, category);
            if (categorySerializationContractElement == null)
                throw new NullReferenceException("The category does not exist in the storage");

            int index = category.Entries.IndexOf(oldEntry);

            XElement entriesElement = categorySerializationContractElement.Element(nameof(Category.Entries));

            entriesElement.Elements().
                           ElementAt(index).
                           ReplaceWith(_anyTypeToXmlSerializer.Serialize(new EntrySerializationContract(newEntry, _bitmapSourceSerializer, _cipher)));

            document.Save(Path);
        }

        public void DeleteEntry(Category category, Entry entry)
        {
            XDocument document = XDocument.Load(Path);

            XElement categorySerializationContractElement = GetCategorySerializationContractElement(document, category);
            if (categorySerializationContractElement == null)
                throw new NullReferenceException("The category does not exist in the storage");

            int index = category.Entries.IndexOf(entry);

            XElement entriesElement = categorySerializationContractElement.Element(nameof(Category.Entries));
            entriesElement.Elements().ElementAt(index).Remove();

            document.Save(Path);
        }

        public IEnumerable<Category> GetCategories()
        {
            XDocument document = XDocument.Load(Path);
            //Deserialize all CategorySerializationContract elements under root and change to category and return.
            IEnumerable<Category> categories = document.Root.Elements().
                                                        Select(
                                                            e =>
                                                            _anyTypeToXmlSerializer.Deserialize<CategorySerializationContract>(e).
                                                                                    ToCategory(_bitmapSourceSerializer, _cipher));
            return categories.ToList();
        }

        /// <summary>
        ///     Gets the CategorySerializationContract element from the specified XDocument using the specified category. This
        ///     method uses <paramref name="category" />'s Name to find the corresponding CategorySerializationContract element.
        /// </summary>
        /// <remarks>
        ///     It uses Name because there can only be unique category names (guaranteed by the program)
        /// </remarks>
        private XElement GetCategorySerializationContractElement(XDocument document, Category category)
        {
            //Get first CategorySerializationContract element whose Name matches the Name of the category parameter
            XElement categorySerializationContractElement = document.Root.Elements().FirstOrDefault(e => e.Element(nameof(Category.Name)).Value == category.Name);
            return categorySerializationContractElement;
        }
    }
}