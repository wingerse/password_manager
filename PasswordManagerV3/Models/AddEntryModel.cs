using PasswordManagerV3.MVVM;

namespace PasswordManagerV3.Models
{
    internal class AddEntryModel : IDialogReturnModel
    {
        public AddEntryModel(Entry entry)
        {
            Entry = entry;
        }

        public Entry Entry { get; }
        public bool? DialogResult { get; set; }
    }
}