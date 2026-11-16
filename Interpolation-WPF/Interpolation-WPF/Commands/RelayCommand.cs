using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Interpolation_WPF.Commands
{
    internal class RelayCommand : ICommand
    {
        private readonly Action execute;

        public RelayCommand(Action action)
        {
            execute = action;
        }

        public void Execute(object? parameter)
        {
            execute();
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public event EventHandler? CanExecuteChanged;
    }
}
