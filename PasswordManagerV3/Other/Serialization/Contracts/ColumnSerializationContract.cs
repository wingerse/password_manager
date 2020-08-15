using System.Runtime.Serialization;
using PasswordManagerV3.Models;

namespace PasswordManagerV3.Other.Serialization.Contracts
{
    [DataContract(Namespace = "")]
    public class ColumnSerializationContract
    {
        public ColumnSerializationContract(Column column)
        {
            Name = column.Name;
            IsVisibleInTable = column.IsVisibleInTable;
            IsMultiline = column.IsMultiline;
            IsProtected = column.IsProtected;
        }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public bool IsVisibleInTable { get; set; }

        [DataMember]
        public bool IsMultiline { get; set; }

        [DataMember]
        public bool IsProtected { get; set; }

        public Column ToColumn()
        {
            return new Column(Name, IsVisibleInTable, IsMultiline, IsProtected);
        }
    }
}