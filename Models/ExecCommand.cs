using System;
using System.Windows.Input;

namespace VendingMachine.Models
{
    public class ExecCommand : ICommand
    {
        private Action action;
        private bool canExecute;

        public ExecCommand(Action action, bool canExecute = true)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return canExecute;
        }

        public void Execute(object parameter)
        {
            action();
        }
    }
}
