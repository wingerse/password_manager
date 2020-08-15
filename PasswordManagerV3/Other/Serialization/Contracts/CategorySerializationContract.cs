using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Linq;
using PasswordManagerV3.Models;
using PasswordManagerV3.Other.Security;
using PasswordManagerV3.Other.Serialization.Serializers;

namespace PasswordManagerV3.Other.Serialization.Contracts
{
    [DataContract(Namespace = "")]
    [KnownType(typeof (ColumnSerializationContract))]
    [KnownType(typeof (EntrySerializationContract))]
    public class CategorySerializationContract
    {
        public CategorySerializationContract(Category category, IBitmapSourceSerializer iconSerializer, ICipher cipher)
        {
            Name = category.Name;
            Icon = iconSerializer.Serialize(category.Icon);
            Columns = category.Columns.Select(c => new ColumnSerializationContract(c));
            Entries = category.Entries.Select(e => new EntrySerializationContract(e, iconSerializer, cipher));
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Icon { get; set; }

        [DataMember]
        public IEnumerable<ColumnSerializationContract> Columns { get; set; }

        [DataMember]
        public IEnumerable<EntrySerializationContract> Entries { get; set; }

        public Category ToCategory(IBitmapSourceSerializer iconSerializer, ICipher cipher)
        {
            return new Category(Name, new List<Column>(Columns.Select(c => c.ToColumn())), iconSerializer.Deserialize(Icon),
                                Entries.Select(e => e.ToEntry(iconSerializer, cipher)).ToList());
        }
    }
}