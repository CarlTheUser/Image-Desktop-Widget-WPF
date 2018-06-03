using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Image_Desktop_Widget.Command
{
    class RelayParameterizedCommand : ICommand
    {
        public event EventHandler CanExecuteChanged = (s, e) => { };

        private Action<object> action;

        public Func<bool> CanExecuteFunc { get; set; } = null;

        public RelayParameterizedCommand(Action<object> action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc == null ? true : CanExecuteFunc.Invoke();
        }

        public void Execute(object parameter)
        {
            action.Invoke(parameter);
        }
    }
}
