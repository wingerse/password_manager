using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using PasswordManagerV3.Models;
using PasswordManagerV3.Other.Security;
using PasswordManagerV3.Other.Serialization.Serializers;

namespace PasswordManagerV3.Other.Serialization.Contracts
{
    [DataContract(Namespace = "")]
    [KnownType(typeof (EntryKeyValuePairSerializationContract))]
    public class EntrySerializationContract
    {
        public EntrySerializationContract(Entry entry, IBitmapSourceSerializer iconSerializer, ICipher cipher)
        {
            EntryKeyValuePairs = entry.EntryKeyValuePairs.Select(pair => new EntryKeyValuePairSerializationContract(pair, cipher));
            Icon = iconSerializer.Serialize(entry.Icon);
        }

        [DataMember]
        public IEnumerable<EntryKeyValuePairSerializationContract> EntryKeyValuePairs { get; set; }

        /// <summary>
        ///     Icon in base64 format
        /// </summary>
        [DataMember]
        public string Icon { get; set; }

        public Entry ToEntry(IBitmapSourceSerializer iconSerializer, ICipher cipher)
        {
            return new Entry(iconSerializer.Deserialize(Icon), EntryKeyValuePairs.Select(pair => pair.ToEntryKeyValuePair(cipher)).ToList());
        }
    }
}