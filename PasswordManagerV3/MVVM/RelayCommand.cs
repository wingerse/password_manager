using System;
using System.Windows.Input;

namespace PasswordManagerV3.MVVM
{
    /// <summary>
    ///     A command class which relays the execution to a method in the ViewModel
    /// </summary>
    /// <typeparam name="TCommandParameter">
    ///     The command parameter type the relay command will use. If the relay command is called with another CommandParameter
    ///     type, an ArgumentException will be thrown.
    /// </typeparam>
    public class RelayCommand<TCommandParameter> : ICommand
    {
        private readonly Action<TCommandParameter> _action;
        private readonly Predicate<TCommandParameter> _canExecute;
        private EventHandler _canExecuteChanged;

        /// <summary>
        ///     Constructs a new relay command, relaying the execution to the <paramref name="action" />
        /// </summary>
        /// <param name="action">The action to which the execution will be relayed to</param>
        /// <param name="canExecute">The predicate which will be used to check if the command can be executed</param>
        public RelayCommand(Action<TCommandParameter> action, Predicate<TCommandParameter> canExecute = null)
        {
            _action = action;
            _canExecute = canExecute;
        }

        /// <summary>Check if the Relay Command can be executed</summary>
        /// <param name="commandParam">Parameter passed by the view. i.e The CommandParameter</param>
        public bool CanExecute(object commandParam)
        {
            switch (commandParam)
            {
                case null:
                case TCommandParameter _:
                    return _canExecute == null || _canExecute((TCommandParameter)commandParam);
                default:
                    throw new ArgumentException(string.Format("CommandParameter is not of Type {0} in relay command", nameof(TCommandParameter)));
            }
        }

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute. This is delegated to
        ///     CommandManager so that
        ///     there will be no need to raise this event whenever CanExecute method return changes. CommandManager will constantly
        ///     raise the event.
        /// </summary>
        /// <remarks>
        ///     The eventHandlers will also be added to an internal delegate so that I can manually invoke the delegate so the
        ///     control binded will re-evaluate
        ///     CanExecute method.
        /// </remarks>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                _canExecuteChanged += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                _canExecuteChanged += value;
            }
        }

        /// <summary>Executes the Action</summary>
        /// <param name="commandParam">An optional parameter passed by the view. i.e the CommandParameter</param>
        public void Execute(object commandParam)
        {
            switch (commandParam)
            {
                case null:
                case TCommandParameter _:
                    _action((TCommandParameter)commandParam);
                    break;
                default:
                    throw new ArgumentException(string.Format("CommandParameter is not of Type {0} in relay command", nameof(TCommandParameter)));
            }
        }

        /// <summary>
        /// Raises the CanExecuteChanged event to manually force controls to check if this Relay Command can Execute.
        /// Otherwise the controls will rely on CommandManager to check it.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            EventHandler canExecuteChanged = _canExecuteChanged;
            if (canExecuteChanged == null)
                return;
            canExecuteChanged((object)this, EventArgs.Empty);
        }
    }
}
