using PasswordManagerV3.MVVM;

namespace PasswordManagerV3.Models
{
    public class AddCategoryModel : IDialogReturnModel
    {
        public AddCategoryModel(Category category = null)
        {
            Category = category;
        }

        public Category Category { get; set; }
        public bool? DialogResult { get; set; }
    }
}