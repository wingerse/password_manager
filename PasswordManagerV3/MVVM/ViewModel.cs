using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PasswordManagerV3.MVVM
{
    /// <summary>
    ///     Base class which every ViewModel should inherit from
    /// </summary>
    public abstract class ViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        /// <summary>
        ///     The errors collection. With property name and a list of errors for that property name.
        /// </summary>
        protected readonly Dictionary<string, List<string>> Errors = new Dictionary<string, List<string>>();

        /// <summary>
        ///     The event which is raised when a Property is Changed. This is used by Binding to update the view
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        ///     Raises the property changed event. Call is like this: `OnPropertyChanged(nameof(Property))`
        /// </summary>
        /// <param name="propertyName">The name of the property which was changed.</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (propertyChanged == null)
                return;
            propertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>Sets an error list to a property</summary>
        /// <param name="propertyName">The property name to set errors to</param>
        /// <param name="propertyErrors">Error list.</param>
        protected void SetErrors(string propertyName, List<string> propertyErrors)
        {
            Errors.Remove(propertyName);
            Errors.Add(propertyName, propertyErrors);
            OnErrorsChanged(propertyName);
        }

        /// <summary>
        ///     Clears the specified <paramref name="errorMessage" /> from <paramref name="propertyName" />. If <paramref name="errorMessage" /> is null. Clears all errors.
        /// </summary>
        /// <param name="propertyName">The property name to clear errors from</param>
        /// <param name="errorMessage">The error message to remove from property. Can be null if all the error messages has to be removed.</param>
        protected void ClearErrors(string propertyName, string errorMessage = null)
        {
            if (errorMessage == null)
                Errors.Remove(propertyName);
            else if (Errors.ContainsKey(propertyName) && Errors[propertyName].Contains(errorMessage))
                Errors[propertyName].Remove(errorMessage);
            OnErrorsChanged(propertyName);
        }

        /// <summary>
        ///     Gets the errors in a property name. If <paramref name="propertyName" /> is all whitespace or empty or null, the whole
        ///     Dictionary &lt;string, List&lt;string&gt;&gt; is returned, which maps propertyName to the list of errors in it. Returns emptry list if there are no errors.
        /// </summary>
        /// <param name="propertyName">The name of the property to get errors from</param>
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return Errors;
            return !Errors.ContainsKey(propertyName) ? new List<string>() : Errors[propertyName];
        }

        /// <summary>
        ///     Check if <paramref name="propertyName" /> has any errors.
        /// </summary>
        /// <param name="propertyName">The name of the property to check</param>
        /// <remarks>
        ///     Returns false even if the property has a null errorList (Which will never be true in this implementation at least)
        /// </remarks>
        public bool HasErrorsInProperty(string propertyName)
        {
            if (!Errors.ContainsKey(propertyName))
                return false;
            List<string> error = Errors[propertyName];
            return error != null && error.Any<string>();
        }

        /// <summary>Checks if this ViewModel has any errors.</summary>
        public bool HasErrors => Errors.Count > 0;

        /// <summary>
        ///     The event which is raised when any errors are changed.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private void OnErrorsChanged(string propertyName)
        {
            EventHandler<DataErrorsChangedEventArgs> errorsChanged = ErrorsChanged;
            if (errorsChanged == null)
                return;
            errorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
        }
    }
}
