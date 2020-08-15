using System;
using System.Windows;

namespace PasswordManagerV3.MVVM
{
    /// <summary>
    ///     A class with extension methods for Dialog handling
    /// </summary>
    public static class DialogExtensions
    {
        /// <summary>
        ///     ShowDialog`s a window and return the ReturnInformation from its VM. ReturnInformation will contain window's
        ///     DialogResult. ReturnInformation could be null if VM had not set it when window.ShowDialog returned.
        /// </summary>
        /// <typeparam name="TOutput">The model type this window's VM will have</typeparam>
        /// <param name="window"></param>
        /// <param name="viewModel">The VM of this window</param>
        public static TOutput ShowDialogReturn<TOutput>(
          this Window window,
          IDialogReturnViewModel<TOutput> viewModel)
          where TOutput : class, IDialogReturnModel
        {
            if (viewModel == null)
                throw new ArgumentNullException(string.Format("{0} is null", (object)nameof(viewModel)));
            if (window.DataContext != viewModel)
                throw new ArgumentException(string.Format("{0} is not the DataContext of window", (object)viewModel));
            bool? nullable = window.ShowDialog();
            TOutput returnInformation = viewModel.ReturnInformation;
            if ((object)returnInformation == null)
                return default(TOutput);
            returnInformation.DialogResult = nullable;
            return returnInformation;
        }
    }
}
