namespace PasswordManagerV3.MVVM
{
    /// <summary>
    ///     Exposes a single property (ReturnInformation). This interface has to be implemented by Dialog ViewModels needing to return a value to controller which called it.
    /// </summary>
    /// <typeparam name="TOutput">The type which will be returned</typeparam>
    public interface IDialogReturnViewModel<TOutput> where TOutput : class, IDialogReturnModel
    {
        /// <summary>
        /// An IDialogReturnModel which the VM should set before closing
        /// </summary>
        TOutput ReturnInformation { get; set; }
    }
}
