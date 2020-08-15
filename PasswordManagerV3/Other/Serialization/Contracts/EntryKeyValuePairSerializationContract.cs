using System.Runtime.Serialization;
using PasswordManagerV3.Models;
using PasswordManagerV3.Other.Security;

namespace PasswordManagerV3.Other.Serialization.Contracts
{
    [DataContract(Namespace = "")]
    public class EntryKeyValuePairSerializationContract
    {
        private const string KeyPass = "t7KAk%>Wy_(\"G^=.";
        private const string ValuePass = "E''LAbYs~LF/6d2W";

        public EntryKeyValuePairSerializationContract(EntryKeyValuePair entryKeyValuePair, ICipher cipher)
        {
            Key = cipher.Encrypt(entryKeyValuePair.Key);
            Value = cipher.Encrypt(entryKeyValuePair.Value);
            IsMultiline = entryKeyValuePair.IsMultiline;
            IsMandatory = entryKeyValuePair.IsMandatory;
            IsProtected = entryKeyValuePair.IsProtected;
        }

        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public bool IsMultiline { get; set; }

        [DataMember]
        public bool IsMandatory { get; set; }

        [DataMember]
        public bool IsProtected { get; set; }

        public EntryKeyValuePair ToEntryKeyValuePair(ICipher cipher)
        {
            return new EntryKeyValuePair(cipher.Decrypt(Key), cipher.Decrypt(Value), IsMultiline, IsMandatory, IsProtected);
        }
    }
}