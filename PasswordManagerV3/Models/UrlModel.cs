using PasswordManagerV3.MVVM;

namespace PasswordManagerV3.Models
{
    internal class UrlModel : IDialogReturnModel
    {
        public UrlModel(string url)
        {
            Url = url;
        }

        public string Url { get; set; }

        public bool? DialogResult { get; set; }
    }
}