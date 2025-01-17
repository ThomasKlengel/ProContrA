﻿using System;
using System.Windows.Input;

namespace ProContrA.UI.ViewModels.Base
{
    public class RelayCommand : ICommand
    {
        // event thrown by the UI when e.g. a TextBox has lost focus

        /// <inheritdoc/>
        public event EventHandler CanExecuteChanged
        {
            // checks if the CanExecute has to be evaluated anew
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        /// <summary>
        /// Creates a new RelayCommand.
        /// </summary>
        /// <param name="execute">The <see cref="Action"/> the command should exectute.</param>
        /// <param name="canExecute">The <see cref="Predicate{T}"/> to check if the command can be executed.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this._execute = execute;
            this._canExecute = canExecute != null ? canExecute : new Predicate<object>((o) => { return true; });
        }

        /// <summary>
        /// Creates a new RelayCommand that can always execute.
        /// </summary>
        /// <param name="execute">The <see cref="Action"/> the command should exectute.</param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <inheritdoc/>
        public bool CanExecute(object parameter)
        {
            return this._canExecute(parameter);
        }

        /// <inheritdoc/>
        public void Execute(object parameter)
        {
            this._execute.Invoke(parameter);
        }
    }
}
