namespace PasswordManagerV3.MVVM
{
    /// <summary>Base class for Controllers</summary>
    /// <typeparam name="TView">Type of the View (UserControl or Window)</typeparam>
    /// <typeparam name="TViewModel">Type of ViewModel</typeparam>
    public abstract class Controller<TView, TViewModel>
    {
        /// <summary>The view this controller is bound to</summary>
        protected TView View;
        /// <summary>The ViewModel this controller is bound to</summary>
        protected TViewModel ViewModel;

        /// <summary>
        /// </summary>
        /// <param name="view"></param>
        /// <param name="viewModel"></param>
        protected Controller(TView view, TViewModel viewModel)
        {
            this.View = view;
            this.ViewModel = viewModel;
        }
    }
}
