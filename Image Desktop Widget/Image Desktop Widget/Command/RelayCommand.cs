using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Image_Desktop_Widget.Command
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        readonly Action Command;

        readonly Func<bool> IsExecutable;

        public RelayCommand(Action commandAction) : this(commandAction, null) { }

        public RelayCommand(Action commandAction, Func<bool> isExecutable)
        {
            if (commandAction == null) throw new ArgumentNullException("commandAction");

            Command = commandAction;
            IsExecutable = isExecutable;
        }

        public bool CanExecute(object parameter)
        {
            return IsExecutable == null ? true : IsExecutable.Invoke();
        }

        public void Execute(object parameter)
        {
            Command.Invoke();
        }
    }
}
