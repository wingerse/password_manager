using System.Xml.Linq;
using PasswordManagerV3.Other;

namespace PasswordManagerV3.Models
{
    public class Column
    {
        public Column(string name, bool isVisibleInTable = true, bool isMultiline = false, bool isProtected = false)
        {
            Name = name;
            IsVisibleInTable = isVisibleInTable;
            IsMultiline = isMultiline;
            IsProtected = isProtected;
        }

        public string Name { get; set; }
        public bool IsVisibleInTable { get; set; }
        public bool IsMultiline { get; set; }
        public bool IsProtected { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}