using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace WixSharpSetup.Dialogs
{
    /// <summary>
    /// Implements <see cref="ICommand"/>.
    /// Redirects subscribers to <see cref="CommandManager.RequerySuggested"/> so state of bound button is updated whenever user interacts with UI.
    /// </summary>
    /// <typeparam name="T">Type of command parameter.</typeparam>
    public class RelayCommand<T> : ICommand, IDisposable
    {
        /// <summary>
        /// Action to execute.
        /// </summary>
        private readonly Action<T> _execute;

        /// <summary>
        /// Func to check if execution is possible.
        /// </summary>
        private readonly Predicate<T> _canExecute;

        /// <summary>
        /// Initializes a new instance of <see cref="RelayCommand{T}"/>
        /// </summary>
        /// <param name="execute">Action to execute.</param>
        /// <param name="canExecute">Func to check if execution is possible.</param>
        public RelayCommand(Action<T> execute, Predicate<T> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Subscribers.
        /// </summary>
        private readonly List<EventHandler> _canExecuteSubscribers = new List<EventHandler>();

        /// <summary>
        /// Indicates if object is already disposed.
        /// </summary>
        private bool _isDisposed;

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (value == null)
                {
                    return;
                }

                CommandManager.RequerySuggested += value;
                _canExecuteSubscribers.Add(value);
            }
            remove
            {
                if (value == null)
                {
                    return;
                }

                CommandManager.RequerySuggested -= value;
                _canExecuteSubscribers.Remove(value);
            }
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter"> Data used by the command.  If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        /// <returns> <see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.</returns>
        public bool CanExecute(T parameter)
        {
            if (_canExecute == null)
            {
                return true;
            }

            return _canExecute(parameter);
        }

        /// <summary>
        /// Defines the method that determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        /// <returns> <see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.</returns>
        public bool CanExecute(object parameter)
        {
            return CanExecute((T)parameter);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing,
        /// releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        public void Execute(T parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to <see langword="null" />.</param>
        public void Execute(object parameter)
        {
            Execute((T)parameter);
        }

        /// <summary>
        /// Releases resources and unsubscribes event handlers.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                foreach (var eventHandler in _canExecuteSubscribers.ToArray())
                {
                    CanExecuteChanged -= eventHandler;
                }
            }

            _isDisposed = true;
        }
    }

    /// <summary>
    /// Variant of a command that doesn't care about parameter type.
    /// </summary>
    public class RelayCommand : RelayCommand<object>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="execute">Action to execute.</param>
        /// <param name="canExecute">Func to check if execution is possible.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
            : base(execute, canExecute)
        {
        }
    }
}