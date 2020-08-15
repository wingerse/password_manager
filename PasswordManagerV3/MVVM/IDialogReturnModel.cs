namespace PasswordManagerV3.MVVM
{
    /// <summary>
    /// An Interface with a single property (DialogResult). This should be implemented by the models which gets passed into Dialog windows.
    /// </summary>
    public interface IDialogReturnModel
    {
        /// <summary>
        /// When the window is closed, this should have the DialogResult of the window. It will not be null because window has been closed.
        /// </summary>
        bool? DialogResult { get; set; }
    }
}
